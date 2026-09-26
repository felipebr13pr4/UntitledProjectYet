using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference m_moveAction;
    [SerializeField] private Rigidbody2D m_rb2d;
    public Rigidbody2D Rb2d => m_rb2d;
    [SerializeField] private float m_speed = 4;
    public float Speed => m_speed;
    private Vector2 m_moveDir;
    private Vector2 m_lastDir = Vector2.up;
    public Vector2 LastDir => m_lastDir;

    private void Update()
    {
        m_moveDir = m_moveAction.action.ReadValue<Vector2>();
        if (m_moveDir != Vector2.zero) m_lastDir = m_moveDir;
    }

    private void FixedUpdate()
    {
        m_rb2d.linearVelocity = m_moveDir * m_speed;
    }

    private void OnDisable()
    {
        m_moveDir = Vector2.zero;
        m_rb2d.linearVelocity = Vector2.zero;
    }
}
