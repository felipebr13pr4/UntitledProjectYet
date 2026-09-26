using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Dialogue")]
public class DialogueNode : NodeWithNext
{
    [SerializeField] private DialogueLine[] m_lines;
    public DialogueLine[] Lines => m_lines;
}
