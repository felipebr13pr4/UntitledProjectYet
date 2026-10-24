using UnityEngine;

[CreateAssetMenu(fileName = "New Cutscene Node", menuName = "Dialogue/Cutscene")]
public class CutsceneNode : NodeWithNext
{
    [SerializeField] private CutsceneData[] m_data;
    public CutsceneData[] Data => m_data;
}