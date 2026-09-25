using UnityEngine;
using System;

[Serializable]
public struct Option
{
    [SerializeField] private string m_name;
    public readonly string Name => m_name;
    [SerializeField] private Node m_node;
    public readonly Node Node => m_node;
    [SerializeField] private EventFlags m_flag;
    public readonly EventFlags Flag => m_flag;
}