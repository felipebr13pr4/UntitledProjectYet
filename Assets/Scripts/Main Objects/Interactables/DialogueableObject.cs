using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class DialogueableObject : InteractableObject
{
    public static event Action<Node> OnInteract;
    [SerializeField] private Node m_node;
    [SerializeField] private Node m_unmetRequirementNode;
    [SerializeField] private EventFlags m_flagRequired;
    public EventFlags FlagRequired => m_flagRequired;
    [SerializeField] private bool m_reverseRequirement;
    public bool ReverseRequirement => m_reverseRequirement;

    public override void Interact()
    {
        base.Interact();

        if (m_flagRequired != EventFlags.None)
        {
            bool allow = false;

            ErrorLogger.DebugLog(EventFlagsHolder.Get(m_flagRequired));

            if (!m_reverseRequirement)
            {
                allow = !EventFlagsHolder.Get(m_flagRequired);
            }
            else
            {
                allow = EventFlagsHolder.Get(m_flagRequired);
            }
            if (allow)
            {
                if (m_unmetRequirementNode != null)
                    OnInteract?.Invoke(m_unmetRequirementNode);
                return;
            }
        }

        OnInteract?.Invoke(m_node);
    }
}