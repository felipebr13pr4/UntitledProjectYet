using System.Collections;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_tmp;
    private Coroutine m_coroutine;
    private bool m_isWriting;
    public bool IsWriting => m_isWriting;
    private string m_text;

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

    public void SkipToFullLine()
    {
        StopCoroutine(m_coroutine);
        m_isWriting = false;
        Show(m_text);
    }

    public void StartWriting(string text)
    {
        m_text = text;
        m_coroutine = StartCoroutine(TypeWriter());
    }

    private IEnumerator TypeWriter()
    {
        m_isWriting = true;
        string tmp = "";
        foreach (char c in m_text)
        {
            tmp += c;
            Show(tmp);
            yield return null;
        }
        m_isWriting = false;
    }
}
