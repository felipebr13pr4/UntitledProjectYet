using System;
using System.Collections;
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
    public static event Action<bool> OnState;
    private Coroutine m_coroutine;
    private bool m_isWriting;

    private void OnEnable()
    {
        OnState?.Invoke(false);
        m_interactAction.action.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        OnState?.Invoke(true);
        m_interactAction.action.performed -= OnInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context) => Next();

    public void Initialize(DialogueData data)
    {
        gameObject.SetActive(true);
        m_data = data;
        m_index = 0;
        if (m_index == m_data.Lines.Length)
        {
            ErrorLogger.LogError("For some reason a dialogue object has no lines. Make sure to insert the lines or to make this a just interactable if it is not supposed to have dialogue.");
            Hide();
            return;
        }
        StartCoroutine(TypeWriter());
    }

    // Good to see the sources.
    public void Show(string line)
    { 
        m_tmp.text = line;
    }

    public void Hide()
    {
        m_tmp.text = "";
        gameObject.SetActive(false);
    }

    private void Next()
    {
        if (m_index >= m_data.Lines.Length - 1 && !m_isWriting)
        {
            Hide();
            return;
        }
        if(!m_isWriting) m_index++;
        if (m_isWriting)
        {
            StopCoroutine(m_coroutine);
            m_isWriting = false;
            Show(m_data.Lines[m_index]);
        }
        else
        {
            m_coroutine = StartCoroutine(TypeWriter());
        }
    }

    private IEnumerator TypeWriter()
    {
        m_isWriting = true;
        string tmp = "";
        foreach (char c in m_data.Lines[m_index])
        {
            tmp += c;
            Show(tmp);
            yield return null;
        }
        m_isWriting = false;
    }
}
