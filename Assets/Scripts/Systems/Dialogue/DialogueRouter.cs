using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueRouter : MonoBehaviour
{
    [SerializeField] private TextBox m_textBox;
    [SerializeField] private ChoicesBoxHandler m_choicesBoxHandler;
    [SerializeField] private InputActionReference m_interactAction;
    private int m_index;
    public int Index => m_index;
    private DialogueNode m_dialogueNode;
    public DialogueNode DialogueNode => m_dialogueNode;
    public static event Action<bool> OnActivation;
    private bool m_isChoosing;
    public static event Action<CutsceneNode> OnCutsceneNode;
    public string DialogueText => m_dialogueNode.Lines[m_index].Text;

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
                m_textBox.Hide();
                return;
            }
            m_isChoosing = false;
            m_textBox.StartWriting(DialogueText, m_dialogueNode.Lines[m_index].Expression);
        }
        else if (node is ChoicesNode)
        {
            Choice(node as ChoicesNode);
        }
        else if (node is CutsceneNode)
        {
            OnCutsceneNode?.Invoke(node as CutsceneNode);
            m_textBox.Hide();
        }
    }

    private void Next()
    {
        if (m_isChoosing) return;
        if (m_index >= m_dialogueNode.Lines.Length - 1 && !m_textBox.IsWriting)
        {
            if (m_dialogueNode.Next is ChoicesNode)
            {
                Choice(m_dialogueNode.Next as ChoicesNode);
            }
            else if (m_dialogueNode.Next is CutsceneNode)
            {
                m_textBox.Hide();
                OnCutsceneNode?.Invoke(m_dialogueNode.Next as CutsceneNode);
            }
            else if (m_dialogueNode.Next != null)
            {
                Initialize(m_dialogueNode.Next as DialogueNode);
            }
            else
            {
                m_textBox.Hide();
            }
            return;
        }
        if (!m_textBox.IsWriting) m_index++;
        if (m_textBox.IsWriting)
        {
            m_textBox.SkipToFullLine();
        }
        else
        {
            m_textBox.StartWriting(DialogueText, m_dialogueNode.Lines[m_index].Expression);
        }
    }

    private void Choice(ChoicesNode choice)
    {
        m_choicesBoxHandler.Initialize(choice);
        m_isChoosing = true;
    }
}
