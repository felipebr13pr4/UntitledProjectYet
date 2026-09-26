using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextBox : MonoBehaviour
{
    [SerializeField] private ChoicesBoxHandler m_choicesBoxHandler;
    [SerializeField] private InputActionReference m_interactAction;
    [SerializeField] private TextMeshProUGUI m_tmp;
    public TextMeshProUGUI Tmp => m_tmp;
    private int m_index;
    private DialogueNode m_dialogueNode;
    public static event Action<bool> OnActivation;
    private Coroutine m_coroutine;
    private bool m_isWriting;
    private bool m_isChoosing;
    public static event Action<CutsceneNode> OnCutsceneNode;

    private void OnEnable()
    {
        OnActivation?.Invoke(true);
        m_interactAction.action.performed += OnInteractPerformed;
        ChoicesBox.OnChoicePicked += Initialize;
    }

    private void OnDisable()
    {
        OnActivation?.Invoke(false);
        m_interactAction.action.performed -= OnInteractPerformed;
        ChoicesBox.OnChoicePicked -= Initialize;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context) => Next();

    public void Initialize(Node node)
    {
        gameObject.SetActive(true);
        if (node is DialogueNode)
        {
            m_dialogueNode = node as DialogueNode;
            m_index = 0;
            if (m_index == m_dialogueNode.Lines.Length)
            {
                ErrorLogger.LogError("For some reason a dialogue object has no lines. Make sure to insert the lines or to make this a just interactable if it is not supposed to have dialogue.");
                Hide();
                return;
            }
            m_isChoosing = false;
            m_coroutine = StartCoroutine(TypeWriter());
        }
        else if (node is ChoicesNode)
        {
            Choice(node as ChoicesNode);
        }
        else if (node is CutsceneNode)
        {
            OnCutsceneNode?.Invoke(node as CutsceneNode);
            Hide();
        }
    }

    // Good to see the sources.
    public void Show(string line)
    { 
        m_tmp.text = line;
    }

    public void Hide()
    {
        m_isChoosing = false;
        m_tmp.text = "";
        gameObject.SetActive(false);
    }

    private void Next()
    {
        if (m_isChoosing) return;
        if (m_index >= m_dialogueNode.Lines.Length - 1 && !m_isWriting)
        {
            if (m_dialogueNode.Next is ChoicesNode)
            {
                Choice(m_dialogueNode.Next as ChoicesNode);
            }
            else if (m_dialogueNode.Next is CutsceneNode)
            {
                Hide();
                OnCutsceneNode?.Invoke(m_dialogueNode.Next as CutsceneNode);
                //StartCoroutine(SpecialFunctions.DelayMethod(Hide));
            }
            else if (m_dialogueNode.Next != null)
            {
                Initialize(m_dialogueNode.Next as DialogueNode);
            }
            else
            {
                Hide();
            }
            return;
        }
        if(!m_isWriting) m_index++;
        if (m_isWriting)
        {
            StopCoroutine(m_coroutine);
            m_isWriting = false;
            Show(m_dialogueNode.Lines[m_index].Text);
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
        foreach (char c in m_dialogueNode.Lines[m_index].Text)
        {
            tmp += c;
            Show(tmp);
            yield return null;
        }
        m_isWriting = false;
    }

    private void Choice(ChoicesNode choice)
    {
        m_choicesBoxHandler.Initialize(choice);
        m_isChoosing = true;
    }
}
