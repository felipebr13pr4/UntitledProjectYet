using System;
using UnityEngine;

[Serializable]
public struct DialogueLine
{
    [SerializeField] private string m_text;
    public readonly string Text => m_text;
    private AudioClip m_voice;
    private AudioClip m_sound;
    private Sprite m_sprite;
}
