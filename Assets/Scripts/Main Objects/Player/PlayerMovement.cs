using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int XLastMovingHash = Animator.StringToHash("XLastMoving");
    private static readonly int YLastMovingHash = Animator.StringToHash("YLastMoving");
    private static readonly int YMovingHash = Animator.StringToHash("YMoving");
    private static readonly int XMovingHash = Animator.StringToHash("XMoving");
    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    [SerializeField] private InputActionReference m_moveAction;
    [SerializeField] private Rigidbody2D m_rb2d;
    [SerializeField] private Animator m_animator;
    [SerializeField] private SpriteRenderer m_spriteRen;
    public Rigidbody2D Rb2d => m_rb2d;
    [SerializeField] private float m_speed = 4;
    public float Speed => m_speed;
    private Vector2 m_moveDir;
    public Vector2 OutsideMoveDir
    {
        set
        {
            m_moveDir = value;
            m_animator.SetBool(IsMovingHash, m_moveDir != Vector2.zero);
            m_animator.SetInteger(YMovingHash, Mathf.RoundToInt(m_moveDir.y));
            m_animator.SetInteger(XMovingHash, Mathf.RoundToInt(m_moveDir.x));
            if (m_moveDir != Vector2.zero)
            {
                m_lastDir = m_moveDir;
                m_animator.SetInteger(YLastMovingHash, Mathf.RoundToInt(m_lastDir.y));
                m_animator.SetInteger(XLastMovingHash, Mathf.RoundToInt(m_lastDir.x));
                SetFlip();
            }
        }
    }
    private Vector2 m_lastDir = Vector2.up;
    public Vector2 LastDir => m_lastDir;
    private float m_pressTimer = 0;
    [SerializeField] private float m_defaultPressTime = 0.1f;
    private bool m_isHolding;

    private void Update()
    {
        m_moveDir = m_moveAction.action.ReadValue<Vector2>();

        if (m_moveDir == Vector2.zero)
        {
            m_isHolding = false;
            m_pressTimer = 0f;
        }
        else
        {
            m_pressTimer += Time.deltaTime;
            if (m_pressTimer >= m_defaultPressTime)
                m_isHolding = true;
        }

        m_animator.SetBool(IsMovingHash, m_isHolding);
        m_animator.SetInteger(YMovingHash, Mathf.RoundToInt(m_moveDir.y));
        m_animator.SetInteger(XMovingHash, Mathf.RoundToInt(m_moveDir.x));

        if (m_moveDir != Vector2.zero)
        {
            m_lastDir = m_moveDir;
            m_animator.SetInteger(YLastMovingHash, Mathf.RoundToInt(m_lastDir.y));
            m_animator.SetInteger(XLastMovingHash, Mathf.RoundToInt(m_lastDir.x));
            SetFlip();
        }
    }

    private void FixedUpdate()
    {
        m_rb2d.linearVelocity = m_isHolding ? m_moveDir * m_speed : Vector2.zero;
    }

    private void OnDisable()
    {
        m_moveDir = Vector2.zero;
        m_rb2d.linearVelocity = Vector2.zero;
    }

    private void SetFlip()
    {
        m_spriteRen.flipX = m_lastDir.x < 0;
        ErrorLogger.LogVar(m_lastDir.x < 0, nameof(m_lastDir.x));
    }
}
