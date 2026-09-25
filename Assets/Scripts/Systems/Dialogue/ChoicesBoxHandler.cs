using UnityEngine;

public class ChoicesBoxHandler : MonoBehaviour
{
    [SerializeField] private ChoicesBox[] m_choicesBox;
    [SerializeField] private GameObject[] m_components;

    private void OnEnable()
    {
        ChoicesBox.OnChoicePicked += HideWrap;
    }

    private void OnDisable()
    {
        ChoicesBox.OnChoicePicked -= HideWrap;
    }

    public void Initialize(ChoicesNode node)
    {
        foreach (GameObject comp in m_components)
        {
            comp.SetActive(true);
        }
        for (int i = 0; i < node.Options.Length; i++)
        {
            m_choicesBox[i].gameObject.SetActive(true);
            m_choicesBox[i].Node = node.Options[i].Node;
            m_choicesBox[i].Text = node.Options[i].Name;
        }
    }

    private void HideWrap(Node node) => Hide();

    private void Hide()
    {
        foreach (ChoicesBox box in m_choicesBox)
        {
            box.gameObject.SetActive(false);
        }
        foreach (GameObject comp in m_components)
        {
            comp.SetActive(false);
        }
    }
}
