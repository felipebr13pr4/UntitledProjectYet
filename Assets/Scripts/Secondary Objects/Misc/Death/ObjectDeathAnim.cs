using UnityEngine;

public class ObjectDeathAnim : MonoBehaviour
{
    [SerializeField] private DeathType m_type;
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_state;

    private void OnEnable() => DeathHandler.OnDeathType += Execute;   

    private void OnDisable() => DeathHandler.OnDeathType -= Execute;

    private void Execute(DeathType type)
    {
        if (type == DeathType.None) return;
        else if (type == m_type)
        {
            if (m_animator.HasState(0, Animator.StringToHash(m_state)))
                m_animator.Play(m_state);
            else
                ErrorLogger.LogError("State received in Object Death Anim but the state name is either missing or written wrong.");
        }
    }
}