using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeUIStart : MonoBehaviour
{
    [SerializeField] private Image m_image;
    [SerializeField] private bool m_hideInEditor;

    void Start()
    {
        if (m_hideInEditor)
        {
            Color color = m_image.color;
            color.a = 1;
            m_image.color = color;
        }
        StartCoroutine(Fade());
    }

    private void OnValidate()
    {
        if (m_hideInEditor)
        {
            Color color = m_image.color;
            color.a = 0;
            m_image.color = color;
        }
    }

    private IEnumerator Fade()
    {
        Color color = m_image.color;
        float elapsed = 0f;
        float duration = 2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - (elapsed / duration);
            m_image.color = color;
            yield return null;
        }
        color.a = 0f;
        m_image.color = color;
    }
}
