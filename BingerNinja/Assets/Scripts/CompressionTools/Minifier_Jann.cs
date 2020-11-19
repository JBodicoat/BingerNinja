// Jann

// Jann - 16/11/20 - First version that removes comments and put everything in one line
// Jann - 18/11/20 - Removes region and added multiple edge case checks for comment removement

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using UnityEngine;

/// <summary>
/// This tool removes comments and unnecessary whitespaces from the file and writes everything into one line
/// </summary>
public class Minifier_Jann : M
{
    private string m_AssetsPath;
    private string m_OutputDirectory;

    private List<string> m_files = new List<string>();
    private Dictionary<string, string> m_codeReplacements;

    private void Start()
    {
        m_codeReplacements = new Dictionary<string, string>();
        m_codeReplacements.Add("MonoBehaviour", "M");
        // m_codeReplacements.Add("GetComponent", "G");
        m_codeReplacements.Add("GameObject.Find", "F");
        m_codeReplacements.Add("GameObject.FindGameObjectWithTag", "FT");
        m_codeReplacements.Add("FindObjectOfType", "F");
        m_codeReplacements.Add("FindObjectsOfType", "Fs");
        m_codeReplacements.Add("StartCoroutine", "SC");
        m_codeReplacements.Add("Destroy", "D");

        m_AssetsPath = Application.dataPath;
        m_OutputDirectory = m_AssetsPath + "/../Minified/";

        RetrieveAllScripts(m_AssetsPath);

        // Remove compression tools from files
        m_files.RemoveAll(path => path.Contains("Minifier_Jann.cs") || path.Contains("M.cs"));

        if (Directory.Exists(m_OutputDirectory))
        {
            Directory.Delete(m_OutputDirectory, true);
        }

        Directory.CreateDirectory(m_OutputDirectory);

        List<string> codebase = new List<string>();
        foreach (string file in m_files)
        {
            string relativePath = file.Substring(m_AssetsPath.Length);
            string[] splits = relativePath.Split('\\');
            string filename = splits[splits.Length - 1];
            string directory = relativePath.Substring(0, relativePath.Length - filename.Length);

            string[] source = File.ReadAllLines(file);
            string minified = Minify(source);

            codebase.Add(SaveFile(directory, filename, minified));
        }

        if (CanCompile(codebase.ToArray()))
        {
            print("Minified code compiled successfully");
        }
    }

    public string Minify(string[] code)
    {
        // Remove comments
        string[] minifed = RemoveSinglelineComments(code);
        minifed = RemoveMultilineComments(minifed);

        // Remove regions
        minifed = RemoveRegions(minifed);

        // Change methods to use M class
        minifed = ApplyMethodShortener(minifed);

        string output = "";

        for (int i = 0; i < minifed.Length; i++)
        {
            string line = minifed[i];

            output += line.Trim() + " ";
        }

        return output;
    }

    #region Code manipulation methods

    private string[] ApplyMethodShortener(string[] code)
    {
        for (int i = 0; i < code.Length; i++)
        {
            foreach (var pair in m_codeReplacements)
            {
                switch (pair.Key)
                {
                    case "GetComponent":
                        // if(!code[i].Contains("gameObject.GetComponent"))
                        //     code[i] = ReplaceWithDefault(code[i], pair.Key, pair.Value);
                        break;
                    default:
                        code[i] = ReplaceWithDefault(code[i], pair.Key, pair.Value);
                        break;
                }
            }
        }

        return code;
    }

    private string ReplaceWithDefault(string line, string from, string to)
    {
        string pattern = $@"\b{from}\b";
        return Regex.Replace(line, pattern, to);
    }

    private string[] RemoveSinglelineComments(string[] source)
    {
        List<string> sourceWithoutComments = new List<string>();

        for (int i = 0; i < source.Length; i++)
        {
            string line = source[i].Trim();
            if (line.Length == 0 || (line[0] == '/' && line[1] == '/'))
                continue;

            if (line.Contains("//"))
            {
                // Add code from before the the start of the comment
                sourceWithoutComments.Add(line.Substring(0, line.IndexOf("//")));
            }
            else
            {
                sourceWithoutComments.Add(line);
            }
        }

        return sourceWithoutComments.ToArray();
    }

