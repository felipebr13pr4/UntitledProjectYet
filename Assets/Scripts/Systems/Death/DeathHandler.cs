using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private DeathData[] m_data;
    public static event Action OnDeathLate;
    public static event Action OnDeath;
    public static event Action<PhoneNode> OnDeathImage;
    public static event Action OnDeathVideo;
    private bool m_finishedVideo;
    private readonly string[] m_randoms = new string[]
    {
        "WHY WHY WHY WHY WHY WHY WHY WHY WHY WHY WHY WHY WHY WHY",
        "YOU YOU YOU YOU YOU YOU YOU YOU YOU YOU YOU YOU YOU YOU",
        "IM IM IM IM IM IM IM IM IM IM IM IM IM IM IM IM IM IM IM",
        "SCARED SCARED SCARED SCARED SCARED SCARED SCARED SCARED SCARED",
        "HELP ME HELP ME HELP ME HELP ME HELP ME HELP ME HELP ME HELP ME HELP ME HELP ME",
        "SOMEONE SOMEONE SOMEONE SOMEONE SOMEONE SOMEONE SOMEONE SOMEONE SOMEONE SOMEONE",
        "WHY IS IT SO DARK WHY IS IT SO DARK WHY IS IT SO DARK WHY IS IT SO DARK WHY IS IT SO DARK WHY IS IT SO DARK",
        "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
        "please please please please please please please please please please please please please please",
        "ANYONE ANYONE ANYONE ANYONE ANYONE ANYONE ANYONE ANYONE ANYONE ANYONE ANYONE ANYONE ANYONE ANYONE",
        "MY THOUGHTS MY THOUGHTS MY THOUGHTS MY THOUGHTS MY THOUGHTS MY THOUGHTS MY THOUGHTS MY THOUGHTS MY THOUGHTS",
        "I CANT SEE I CANT SEE I CANT SEE I CANT SEE I CANT SEE I CANT SEE I CANT SEE I CANT SEE I CANT SEE I CANT SEE I CANT SEE",
        "01001000 01000101 01001100 01010000 00100000 01001101 01000101",
        "I CANT HEAR I CANT HEAR I CANT HEAR I CANT HEAR I CANT HEAR I CANT HEAR I CANT HEAR I CANT HEAR I CANT HEAR I CANT HEAR",
        "I CANT SCREAM I CANT SCREAM I CANT SCREAM I CANT SCREAM I CANT SCREAM I CANT SCREAM I CANT SCREAM I CANT SCREAM I CANT SCREAM",
        "Stop it please Stop it please Stop it please Stop it please Stop it please Stop it please Stop it please Stop it please",
        "IT HURTS IT HURTS IT HURTS IT HURTS IT HURTS IT HURTS IT HURTS IT HURTS IT HURTS IT HURTS IT HURTS IT HURTS",
        "I CANT BREATH I CANT BREATH I CANT BREATH I CANT BREATH I CANT BREATH I CANT BREATH I CANT BREATH I CANT BREATH I CANT BREATH",
        "WHY DO I FEEL NOTHING WHY DO I FEEL NOTHING WHY DO I FEEL NOTHING WHY DO I FEEL NOTHING WHY DO I FEEL NOTHING WHY DO I FEEL NOTHING",
        "I FEEL SO COLD I FEEL SO COLD I FEEL SO COLD I FEEL SO COLD I FEEL SO COLD I FEEL SO COLD I FEEL SO COLD I FEEL SO COLD",
        "HOME HOME HOME HOME HOME HOME HOME HOME HOME HOME HOME HOME HOME HOME HOME HOME HOME HOME",
        "ESCAPE ESCAPE ESCAPE ESCAPE ESCAPE ESCAPE ESCAPE ESCAPE ESCAPE ESCAPE ESCAPE",
        "MY LIFE MY LIFE MY LIFE MY LIFE MY LIFE MY LIFE MY LIFE MY LIFE MY LIFE MY LIFE MY LIFE MY LIFE",
        "ffhtHnyzsqpMvuytsijvCupzsbyhrFemhnuczAzuxuyalxeIgdw",
        "tczxEyreufyEeuylwgihAgfpvxiswEkaykallNizdupmcbbNdr",
        "safbLqzdqodjhpvhkusaNwlgbsiqaEqjrumfiYltnlqsyskGyr",
        "tthzPciocmsIxhyxpprjTuzfdhitpLkwxcgmbTyfhcmmovnjvg",
        "yjsuppdrnfaipavyccvizglmbkrozkznhlrslHbggkzrvijprb"
    };

    private void OnEnable()
    {
        DeathTrigger.OnTriggerEnter += DeathWrap;
        DeathOverlay.OnVideoFinish += Finished;
    }

    private void OnDisable()
    {
        DeathTrigger.OnTriggerEnter -= DeathWrap;
        DeathOverlay.OnVideoFinish -= Finished;
    }

    private void OnValidate()
    {
        if (m_data.Length > 0)
            for (int i = 0; i < m_data.Length; i++)
            {
                if (m_data[i].Node != null)
                    m_data[i].Node.Data =
                        new(m_data[i].Node.Data.ImagesFiles,
                        m_data[i].Node.Data.AudiosFiles,
                        m_data[i].Node.Data.TextFiles,
                        "HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP_HELP");
            }
    }

    private void DeathWrap(float delay, DeathType type) => StartCoroutine(Death(delay, type));
    private void Finished() => m_finishedVideo = true;

    private IEnumerator Death(float delay, DeathType type)
    {
        OnDeath?.Invoke();
        yield return new WaitForSecondsRealtime(delay);
        OnDeathVideo?.Invoke();
        while (!m_finishedVideo)
        {
            yield return null;
        }

        int imageIndex = -1;
        for (int i = 0; i < m_data.Length; i++)
            if (m_data[i].Type == type)
                imageIndex = i;

        if (imageIndex < 0)
            ErrorLogger.LogError("Something went wrong, a death type has been received in the death handler but it does not match any of the types death handler has, make sure all types are up to date or that all of the elements have their enums correctly assigned.");
        else if (type != DeathType.None)
        {
            PhoneNode copy = Instantiate(m_data[imageIndex].Node);

            string stamp = DateTime.Now.ToString("HH;mm;ss");

            copy.Data.ImagesFiles[0] =
                new(copy.Data.ImagesFiles[0].Name + " " + stamp + " " + m_randoms[Random.Range(0, m_randoms.Length)],
                copy.Data.ImagesFiles[0].Path);

            OnDeathImage?.Invoke(copy);
        }

        OnDeathLate?.Invoke();
    }
}
