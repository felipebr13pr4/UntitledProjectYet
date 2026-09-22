using System;
using UnityEngine;

[Serializable]
public struct DialogueData
{
    [SerializeField] private string[] m_lines;
    public readonly string[] Lines => m_lines;
    private AudioClip m_voice;
    private AudioClip m_sound;
    private Sprite m_sprite;
}
