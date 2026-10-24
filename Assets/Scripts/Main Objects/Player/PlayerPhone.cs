using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPhone : MonoBehaviour
{
    [SerializeField] private DialogueNode m_phoneDialogue;
    [SerializeField] private InputActionReference m_moveAction;
    [SerializeField] private Animator m_animator;
    [SerializeField] private LayerMask m_phoneableLayer;
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private float m_reach = 3f;
    public static event Action<DialogueNode> OnPhoneActivated;

    private void Awake()
    {
        ChoicesBox.OnChoicePicked += PhoneChoicePicked;
    }

    private void OnEnable()
    {
        m_moveAction.action.performed += OnActionPerformed;
    }

    private void OnDisable()
    {
        m_moveAction.action.performed -= OnActionPerformed;
    }

    private void OnDestroy()
    {
        ChoicesBox.OnChoicePicked -= PhoneChoicePicked;
    }

    private void OnActionPerformed(InputAction.CallbackContext context) => OnPhone();

    private void OnPhone()
    {
        OnPhoneActivated?.Invoke(m_phoneDialogue);
    }

    private void PhoneChoicePicked(Node node)
    {
        if (node is PhoneNode)
        {
            PhoneNode phone = node as PhoneNode;
            switch (phone.Type)
            {
                case PhoneChoiceType.None: return;

                case PhoneChoiceType.Check:
                    StartCoroutine(SpecialFunctions.DelayMethodFrame(OnPhoneCheck));
                    return;

                default:
                    ErrorLogger.LogError($"Something went wrong, a phone type has been received in player phone but it did not fall under any of the switch cases. Make sure all types exist in the switch cases. Type received: {phone.Type}");
                    return;
            }
        }
    }

    private void OnPhoneCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_movement.LastDir, m_reach, m_phoneableLayer);

        Color rayColor = Color.purple;

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out DialogueableObject phoneable))
            {
                rayColor = Color.lawnGreen;
                phoneable?.Interact();
                m_movement.OutsideMoveDir = Vector2.zero;
            }
        }
        Debug.DrawRay(transform.position, m_movement.LastDir.normalized * (m_reach), rayColor, 1);
    }
}
