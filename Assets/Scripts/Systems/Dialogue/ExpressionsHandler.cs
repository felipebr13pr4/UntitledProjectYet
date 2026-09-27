using UnityEngine;
using UnityEngine.UI;

public class ExpressionsHandler : MonoBehaviour
{
    [SerializeField] private Image m_portrait;
    [SerializeField] private Image m_fullBody;
    [SerializeField] private ExpressionSprites[] m_sprites;

    private void OnEnable()
    {
        TextBox.OnExpression += HandleExpression;
    }

    private void OnDisable()
    {
        TextBox.OnExpression -= HandleExpression;
    }

    private void OnValidate()
    {
        for (int i = 0;  i < m_sprites.Length; i++)
        {
            m_sprites[i].Name = m_sprites[i].Expression.ToString();
        }
    }

    public void HandleExpression(LineExpression expression)
    {
        m_portrait.enabled = true;
        m_fullBody.enabled = expression.HasFullBody;

        bool found = false;

        if (expression.AssignedExpression == Expressions.None)
        {
            m_portrait.enabled = false;
            m_fullBody.enabled = false;
            return;
        }

        foreach (ExpressionSprites data in m_sprites)
        {
            if (expression.AssignedExpression == data.Expression)
            {
                m_portrait.sprite = data.Portrait;
                m_fullBody.sprite = data.FullBody;
                found = true;
            }
        }

        if (!found)
            ErrorLogger.LogError("Something went wrong, a expression not detected has been received in the handler. Make sure theres a data for each expression.");
    }
}
