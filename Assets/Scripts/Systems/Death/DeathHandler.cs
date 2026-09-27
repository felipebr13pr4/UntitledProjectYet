using System;
using System.Collections;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public static event Action OnDeathLate;
    public static event Action OnDeath;
    public static event Action OnDeathVideo;
    private bool m_finishedVideo;

    private void OnEnable()
    {
        DeathTrigger.OnTriggerEnter += DeathWrap;
        DeathOverlay.OnVideoFinish += Finished;
    }

    private void OnDisable()
    {
        DeathTrigger.OnTriggerEnter -= DeathWrap;
        DeathOverlay.OnVideoFinish -= Finished;
    }

    private void DeathWrap(float delay) => StartCoroutine(Death(delay));
    private void Finished() => m_finishedVideo = true;

    private IEnumerator Death(float delay)
    {
        OnDeath?.Invoke();
        yield return new WaitForSecondsRealtime(delay);
        OnDeathVideo?.Invoke();
        while (!m_finishedVideo)
        {
            yield return null;
        }
        OnDeathLate?.Invoke();
    }
}
