using System.Collections;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    [SerializeField] private float m_time = 0.3f;
    private CameraData m_data;
    private Vector3 m_velocity;
    private Coroutine m_coroutine;

    private void OnEnable()
    {
        CameraTrigger.OnTriggerEnter += UpdateState;
    }

    private void OnDisable()
    {
        CameraTrigger.OnTriggerEnter -= UpdateState;
    }

    private void UpdateState(CameraData data)
    {
        if (m_coroutine != null) StopCoroutine(m_coroutine);
        m_data = data;
        if (m_data.ShouldPosition)
        {
            m_coroutine = StartCoroutine(GoToPos());
        }
    }

    private void LateUpdate()
    {
        if (m_data.ShouldPosition) return;

        Vector3 target = Vector3.SmoothDamp(transform.position, m_player.transform.position,
            ref m_velocity, m_time);

        if (m_data.IsXLocked)
            target.x = Mathf.Clamp(target.x, m_data.XBoundary.Start, m_data.XBoundary.End);

        if (m_data.IsYLocked)
            target.y = Mathf.Clamp(target.y, m_data.YBoundary.Start, m_data.YBoundary.End);

        target.z = -10;
        transform.position = target;
    }

    private IEnumerator GoToPos()
    {
        while (Vector3.Distance(transform.position, m_data.Position) > 0.01f)
        {
            Vector3 target = Vector3.SmoothDamp(transform.position, m_data.Position,
                ref m_velocity, m_time / 2);
            target.z = -10;
            transform.position = target;
            yield return null;
        }
        transform.position = new Vector3(m_data.Position.x, m_data.Position.y, -10);
    }
}