    private string[] RemoveMultilineComments(string[] source)
    {
        List<string> sourceWithoutComments = new List<string>();
        bool isInsideComment = false;

        for (int i = 0; i < source.Length; i++)
        {
            if (source[i].Contains("/*"))
            {
                if (source[i].IndexOf("/*") > 1)
                {
                    // Check for multiline comment in a single line
                    if (source[i].Contains("/*") && source[i].Contains("*/"))
                    {
                        sourceWithoutComments.Add(source[i].Substring(0, source[i].IndexOf("/*") - 1));
                        sourceWithoutComments.Add(source[i].Substring(source[i].IndexOf("*/") + 2));
                        continue;
                    }

                    // Add code from before the opening comment
                    sourceWithoutComments.Add(source[i].Substring(0, source[i].IndexOf("/*") - 1));
                }

                isInsideComment = true;
                continue;
            }

            if (source[i].Contains("*/"))
            {
                // Add code from after the closing comment
                sourceWithoutComments.Add(source[i].Substring(source[i].IndexOf("*/") + 2));
                isInsideComment = false;
                continue;
            }

            if (!isInsideComment)
            {
                sourceWithoutComments.Add(source[i]);
            }
        }

        return sourceWithoutComments.ToArray();
    }

    private string[] RemoveRegions(string[] source)
    {
        List<string> sourceWithoutRegions = new List<string>();

        for (int i = 0; i < source.Length; i++)
        {
            if (source[i].Contains("#region") || source[i].Contains("#endregion"))
                continue;

            sourceWithoutRegions.Add(source[i]);
        }

        return sourceWithoutRegions.ToArray();
    }

    #endregion

    #region FileHandling

    private void RetrieveAllScripts(string assetsDirectory)
    {
        if (File.Exists(assetsDirectory))
        {
            ProcessFile(assetsDirectory);
        }
        else if (Directory.Exists(assetsDirectory))
        {
            ProcessDirectory(assetsDirectory);
        }
    }

    private void ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string fileName in fileEntries)
        {
            ProcessFile(fileName);
        }

        // Recurse into subdirectories of this directory.
        string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
        foreach (string subdirectory in subdirectoryEntries)
        {
            ProcessDirectory(subdirectory);
        }
    }

    private void ProcessFile(string path)
    {
        if (path.Substring(path.Length - 3).Equals(".cs"))
        {
            m_files.Add(path);
        }
    }

    private string SaveFile(string directory, string filename, string source)
    {
        Directory.CreateDirectory(m_OutputDirectory + directory);

        string path = $"{m_OutputDirectory}/{directory}/{filename}";
        File.WriteAllText(path, source);

        return path;
    }

    private string SaveFile(string directory, string filename, string[] source)
    {
        Directory.CreateDirectory(m_OutputDirectory + directory);

        string path = $"{m_OutputDirectory}/{directory}/{filename}";
        File.WriteAllLines(path, source);

        return path;
    }

    #endregion

    #region CompilationUtils

    private bool CanCompile(string[] program)
    {
        var errors = Compile(program).Errors;
        if (!errors.HasErrors)
            return true;
        else
        {
            foreach (CompilerError error in errors)
            {
                if (!error.IsWarning)
                    print(error.ToString());
            }

            return false;
        }
    }

    private CompilerResults Compile(string[] filenames)
    {
        CompilerResults compilerResults = null;
        using (CSharpCodeProvider provider = new CSharpCodeProvider())
        {
            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.GenerateExecutable = false;

            var assemblies = from asm in AppDomain.CurrentDomain.GetAssemblies()
                where !asm.IsDynamic
                select asm.Location;
            compilerParameters.ReferencedAssemblies.AddRange(assemblies.ToArray());

            compilerResults = provider.CompileAssemblyFromFile(compilerParameters, filenames);
        }

        return compilerResults;
    }

    #endregion
}