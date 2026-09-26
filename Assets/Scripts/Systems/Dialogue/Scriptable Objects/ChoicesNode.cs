using UnityEngine;

[CreateAssetMenu(fileName = "New Choice Node", menuName = "Dialogue/Choice")]
public class ChoicesNode : Node
{
    [SerializeField] private Option[] m_options = new Option[1];
    public Option[] Options => m_options;

    private void OnEnable()
    {
        foreach (Option option in m_options)
        {
            if (option.Node == null)
                ErrorLogger.LogError($"Null next node detected in a reference. It is option: '{(option.Name == "" ? "No option named detected" : option.Name)}', inside '{name}'");
        }
    }

    private void OnValidate()
    {
        if (m_options.Length == 0)
            m_options = new Option[1];
    }
}