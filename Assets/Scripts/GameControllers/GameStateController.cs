using System;
using UnityEngine;

[RequireComponent(typeof(AudioHolder))]
public class GameStateController : MonoBehaviour
{
    private bool m_canUnpause;
    private bool m_isGamePaused;

    public static event Action OnGamePaused;
    public static event Action<bool> OnGamePausedWithIfLocked;

    public static GameStateController Instance { get; private set; }
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

    private void OnEnable()
    {
        OverlayWindow.OnOpenWindow += CheckIfCanPause;
        OverlayWindow.OnCloseWindow += CheckIfCanPause;
        // Put things when there is something to listen to prevent it from pausing.
        DeathHandler.OnDeath += Pause;
        DeathHandler.OnDeath += DisablePausing;

    }

    private void OnDisable()
    {
        OverlayWindow.OnOpenWindow -= CheckIfCanPause;
        OverlayWindow.OnCloseWindow -= CheckIfCanPause;
        // Put things when there is something to listen to prevent it from pausing.
        DeathHandler.OnDeath -= Pause;
        DeathHandler.OnDeath -= DisablePausing;
    }

    private void Pause() => TogglePause(true);
    private void DisablePausing() => m_canUnpause = false;

    public void CheckIfCanPause()
    {
        CheckIfCanPause(Time.timeScale > 0);
    }

    public void CheckIfCanPause(bool pause)
    {
        if (!m_canUnpause ||
            SceneController.Instance.IsInMenu) return;
        TogglePause(pause);
    }

    private void TogglePause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        m_isGamePaused = Time.timeScale == 0;
        OnGamePaused?.Invoke();
        OnGamePausedWithIfLocked?.Invoke(m_canUnpause);
    }

    public void ResetStates()
    {
        m_canUnpause = true;
        m_isGamePaused = false;
    }
}
