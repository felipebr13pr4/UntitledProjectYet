using System;
using UnityEngine;

[Serializable]
public struct DialogueLine
{
    [TextArea(5, 1000)]
    [SerializeField] private string m_text;
    public readonly string Text => m_text;
    [SerializeField] private LineExpression m_expression;
    public readonly LineExpression Expression => m_expression;
    [SerializeField] private AudioData m_voice;
    public AudioData Voice { readonly get => m_voice; set => m_voice = value; }
    [SerializeField] private AudioData[] m_sounds;
    public AudioData[] Sounds { readonly get => m_sounds; set => m_sounds = value; }
    [SerializeField] private bool m_autoSkip;
    public readonly bool AutoSkip => m_autoSkip;
    [SerializeField] private float m_delay;
    public readonly float Delay => m_delay;
    [SerializeField] private bool m_unskippable;
    public readonly bool Unskippable => m_unskippable;
}
