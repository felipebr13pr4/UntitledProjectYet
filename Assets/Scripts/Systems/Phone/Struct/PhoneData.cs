using System;
using UnityEngine;

[Serializable]
public struct PhoneData
{
    [SerializeField] private BasicFile[] m_imagesFiles;
    public readonly BasicFile[] ImagesFiles => m_imagesFiles;
    [SerializeField] private BasicFile[] m_audiosFiles;
    public readonly BasicFile[] AudiosFiles => m_audiosFiles;
    [SerializeField] private TextFile[] m_textFile;
    public readonly TextFile[] TextFiles => m_textFile;
    [SerializeField] private string m_subFolder;
    public string SubFolder { readonly get => m_subFolder; set => m_subFolder = value; }

    public PhoneData(BasicFile[] imagesFiles, BasicFile[] audiosFiles, TextFile[] textFiles, string subFolder)
    {
        m_imagesFiles = imagesFiles;
        m_audiosFiles = audiosFiles;
        m_textFile = textFiles;
        m_subFolder = subFolder;
    }
}