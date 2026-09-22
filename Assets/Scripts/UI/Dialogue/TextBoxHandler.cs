using UnityEngine;
using UnityEngine.InputSystem;

public class TextBoxHandler : MonoBehaviour
{
    [SerializeField] private TextBox m_box;

    private void OnEnable()
    {
        DialogueableObject.OnInteract += Execute;
    }

    private void OnDisable()
    {
        DialogueableObject.OnInteract -= Execute;
    }

    private void Execute(DialogueData data)
    {
        m_box.Initialize(data);
    }

}