using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_tmp;
    [SerializeField] private float m_typingSpeed = 0.03f;
    private Coroutine m_coroutine;
    private bool m_isWriting;
    public bool IsWriting => m_isWriting;
    private string m_text;
    public static event Action<LineExpression> OnExpression;

    // Good to see the sources.
    public void Show(string line)
    { 
        m_tmp.text = line;
    }

    public void Hide()
    {
        OnExpression?.Invoke(new LineExpression());
        m_tmp.text = "";
        gameObject.SetActive(false);
    }

    public void SkipToFullLine()
    {
        StopCoroutine(m_coroutine);
        m_isWriting = false;
        Show(m_text);
    }

    public void StartWriting(string text, LineExpression expression)
    {
        OnExpression?.Invoke(expression);
        m_text = text;
        m_coroutine = StartCoroutine(TypeWriter());
    }

    private IEnumerator TypeWriter()
    {
        m_isWriting = true;
        string tmp = "";
        float elapsed = 0f;
        foreach (char c in m_text)
        {
            while (elapsed < m_typingSpeed)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
            tmp += c;
            Show(tmp);
            elapsed = 0f;
        }
        m_isWriting = false;
    }
}
