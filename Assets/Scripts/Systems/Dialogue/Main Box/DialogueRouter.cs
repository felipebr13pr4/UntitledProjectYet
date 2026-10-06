using System;
using System.Collections;
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
    public static event Action<PhoneNode> OnPhoneNode;
    public string DialogueText => m_dialogueNode.Lines[m_index].Text;
    public bool InLastLineEnd => m_index >= m_dialogueNode.Lines.Length - 1 && !m_textBox.IsWriting;
    private Coroutine m_skipCoroutine;

    private void OnEnable()
    {
        OnActivation?.Invoke(true);
        m_interactAction.action.performed += OnInteractPerformed;
        ChoicesBox.OnChoicePicked += Initialize;
        TextBox.OnEndLine += LineEnd;
    }

    private void OnDisable()
    {
        OnActivation?.Invoke(false);
        m_interactAction.action.performed -= OnInteractPerformed;
        ChoicesBox.OnChoicePicked -= Initialize;
        TextBox.OnEndLine -= LineEnd;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context) => Next();

    private void LineEnd()
    {
        if (m_dialogueNode.Lines[m_index].AutoSkip)
        {
            if (m_skipCoroutine != null) StopCoroutine(m_skipCoroutine);
            m_skipCoroutine =
                StartCoroutine(Skip(m_dialogueNode.Lines[m_index].Delay));
        }
    }

    private IEnumerator Skip(float time)
    {
        yield return new WaitForSeconds(time);
        if (!InLastLineEnd)
            m_index++;
        else
            { DecideNextAction(); yield break; }
        Write();
    }

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
            Write();
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
        else if (node is PhoneNode)
        {
            OnPhoneNode?.Invoke(node as PhoneNode);
            PhoneNode phone = node as PhoneNode;
            if (phone.Next != null)
            {
                Initialize(phone.Next);
                return;
            }
            m_textBox.Hide();
        }
        else if (node == null)
        {
            m_textBox.Hide();
        }
    }

    private void Next()
    {
        if (m_isChoosing) return;
        if (InLastLineEnd)
        {
            if (m_dialogueNode.Lines[m_index].Unskippable &&
                m_dialogueNode.Lines[m_index].AutoSkip)
                return;
            DecideNextAction();
            return;
        }
        if (!m_textBox.IsWriting)
        {
            if (m_dialogueNode.Lines[m_index].Unskippable &&
                m_dialogueNode.Lines[m_index].AutoSkip)
                return;

            m_index++; 
        }
        if (m_skipCoroutine != null) StopCoroutine(m_skipCoroutine);
        if (m_textBox.IsWriting)
        {
            if (!m_dialogueNode.Lines[m_index].Unskippable)
                m_textBox.SkipToFullLine();
        }
        else
        {
            Write();
        }
    }

    private void Write()
    {
        m_textBox.StartWriting(DialogueText,
            m_dialogueNode.Lines[m_index].Expression,
            m_dialogueNode.Lines[m_index].Voice,
            m_dialogueNode.Lines[m_index].Sounds);
    }

    private void DecideNextAction()
    {
        if (m_skipCoroutine != null) StopCoroutine(m_skipCoroutine);
        if (m_dialogueNode.Next is ChoicesNode)
        {
            Choice(m_dialogueNode.Next as ChoicesNode);
        }
        else if (m_dialogueNode.Next is CutsceneNode)
        {
            OnCutsceneNode?.Invoke(m_dialogueNode.Next as CutsceneNode);
            m_textBox.Hide();
        }
        else if (m_dialogueNode.Next is PhoneNode)
        {
            OnPhoneNode?.Invoke(m_dialogueNode.Next as PhoneNode);
            Initialize(m_dialogueNode.Next);
        }
        else if (m_dialogueNode.Next != null)
        {
            Initialize(m_dialogueNode.Next);
        }
        else
        {
            m_textBox.Hide();
        }
    }

    private void Choice(ChoicesNode choice)
    {
        m_choicesBoxHandler.Initialize(choice);
        m_isChoosing = true;
    }
}
