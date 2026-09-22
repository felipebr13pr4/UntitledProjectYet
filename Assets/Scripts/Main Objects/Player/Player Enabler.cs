using UnityEngine;

public class PlayerEnabler : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private PlayerInteraction m_interaction;

    private void OnEnable()
    {
        TextBox.OnState += HandleStates;
    }

    private void OnDisable()
    {
        TextBox.OnState -= HandleStates;
    }

    private void HandleStates(bool state)
    {
        m_movement.enabled = state;
        m_interaction.enabled = state;
    }
}
