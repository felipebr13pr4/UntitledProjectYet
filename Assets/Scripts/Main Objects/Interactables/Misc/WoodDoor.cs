using UnityEditor;
using UnityEngine;

public class WoodDoor : ToggleableObject
{
    [SerializeField] protected GameObject m_open;
    [SerializeField] protected GameObject m_closed;

    protected virtual void Start() => HandleState();

    protected override void HandleState()
    {
        m_open.SetActive(m_toggledState);
        m_closed.SetActive(!m_toggledState);
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;

        if (m_toggledState)
        {
            if (!m_open.activeInHierarchy)
            {
                EditorApplication.delayCall += () =>
                {
                    if (this != null && gameObject != null)
                        HandleState();
                };
            }
        }
        else
        {
            if (m_open.activeInHierarchy)
            {
                EditorApplication.delayCall += () =>
                {
                    if (this != null && gameObject != null)
                        HandleState();
                };
            }
        }
#endif
    }
}