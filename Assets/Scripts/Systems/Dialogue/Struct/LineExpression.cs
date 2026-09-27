using System;
using UnityEngine;

[Serializable]
public struct LineExpression
{
    [SerializeField] private Expressions m_assignedExpression;
    public readonly Expressions AssignedExpression => m_assignedExpression;
    [SerializeField] private bool m_hasFullBody;
    public readonly bool HasFullBody => m_hasFullBody;

}