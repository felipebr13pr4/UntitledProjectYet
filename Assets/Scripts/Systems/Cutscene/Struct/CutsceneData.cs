using System;
using UnityEngine;

[Serializable]
public struct CutsceneData
{
    [SerializeField] private Vector2 m_pos;
    public readonly Vector2 Pos => m_pos;
    [SerializeField] private bool m_interact;
    public readonly bool Interact => m_interact;
    [SerializeField] private Vector2 m_dir;
    public readonly Vector2 Dir => m_dir;
    [SerializeField] private float m_delay;
    public readonly float Delay => m_delay;
}
