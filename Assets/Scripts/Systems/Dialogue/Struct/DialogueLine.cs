using System;
using UnityEngine;

[Serializable]
public struct DialogueLine
{
    [SerializeField] private string m_text;
    public readonly string Text => m_text;
    [SerializeField] private LineExpression m_expression;
    public readonly LineExpression Expression => m_expression;

    // to be implemented
    private AudioClip m_voice;
    private AudioClip m_sound;
}
