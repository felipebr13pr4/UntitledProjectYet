using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesBox : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private TextMeshProUGUI m_text;
    private Node m_node;
    public Node Node { set => m_node = value; }
    public string Text { set { m_text.text = value; } }
    public static event Action<Node> OnChoicePicked;

    private void OnEnable() => m_button.onClick.AddListener(ButtonClicked);

    private void OnDisable() => m_button.onClick.RemoveListener(ButtonClicked);

    private void ButtonClicked()
    {
        OnChoicePicked?.Invoke(m_node);
    }
}