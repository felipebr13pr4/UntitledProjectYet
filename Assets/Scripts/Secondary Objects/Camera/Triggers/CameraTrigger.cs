using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CameraData m_data;
    public static event Action<CameraData> OnTriggerEnter;

    [ContextMenu("Make Static At Trigger")]
    private void StaticDefault()
    {
        m_data = new(Boundary.Zero, Boundary.Zero, true, true, gameObject.transform.position, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            OnTriggerEnter?.Invoke(m_data);
        }
    }

    private void OnValidate()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
