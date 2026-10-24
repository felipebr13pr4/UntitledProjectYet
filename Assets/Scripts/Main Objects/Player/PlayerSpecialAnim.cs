using UnityEngine;

public class PlayerSpecialAnim : MonoBehaviour
{
    [SerializeField] private GameObject m_sprite;
    [SerializeField] private SpriteRenderer m_specialSprite;
    [SerializeField] private Animator m_animator;

    private void OnEnable()
    {
        DialogueRouter.OnAnimationNode += Execute;
        DialogueRouter.OnActivation += DisableCheck;
    }

    private void OnDisable()
    {
        DialogueRouter.OnAnimationNode -= Execute;
        DialogueRouter.OnActivation -= DisableCheck;
    }

    private void Execute(AnimationNode node)
    {
        if (node.Disable)
        {
            Disable();
            return;
        }

        m_sprite.SetActive(false);
        m_specialSprite.enabled = true;

        m_animator.Play(Animator.StringToHash(node.State));

        if (!node.Keep)
            StartCoroutine(
                SpecialFunctions.DelayMethodSecReal(
                    Disable,
                    node.Time > 0 ?
                    node.Time : m_animator.GetCurrentAnimatorStateInfo(0).length));
    }

    private void DisableCheck(bool dialogueState)
    {
        if (!dialogueState)
            Disable();
    }

    private void Disable()
    {
        m_sprite.SetActive(true);
        m_specialSprite.enabled = false;
    }
}
