using UnityEngine;

public class DialogueLine : MonoBehaviour
{
    [SerializeField] private TextBubble dialogueBox;
    [SerializeField] private TextBubble speakerBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Setup(string text, string speaker)
    {
        dialogueBox.Setup(text);
        speakerBox.Setup(speaker);
    }


}
