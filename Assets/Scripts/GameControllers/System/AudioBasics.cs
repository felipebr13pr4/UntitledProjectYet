using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioBasics : MonoBehaviour
{
    protected float m_audioVolume;
    public float AudioVolume
    {
        get => m_audioVolume;
        private set
        {
            m_audioVolume = value;
            m_audioVolume = Mathf.Clamp01(m_audioVolume);
            m_audioSource.volume = m_audioVolume;
        }
    }

    [SerializeField] protected AudioSource m_audioSource;

    public void SetAudio(float value)
    {
        AudioVolume = value;
    }
}
