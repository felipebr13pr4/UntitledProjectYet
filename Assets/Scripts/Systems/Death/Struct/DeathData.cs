using System;
using UnityEngine;

[Serializable]
public struct DeathData
{
    [SerializeField] private PhoneNode m_node;
    public readonly PhoneNode Node => m_node;
    [SerializeField] private DeathType m_type;
    public readonly DeathType Type => m_type;
}
