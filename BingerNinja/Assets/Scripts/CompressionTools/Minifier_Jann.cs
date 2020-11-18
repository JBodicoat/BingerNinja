// Jann

// Jann - 16/11/20 - First version that removes comments and put everything in one line
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This tool removes comments and unnecessary whitespaces from the file and writes everything into one line
/// </summary>
public class Minifier_Jann : M
{
    private bool IsSpaceOrTab(char ch) => ch == ' ' || ch == '\t';
    private bool IsAsciiLetter(char ch) => (ch = (char) (ch & ~0x20)) >= 'A' && ch <= 'z';
    private bool IsWordChar(char ch) => char.IsLetter(ch) || ch >= '0' && ch <= '9' || ch == '_';

    private const string MonoBehaviour = "MonoBehaviour";
    
    private string m_AssetsPath;
    private string m_OutputDirectory;

    private List<string> m_files = new List<string>();

    private void Start()
    {
        m_AssetsPath = Application.dataPath;
        m_OutputDirectory = m_AssetsPath + "/../Minified/";

        RetrieveAllScripts(m_AssetsPath);

        if (Directory.Exists(m_OutputDirectory))
        {
            Directory.Delete(m_OutputDirectory, true);
        }

        Directory.CreateDirectory(m_OutputDirectory);

        int files = 3;
        foreach (string file in m_files)
        {
            string relativePath = file.Substring(m_AssetsPath.Length);
            string[] splits = relativePath.Split('\\');
            string filename = splits[splits.Length - 1];
            string directory = relativePath.Substring(0, relativePath.Length - filename.Length);

            string[] source = File.ReadAllLines(file);
            string minified = Minify(source);

            SaveFile(directory, filename, minified);
            
            // if(--files <= 0)
            //     break;
        }
    }

    public string Minify(string[] code)
    {
        // Remove comments
        string[] minifed = RemoveSinglelineComments(code);
        minifed = RemoveMultilineComments(minifed);

        // Remove regions
        minifed = RemoveRegions(minifed);
        
        // Change to derive from M class instead of MonoBehaviour
        minifed = ChangeMonoBehaviour(minifed);
        
        string output = "";

        for (int i = 0; i < minifed.Length; i++)
        {
            string line = minifed[i];

            output += line;
        }

        return output;
    }

    #region Code manipulation methods
    private string[] ChangeMonoBehaviour(string[] code)
    {
        for (int i = 0; i < code.Length; i++)
        {
            if (code[i].Contains(MonoBehaviour))
            {
                code[i] = code[i].Replace(MonoBehaviour, "M");;
                return code;
            }
        }

        return code;
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
                sourceWithoutComments.Add(source[i].Substring(0, line.IndexOf("//") - 1));
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
                // Add code from before the opening comment
                if (source[i].IndexOf("/*") > 1)
                {
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

    private void SaveFile(string directory, string filename, string source)
    {
        Directory.CreateDirectory(m_OutputDirectory + directory);
        File.WriteAllText(string.Format("{0}/{1}/{2}", m_OutputDirectory, directory, filename), source);
    }

    #endregion
}