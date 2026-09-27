using System;
using UnityEngine;

public class DeathTrigger : AreaTrigger
{
    [SerializeField] private float m_delay;
    protected override Color GizmosColor => Color.red;
    public static event Action<float> OnTriggerEnter;

    protected override void ExecuteAction()
    {
        OnTriggerEnter?.Invoke(m_delay);
    }
}
