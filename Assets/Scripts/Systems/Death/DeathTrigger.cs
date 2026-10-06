using System;
using UnityEngine;

public class DeathTrigger : AreaTrigger
{
    [SerializeField] private float m_delay;
    [SerializeField] private DeathType m_type;
    protected override Color GizmosColor => Color.red;
    public static event Action<float, DeathType> OnTriggerEnter;

    protected override void ExecuteAction()
    {
        OnTriggerEnter?.Invoke(m_delay, m_type);
    }
}
