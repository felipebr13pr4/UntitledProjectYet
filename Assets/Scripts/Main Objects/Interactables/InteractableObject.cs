using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public virtual void Interact()
    {
        ErrorLogger.DebugLog($"Interacted! + {gameObject.name}");
    }
}