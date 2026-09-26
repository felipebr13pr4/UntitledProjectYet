using System;
using UnityEngine;

public class PlayerEnabler : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private PlayerInteraction m_interaction;
    private bool m_inDialogue;
    private bool m_inCutscene;

    private void OnEnable()
    {
        TextBox.OnActivation += Dialogue;
        CutsceneMover.OnCutscene += Cutscene;
    }

    private void OnDisable()
    {
        TextBox.OnActivation -= Dialogue;
        CutsceneMover.OnCutscene -= Cutscene;
    }

    private void Cutscene(bool state)
    {
        if (!state) m_inCutscene = false;
        Inverter(state);
        if (state) m_inCutscene = true;
    }

    private void Dialogue(bool state)
    {
        if (!state) m_inDialogue = false;
        Inverter(state);
        if (state) m_inDialogue = true;
    }

    private void Inverter(bool state) => HandleStates(!state);

    private void HandleStates(bool state)
    {
        if (m_inCutscene || m_inDialogue) return;
        m_movement.enabled = state;
        m_interaction.enabled = state;
    }
}
