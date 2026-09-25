using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference m_interactAction;
    [SerializeField] private InputActionReference m_checkAction;
    [SerializeField] private LayerMask m_interactablesLayer;
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private float m_reach = 1.25f;
    [SerializeField] private bool m_checkDebug = false;

    private void OnEnable()
    {
        m_interactAction.action.performed += OnInteractPerformed;
        m_checkAction.action.performed += OnCheckPerformed;
    }

    private void OnDisable()
    {
        m_interactAction.action.performed -= OnInteractPerformed;
        m_checkAction.action.performed -= OnCheckPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context) => OnInteract();
    private void OnCheckPerformed(InputAction.CallbackContext context) => OnCheck();

    private void OnInteract()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, m_movement.LastDir, m_reach, m_interactablesLayer);

        Color rayColor = Color.red;

        if (hit.Length != 0)
        {
            rayColor = Color.green;
            var interactable = hit[^1].collider.GetComponentInParent<IInteractable>();
            interactable?.Interact();
        }
        Debug.DrawRay(transform.position, m_movement.LastDir.normalized * m_reach, rayColor, 1);
    }

    private void OnCheck()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position + (Vector3)m_movement.LastDir.normalized * m_reach, 1, m_interactablesLayer);

        Color rayColor = Color.red;

        if (hit.Length != 0)
        {
            rayColor = Color.green;
            ErrorLogger.DebugLog(hit.Length);
            // Remember to put here a proper more correct way that shows the player
            // how many objects are in there.
        }
        Debug.DrawRay(transform.position, m_movement.LastDir.normalized * m_reach, rayColor, 1);
    }

    private void OnDrawGizmosSelected()
    {
        if (m_checkDebug)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position + (Vector3)m_movement.LastDir.normalized * m_reach, 1);
        }
    }
}
