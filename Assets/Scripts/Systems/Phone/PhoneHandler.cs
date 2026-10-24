using System;
using System.IO;
using UnityEngine;
using static StreamingAssetFolders;

public class PhoneHandler : MonoBehaviour
{
    private string m_godPhonePath;
    private string m_targetPath;

    private void OnEnable()
    {
        DialogueRouter.OnPhoneNode += Execute;
        DeathHandler.OnDeathImage += Execute;
    }

    private void Start()
    {
        m_godPhonePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GodPhone");
        Directory.CreateDirectory(m_godPhonePath);
    }

    private void OnDisable()
    {
        DialogueRouter.OnPhoneNode -= Execute;
        DeathHandler.OnDeathImage -= Execute;
    }

    private void Execute(PhoneNode node)
    {
        if (node.Data.SubFolder != "" && node.Data.SubFolder.Length > 0)
        {
            m_targetPath = Path.Combine(m_godPhonePath, node.Data.SubFolder);
            Directory.CreateDirectory(m_targetPath);
        }
        else
        {
            m_targetPath = m_godPhonePath;
        }
        if (node.Data.ImagesFiles != null && node.Data.ImagesFiles.Length > 0)
        {
            foreach (BasicFile spriteFile in node.Data.ImagesFiles)
            {
                File.Copy(
                    Path.Combine(
                        Application.streamingAssetsPath,
                        ImagesPath,
                        spriteFile.Path),

                    Path.Combine(
                        m_targetPath,
                        spriteFile.Name + Path.GetExtension(spriteFile.Path)),

                    true);
            }
        }
        if (node.Data.AudiosFiles != null && node.Data.AudiosFiles.Length > 0)
        {
            foreach (BasicFile audioFile in node.Data.AudiosFiles)
            {
                File.Copy(
                    Path.Combine(
                        Application.streamingAssetsPath,
                        AudiosPath,
                        audioFile.Path),

                    Path.Combine(
                        m_targetPath,
                        audioFile.Name + Path.GetExtension(audioFile.Path)),

                    true);
            }
        }
        if (node.Data.TextFiles != null && node.Data.TextFiles.Length > 0)
        {
            foreach (TextFile textFile in node.Data.TextFiles)
            {
                File.WriteAllText(Path.Combine(m_targetPath, textFile.Name + ".txt"), textFile.Text);
            }
        }
        
    }
}
