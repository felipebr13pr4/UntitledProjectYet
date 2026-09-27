using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DeathOverlay : MonoBehaviour
{
    [SerializeField] private VideoPlayer m_video;
    [SerializeField] private RawImage m_image;
    public static event Action OnVideoFinish;

    private void OnEnable()
    {
        m_video.loopPointReached += FinishedVideo;
        DeathHandler.OnDeathVideo += Play;
    }

    private void OnDisable()
    {
        m_video.loopPointReached -= FinishedVideo;
        DeathHandler.OnDeathVideo -= Play;
    }

    public void Play()
    {
        m_image.color = Color.white;
        m_video.Play();
    }

    private void FinishedVideo(VideoPlayer source) => Finished();

    private void Finished()
    {
        OnVideoFinish?.Invoke();
    }
}
