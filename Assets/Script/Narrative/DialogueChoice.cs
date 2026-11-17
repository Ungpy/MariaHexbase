using UnityEngine;

public class DialogueChoice : MonoBehaviour
{
    [SerializeField] private TextBubble textbox;
    private DialogueData nextNode = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(DialogueChoiceData choice)
    {
        textbox.Setup(choice.text);
        this.nextNode = choice.nextNode;
    }

    public void BTNClick()
    {
        if (nextNode != null)
        {
            DialogueManager.dialogueManager.RunDialogue(nextNode);
            DialogueManager.dialogueManager.ClearChoice();
        }
        else
        {
            Debug.Log("Nextnode is null");
        }
    }

}
