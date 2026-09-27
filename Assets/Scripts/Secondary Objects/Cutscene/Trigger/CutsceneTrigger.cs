using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private EventFlags m_flag;
    [SerializeField] private bool m_isRepeatable;
    [SerializeField] private CutsceneNode m_node;
    public static event Action<CutsceneNode> OnTriggerEnter;
    private bool m_hasTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!EventFlagsHolder.Get(m_flag))
            if (!m_hasTouched)
                if (collision.GetComponent<PlayerMovement>() != null)
                {
                    OnTriggerEnter?.Invoke(m_node);
                    if (!m_isRepeatable)
                        m_hasTouched = true;

                    if (m_flag != EventFlags.None)
                        EventFlagsHolder.Set(m_flag, true);
                }
    }

    private void OnValidate()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blueViolet;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }

#if UNITY_EDITOR
    [ContextMenu("Test, set flag to false")]
    private void Test()
    {
        EventFlagsHolder.Set(m_flag, false);
    }
#endif
}
