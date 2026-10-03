using UnityEngine;

public class TextBoxHandler : MonoBehaviour
{
    [SerializeField] private DialogueRouter m_router;

    private void OnEnable()
    {
        DialogueableObject.OnInteract += Execute;
        CutsceneMover.OnNextNode += Execute;
        PlayerInteraction.OnCheckEvent += Execute;
    }

    private void OnDisable()
    {
        DialogueableObject.OnInteract -= Execute;
        CutsceneMover.OnNextNode -= Execute;
        PlayerInteraction.OnCheckEvent -= Execute;
    }

    private void Execute(Node node)
    {
        m_router.Initialize(node);
    }
}