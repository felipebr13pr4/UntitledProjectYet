using System;
using UnityEngine;

[Serializable]
public struct BasicFile
{
    [SerializeField] private string m_name;
    public readonly string Name => m_name;
    [SerializeField] private string m_path;
    public readonly string Path => m_path;

    public BasicFile(string name, string path)
    {
        m_name = name;
        m_path = path;
    }
}