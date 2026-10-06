using System.IO;
using UnityEngine;
using static StreamingAssetFolders;

[CreateAssetMenu(fileName = "New Phone Node", menuName = "Dialogue/Phone")]
public class PhoneNode : NodeWithNext
{
    [SerializeField] private PhoneChoiceType m_type;
    public PhoneChoiceType Type => m_type;
    [SerializeField] private PhoneData m_data;
    public PhoneData Data { get => m_data; set => m_data = value; }
    private int m_warningIndex;

    private void OnValidate()
    {
        m_warningIndex++;
        if (m_warningIndex > 2)
        {
            m_warningIndex = 0;
            
            for (int i = 0; i < m_data.ImagesFiles.Length; i++)
                if (!File.Exists(Path.Combine(Application.streamingAssetsPath, ImagesPath, m_data.ImagesFiles[i].Path)))
                    ErrorLogger.LogWarning($"Warning! A image file path in a phone data has not been detected, make sure all image paths are named correctly and exactly as the file name (make sure it has the extension name included, .png). I am: {name} (phone node), and the infriging line is inside Image Files, at element: {i}.");
            
            for (int i = 0; i < m_data.AudiosFiles.Length; i++)
                if (!File.Exists(Path.Combine(Application.streamingAssetsPath, AudiosPath, m_data.AudiosFiles[i].Path)))
                    ErrorLogger.LogWarning($"Warning! A audio file path in a phone data has not been detected, make sure all audio paths are named correctly and exactly as the file name (make sure it has the extension name included, .mp3). I am: {name} (phone node), and the infriging line is inside Audio Files, at element: {i}.");
        }
    }
}