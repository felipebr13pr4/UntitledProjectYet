using UnityEngine;
using UnityEngine.InputSystem;

public class TextBoxHandler : MonoBehaviour
{
    [SerializeField] private TextBox m_box;

    private void OnEnable()
    {
        DialogueableObject.OnInteract += Execute;
        CutsceneMover.OnNextNode += Execute;
    }

    private void OnDisable()
    {
        DialogueableObject.OnInteract -= Execute;
        CutsceneMover.OnNextNode -= Execute;
    }

    private void Execute(Node node)
    {
        m_box.Initialize(node);
    }
}