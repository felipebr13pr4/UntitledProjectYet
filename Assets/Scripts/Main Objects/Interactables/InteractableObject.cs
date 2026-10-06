using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private EventFlags m_eventFlag;

    public virtual void Interact()
    {
        EventFlagsHolder.Set(m_eventFlag, true);
        ErrorLogger.DebugLog($"Interacted! + {gameObject.name}");
    }
}