using System;
using UnityEngine;

public class DialogueableObject : InteractableObject, IInteractable
{
    public static event Action<DialogueData> OnInteract;
    [SerializeField] private DialogueData m_data;

    public override void Interact()
    {
        base.Interact();
        OnInteract?.Invoke(m_data);
    }
}