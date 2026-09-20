using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : AudioBasics
{
    [SerializeField] private AudioSource m_stoppableAudioSource;


    public static AudioController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_audioVolume = PlayerPrefs.GetFloat(PrefKeys.Volume, 1f);
        SetAudio(m_audioVolume);
    }

    public void PlayAudio(AudioData data)
    {
        float pitch = data.Pitch;
        if (data.IsPitchRandom) pitch = Random.Range(data.Min, data.Max);
        m_audioSource.pitch = pitch;
        m_audioSource.PlayOneShot(data.Clip, m_audioVolume);
    }

    public void PlayStoppableAudio(AudioData data)
    {
        float pitch = data.Pitch;
        if (data.IsPitchRandom) pitch = Random.Range(data.Min, data.Max);
        m_stoppableAudioSource.pitch = pitch;
        m_stoppableAudioSource.Stop();
        m_stoppableAudioSource.PlayOneShot(data.Clip, m_audioVolume);
    }
}