using System;
using UnityEngine;

public class PlayerDeathAnim : MonoBehaviour
{
    [SerializeField] private GameObject m_sprite;
    [SerializeField] private SpriteRenderer m_deathSprite;
    [SerializeField] private Animator m_animator;
    [SerializeField] private DeathClip[] m_deathClips;

    [Serializable]
    public struct DeathClip
    {
        [SerializeField] private string m_clipName;
        public readonly string ClipName => m_clipName;
        [SerializeField] private DeathType m_type;
        public readonly DeathType Type => m_type;
    }

    private void OnEnable()
    {
        DeathHandler.OnDeathType += Execute;
    }

    private void OnDisable()
    {
        DeathHandler.OnDeathType -= Execute;
    }

    private void Execute(DeathType type)
    {
        if (type == DeathType.None)
            return;
        m_sprite.SetActive(false);
        m_deathSprite.enabled = true;

        for (int i = 0; i < m_deathClips.Length; i++)
        {
            if (m_deathClips[i].Type == type)
            {
                if (m_animator.HasState(0, Animator.StringToHash(m_deathClips[i].ClipName)))
                {
                    m_animator.Play(m_deathClips[i].ClipName);
                }
                else
                {
                    ErrorLogger.LogError("State received in Player Death Anim but the state/clip name is either missing or written wrong.");
                }
                return;
            }
        }

        ErrorLogger.LogError("Death type enum received in Player Death Anim but it did not match any of the already existing types inside the struct, make sure the struct has no duplicates and is up to date with the death types enum.");
    }

}
