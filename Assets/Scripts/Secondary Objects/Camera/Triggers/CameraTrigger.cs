using System;
using UnityEngine;

public class CameraTrigger : AreaTrigger
{
    [SerializeField] private CameraData m_data;
    public static event Action<CameraData> OnTriggerEnter;
    protected override Color GizmosColor => Color.yellow;

    [ContextMenu("Make Static At Trigger")]
    private void StaticDefault()
    {
        m_data = new(Boundary.Zero, Boundary.Zero, true, true, gameObject.transform.position, true);
    }

    protected override void ExecuteAction()
    {
        OnTriggerEnter?.Invoke(m_data);
    }
}
