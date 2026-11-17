using UnityEngine;

public class DialogueLine : MonoBehaviour
{
    [SerializeField] private TextBubble dialogueBox;
    [SerializeField] private TextBubble speakerBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Setup(string speaker, string text)
    {
        dialogueBox.Setup(text);
        speakerBox.Setup(speaker);
    }

    public void FadeDown()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0.5f;
    }
}
