using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference m_interactAction;
    [SerializeField] private LayerMask m_interactablesLayer;
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private float m_reach = 1.25f;

    private void OnEnable()
    {
        m_interactAction.action.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        m_interactAction.action.performed -= OnInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context) => OnInteract();

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
}
