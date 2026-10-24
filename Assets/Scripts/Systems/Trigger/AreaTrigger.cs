using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AreaTrigger : MonoBehaviour
{
    protected virtual Color GizmosColor => Color.white;
    [SerializeField] private EventFlags m_flagRequired;
    public EventFlags FlagRequired => m_flagRequired;
    [SerializeField] private bool m_reverseRequirement;
    public bool ReverseRequirement => m_reverseRequirement;


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            ExecuteAction();
        }
    }

    protected virtual void OnValidate()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }

    protected virtual void ExecuteAction()
    {
        if (m_flagRequired != EventFlags.None)
        {
            if (!m_reverseRequirement)
            {
                if (!EventFlagsHolder.Get(m_flagRequired))
                {
                    UnmetFlag();
                    return;
                }
                else
                {
                    MetFlag();
                }
            }
            else
            {
                if (EventFlagsHolder.Get(m_flagRequired))
                {
                    UnmetFlag();
                    return;
                }
                else
                {
                    MetFlag();
                }
            }
        }
    }

    protected virtual void UnmetFlag() { }
    protected virtual void MetFlag() { }
}
