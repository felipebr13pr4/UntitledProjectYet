using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Node")]
public class DialogueNode : Node
{
    [SerializeField] private DialogueLine[] m_lines;
    public DialogueLine[] Lines => m_lines;
    [SerializeField] private Node m_next;
    public Node Next => m_next;
}
