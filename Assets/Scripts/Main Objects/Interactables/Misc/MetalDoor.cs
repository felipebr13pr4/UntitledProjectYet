using UnityEngine;

public class MetalDoor : ToggleableObject
{
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    private static readonly int AnimInteracted = Animator.StringToHash("Interacted");
    [SerializeField] private Animator m_animator; 
    [SerializeField] private SpriteRenderer m_spriteRen;
    [SerializeField] private BoxCollider2D m_boxCollider2D;

    protected override void HandleState()
    {
        m_animator.SetBool(IsOpen, m_toggledState);
        m_animator.SetTrigger(AnimInteracted);
        m_boxCollider2D.enabled = m_toggledState;
    }
}