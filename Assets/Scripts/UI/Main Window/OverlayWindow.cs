using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OverlayWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_windowTitle;
    [SerializeField] private GameObject[] m_mainComps;
    [SerializeField] private FadingMenu[] m_otherWindows;
    [SerializeField] private SubMenuHandler m_submenuHandler;
    private bool m_isPlayerDead;
    public static event Action<bool> OnOpen;
    public static event Action<bool> OnOpenWindow;
    public static event Action<bool> OnCloseWindow;
    public const float m_waitTime = 0.8f;


    private void OnEnable()
    {
        // Put things here.
    }

    private void OnDisable()
    {
        // Put things here.
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !MenuTransitionLock.IsLocked)
            OpenOverlayWindowWrap();
    }

    private void PlayerDied() => m_isPlayerDead = true;

    private void OpenOverlayWindowWrap() => StartCoroutine(OpenOverlayWindow());

    private IEnumerator OpenOverlayWindow()
    {
        MenuTransitionLock.LockFor(m_waitTime);
        yield return null;
        ErrorLogger.DebugLog("reached openoverlay");
        bool shouldActivate = !m_mainComps[0].activeInHierarchy;
        yield return null;
        if (m_submenuHandler.ActiveMenus.Count == 0 && shouldActivate)
            OnOpenWindow?.Invoke(true);
        else if (m_submenuHandler.ActiveMenus.Count == 0 && !shouldActivate)
            OnCloseWindow?.Invoke(false);
        EnableOrDisable(shouldActivate);
        m_windowTitle.text = HandleTitle();
    }

    private void EnableOrDisable(bool shouldActivate)
    {
        foreach (GameObject obj in m_mainComps)
        {
            if (shouldActivate)
            {
                obj.SetActive(true);
                if (obj.activeSelf) { obj.SetActive(false); obj.SetActive(true); }
            }
            else if (!m_isPlayerDead &&
                    !SceneController.Instance.IsInMenu)
            {
                    obj.GetComponent<FadingMenu>().Disable();
            }
        }
        foreach (FadingMenu window in m_otherWindows) window.Disable();
        OnOpen?.Invoke(shouldActivate);
    }

    private string HandleTitle()
    {
        // Put here ifs and else ifs when there are other things that can make this open.
        if (m_isPlayerDead)
        {
            return "You Died.";
        }
        else if (Time.timeScale == 0)
        {
            return "Game Paused.";
        }
        else if (SceneController.Instance.IsInMenu)
        {
            return "GAME NAME";
        }
        return "Game Paused.";
    }
}