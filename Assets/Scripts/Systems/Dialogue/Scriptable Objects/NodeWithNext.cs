using UnityEngine;

public class NodeWithNext : Node
{
    [SerializeField] private Node m_next;
    public Node Next => m_next;
}