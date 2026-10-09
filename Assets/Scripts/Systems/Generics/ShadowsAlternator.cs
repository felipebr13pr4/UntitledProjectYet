using UnityEngine;

public class ShadowsAlternator : MonoBehaviour
{
    public enum ShadowID
    {
        First, Second, Third
    }
    [SerializeField] private ShadowID m_shadowID;
    [SerializeField] private GameObject m_shadows1;
    [SerializeField] private GameObject m_shadows2;
    [SerializeField] private GameObject m_shadows3;

    private void Start()
    {
        m_shadows1.SetActive(true);
        m_shadows2.SetActive(false);
        m_shadows3.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Execute();
    }

    private void Execute()
    {
        switch (m_shadowID)
        {
            case ShadowID.First:
                m_shadows1.SetActive(true);
                m_shadows2.SetActive(false);
                m_shadows3.SetActive(false);
                return;

            case ShadowID.Second:
                m_shadows1.SetActive(false);
                m_shadows2.SetActive(true);
                m_shadows3.SetActive(false);
                return;

            case ShadowID.Third:
                m_shadows1.SetActive(false);
                m_shadows2.SetActive(false);
                m_shadows3.SetActive(true);
                return;

            default:
                ErrorLogger.LogError("Something went wrong, shadow Id not recognized, make sure all entries in shadow alternator match the enum size.");
                return;
        }
    }
}
