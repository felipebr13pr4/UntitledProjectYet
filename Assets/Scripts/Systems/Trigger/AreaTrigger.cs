using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AreaTrigger : MonoBehaviour
{
    protected virtual Color GizmosColor => Color.white;

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

    protected virtual void ExecuteAction() { }
}
