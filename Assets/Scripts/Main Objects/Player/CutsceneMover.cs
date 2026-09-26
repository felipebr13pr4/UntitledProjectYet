using System;
using System.Collections;
using UnityEngine;

public class CutsceneMover : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_playerMovement;
    public static event Action<bool> OnCutscene;
    public static event Action<Node> OnNextNode;
    private float Speed { get => m_playerMovement.Speed; }
    private CutsceneData m_data;
    private bool m_isXMoving;
    private bool m_isYMoving;
    private Vector2 m_dir;

#if UNITY_EDITOR
    [SerializeField] private CutsceneNode m_testNode;
    [ContextMenu("Test cutscene testnode")]
    private void Test() => ExecuteCutsceneWrap(m_testNode);
#endif

    private void OnEnable()
    {
        TextBox.OnCutsceneNode += ExecuteCutsceneWrap;
    }

    private void OnDisable()
    {
        TextBox.OnCutsceneNode -= ExecuteCutsceneWrap;
    }

    private void ExecuteCutsceneWrap(CutsceneNode node)
        => StartCoroutine(ExecuteCutscene(node));


    private IEnumerator ExecuteCutscene(CutsceneNode node)
    {
        float epsilon = 0.2f;
        OnCutscene?.Invoke(true);
        foreach (CutsceneData data in node.Data)
        {
            yield return null;
            m_data = data;
            m_dir = m_data.Pos - (Vector2)transform.position;
            if (MathF.Abs(m_dir.x) > epsilon)
                m_isXMoving = true;

            if (MathF.Abs(m_dir.y) > epsilon)
                m_isYMoving = true;

            while (m_isXMoving || m_isYMoving)
            {
                yield return null;
            }
            gameObject.transform.position = m_data.Pos;
            m_playerMovement.Rb2d.linearVelocity = Vector2.zero;
        }
        m_isXMoving = false;
        m_isYMoving = false;
        OnCutscene?.Invoke(false);
        if (node.Next != null)
            OnNextNode?.Invoke(node.Next);
    }

    private void FixedUpdate()
    {
        if (m_isXMoving){
        if (m_dir.x > 0)
        {
            if (transform.position.x <= m_data.Pos.x)
            {
                m_playerMovement.Rb2d.linearVelocityX = Vector2.right.x * Speed;
            }
            else
            {
                m_playerMovement.Rb2d.linearVelocityX = 0;
                m_isXMoving = false;
            }
        }
            if (m_dir.x < 0)
            {
                if (transform.position.x >= m_data.Pos.x)
                {
                    m_playerMovement.Rb2d.linearVelocityX = Vector2.left.x * Speed;
                }
                else
                {
                    m_playerMovement.Rb2d.linearVelocityX = 0;
                    m_isXMoving = false;
                }
            }
        }
        if (m_isYMoving){
        if (m_dir.y > 0)
            {
            if (transform.position.y <= m_data.Pos.y)
            {
                m_playerMovement.Rb2d.linearVelocityY = Vector2.up.y * Speed;
            }
            else
            {
                m_playerMovement.Rb2d.linearVelocityY = 0;
                m_isYMoving = false;
            }
        }
            if (m_dir.y < 0)
            {
                if (transform.position.y >= m_data.Pos.y)
                {
                    m_playerMovement.Rb2d.linearVelocityY = Vector2.down.y * Speed;
                }
                else
                {
                    m_playerMovement.Rb2d.linearVelocityY = 0;
                    m_isYMoving = false;
                }
            }
        }
    }
}
