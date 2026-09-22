using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextBox : MonoBehaviour
{
    [SerializeField] private InputActionReference m_interactAction;
    [SerializeField] private TextMeshProUGUI m_tmp;
    public TextMeshProUGUI Tmp => m_tmp;
    private int m_index;
    private DialogueData m_data;

    private void OnEnable()
    {
        m_interactAction.action.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        m_interactAction.action.performed -= OnInteractPerformed;
    }

    public void Initialize(DialogueData data)
    {
        m_data = data;
        m_index = 0;
        if (m_index == m_data.Lines.Length)
        {
            ErrorLogger.LogError("For some reason a dialogue object has no lines. Make sure to insert the lines or to make this a just interactable if it is not supposed to have dialogue.");
            Hide();
            return;
        }
        Show(m_data.Lines[m_index]);
    }

    public void Show(string line)
    {
        gameObject.SetActive(true);
        m_tmp.text = line;
    }

    public void Hide()
    {
        m_tmp.text = "";
        gameObject.SetActive(false);
    }

    private void Next()
    {
        if (m_index == m_data.Lines.Length - 1)
        {
            Hide();
            return;
        }
        m_index++;
        Show(m_data.Lines[m_index]);
    }

    private void OnInteractPerformed(InputAction.CallbackContext context) => Next();
}
