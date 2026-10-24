using System;
using UnityEngine;

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
            bool allow = true;

            ErrorLogger.DebugLog(EventFlagsHolder.Get(m_flagRequired));

            if (!m_reverseRequirement)
            {
                allow = EventFlagsHolder.Get(m_flagRequired);
            }
            else
            {
                allow = !EventFlagsHolder.Get(m_flagRequired);
            }
            if (!allow)
            {
                UnMetRequirement();
                if (m_unmetRequirementNode != null)
                    OnInteract?.Invoke(m_unmetRequirementNode);
                return;
            }
            else
            {
                MetRequirement();
            }
        }

        Interacted();
        OnInteract?.Invoke(m_node);
    }

    protected virtual void MetRequirement() { }
    protected virtual void UnMetRequirement() { }
    protected virtual void Interacted() { }
}