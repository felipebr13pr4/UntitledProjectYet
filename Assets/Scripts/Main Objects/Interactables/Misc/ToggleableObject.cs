using UnityEngine;

public class ToggleableObject : DialogueableObject
{
    [SerializeField] protected bool m_toggledState;

    protected override void Interacted()
    {
        m_toggledState = !m_toggledState;
        HandleState();
    }

    protected virtual void HandleState() { }
}