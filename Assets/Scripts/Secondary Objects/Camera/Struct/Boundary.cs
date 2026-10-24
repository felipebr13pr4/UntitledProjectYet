
using System;
using UnityEngine;

[Serializable]
public struct Boundary
{
    [SerializeField] private float m_start;
    [SerializeField] private float m_end;
    public readonly float Start => m_start;
    public readonly float End => m_end;
    public static Boundary Zero
    {
        get
        {
            return new(0,0);
        }
    }

    public Boundary(float start, float end)
    {
        m_start = start;
        m_end = end;
    }
}