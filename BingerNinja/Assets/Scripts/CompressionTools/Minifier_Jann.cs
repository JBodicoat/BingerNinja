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

    private string m_AssetsPath;
    private string m_OutputDirectory;

    private bool m_inMultilineComment;

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

        foreach (string file in m_files)
        {
            string relativePath = file.Substring(m_AssetsPath.Length);
            string[] splits = relativePath.Split('\\');
            string filename = splits[splits.Length - 1];
            string directory = relativePath.Substring(0, relativePath.Length - filename.Length);

            string[] source = File.ReadAllLines(file);
            string minified = Minify(source);

            SaveFile(directory, filename, minified);
            // break;
        }
    }

    public string Minify(string[] code)
    {
        string output = "";

        char lastCh = '\0';
        int lastSingleLineCommentLine = 0;

        foreach (string codeLine in code)
        {
            string line = codeLine.Trim();
            if (line.Length == 0)
                continue;

            if (m_inMultilineComment)
            {
                if (line.Contains("*/"))
                {
                    m_inMultilineComment = false;
                    output += line.Substring(line.IndexOf("*/") + 1);
                    continue;
                }
            }
            
            char firstChar = line[0];

            switch (firstChar)
            {
                case '/':
                    handleSlash(line);
                    break;
                default:
                    if (!m_inMultilineComment)
                        output += line;
                    break;
            }
        }

        return output;
    }

    #region CharacterHandling

    public void handleSlash(string line)
    {
        char secondChar = line[1];

        switch (secondChar)
        {
            case '/':
                break;
            case '*':
                m_inMultilineComment = true;
                break;
        }
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
