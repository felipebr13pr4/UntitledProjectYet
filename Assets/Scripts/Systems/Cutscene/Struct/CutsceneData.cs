using System;
using UnityEngine;

[Serializable]
public struct CutsceneData
{
    [SerializeField] private Vector2 m_pos;
    public readonly Vector2 Pos => m_pos;
}
