
using System;
using UnityEngine;

[Serializable]
public struct CameraData
{
    [SerializeField] private Boundary m_xBoundary;
    [SerializeField] private Boundary m_yBoundary;
    [SerializeField] private bool m_isXLocked;
    [SerializeField] private bool m_isYLocked;
    [SerializeField] private Vector2 m_position;
    [SerializeField] private bool m_shouldPosition;

    public Boundary XBoundary
    {
        readonly get => m_xBoundary;
        set => m_xBoundary = value;
    }

    public Boundary YBoundary
    {
        readonly get => m_yBoundary;
        set => m_yBoundary = value;
    }

    public bool IsXLocked
    {
        readonly get => m_isXLocked;
        set => m_isXLocked = value;
    }

    public bool IsYLocked
    {
        readonly get => m_isYLocked;
        set => m_isYLocked = value;
    }

    public Vector2 Position
    {
        readonly get => m_position;
        set => m_position = value;
    }

    public bool ShouldPosition
    {
        readonly get => m_shouldPosition;
        set => m_shouldPosition = value;
    }

    public CameraData(Boundary xBoundary, Boundary yBoundary, bool isXLocked, bool isYLocked, Vector2 position, bool shouldPosition)
    {
        m_xBoundary = xBoundary;
        m_yBoundary = yBoundary;
        m_isXLocked = isXLocked;
        m_isYLocked = isYLocked;
        m_position = position;
        m_shouldPosition = shouldPosition;
    }
}