using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLine : MonoBehaviour
{
    [SerializeField] private TextBubble dialogueBox;
    [SerializeField] private TextBubble speakerBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Setup(string speaker, string text, int posIndex)
    {
        dialogueBox.Setup(text);
        speakerBox.Setup(speaker);

        Debug.Log("PosIndeex " + posIndex);
        if(posIndex == 0)
        {
            gameObject.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.LowerRight;
        }
        else if (posIndex == 1)
        {
            gameObject.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.LowerLeft;
        }
    }

    public void FadeDown()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0.5f;
    }
}
