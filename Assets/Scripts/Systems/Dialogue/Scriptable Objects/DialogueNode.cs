using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Dialogue")]
public class DialogueNode : NodeWithNext
{
    [SerializeField] private DialogueLine[] m_lines;
    public DialogueLine[] Lines => m_lines;
    [SerializeField] private bool m_defaultVoice = true;
    private bool m_warnedClose = false;
    private int m_warnedCloseIndex = 0;
    private bool m_warnedNumber = false;
    private int m_warnedNumberIndex = 0;

    private void OnValidate()
    {
        // To do a verification system to catch invalid tags.

        if (m_defaultVoice)
        {
            for (int i = 0; i < m_lines.Length; i++)
            {
                m_lines[i].Voice = new(Resources.Load<AudioClip>("DefaultVoice"),
                    1, true, 1f, 1.5f);
            }
            m_defaultVoice = false;
        }
    }
}
