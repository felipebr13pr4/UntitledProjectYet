using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSilhouette : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_playerSpriteRen;
    [SerializeField] private SpriteRenderer m_visibilitySpriteRen;

    private void LateUpdate()
    {
        m_visibilitySpriteRen.sprite = m_playerSpriteRen.sprite;
        m_visibilitySpriteRen.flipX = m_playerSpriteRen.flipX;
    }
}
