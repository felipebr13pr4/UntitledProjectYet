using System;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{
    [SerializeField] private AudioData[] m_audioData = new AudioData[4];

    public void ActivateSound(params int[] indices)
    {
        for (int i = 0; i < indices.Length; i++)
            AudioController.Instance.PlayAudio(m_audioData[indices[i]]);
    }

    public void ActivateStoppableSound(params int[] indices)
    {
        for (int i = 0; i < indices.Length; i++)
            AudioController.Instance.PlayStoppableAudio(m_audioData[indices[i]]);
    }

    private void OnValidate()
    {
        for (int i = 0; i < m_audioData.Length; i++) 
        {
            float pitch;
            float min;
            float max;
            AudioData defaultData = new(m_audioData[i].Clip);
            if (!m_audioData[i].IsPitchRandom)
            {
                pitch = m_audioData[i].Pitch;
                if (m_audioData[i].Pitch == 0) pitch = defaultData.Pitch;
                m_audioData[i] = new(m_audioData[i].Clip, pitch,
                    m_audioData[i].IsPitchRandom, m_audioData[i].Min, m_audioData[i].Max);
            }
            else
            {
                min = m_audioData[i].Min;
                max = m_audioData[i].Max;
                if (m_audioData[i].Min == 0) min = defaultData.Min;
                if (m_audioData[i].Max == 0) max = defaultData.Max;
                m_audioData[i] = new(m_audioData[i].Clip, m_audioData[i].Pitch,
                    m_audioData[i].IsPitchRandom, min, max);
            }
        }
    }
}