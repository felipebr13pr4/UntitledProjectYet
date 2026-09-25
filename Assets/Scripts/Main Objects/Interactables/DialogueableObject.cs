using System;
using UnityEngine;

public class DialogueableObject : InteractableObject, IInteractable
{
    public static event Action<DialogueNode> OnInteract;
    [SerializeField] private DialogueNode m_node;

    public override void Interact()
    {
        base.Interact();
        OnInteract?.Invoke(m_node);
    }
}