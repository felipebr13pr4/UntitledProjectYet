using System;
using UnityEngine;

public class CutsceneTrigger : AreaTrigger
{
    [SerializeField] private EventFlags m_flag;
    [SerializeField] private bool m_isRepeatable;
    [SerializeField] private CutsceneNode m_node;
    public static event Action<CutsceneNode> OnTriggerEnter;
    private bool m_hasTouched;
    protected override Color GizmosColor => Color.blueViolet;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!EventFlagsHolder.Get(m_flag))
            if (!m_hasTouched)
                base.OnTriggerEnter2D(collision);
    }

    protected override void ExecuteAction()
    {
        OnTriggerEnter?.Invoke(m_node);
        if (!m_isRepeatable)
            m_hasTouched = true;

        if (m_flag != EventFlags.None)
            EventFlagsHolder.Set(m_flag, true);
    }


#if UNITY_EDITOR
    [ContextMenu("Test, set flag to false")]
    private void Test()
    {
        EventFlagsHolder.Set(m_flag, false);
    }
#endif
}
