using System;
using UnityEngine;

[Serializable]
public struct TextFile
{
    [SerializeField] private string m_name;
    public readonly string Name => m_name;
    [TextArea(5, 1000)]
    [SerializeField] private string m_texts;
    public readonly string Text => m_texts;
}