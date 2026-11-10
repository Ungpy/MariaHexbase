using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] GameObject dialogueContainer;
    [SerializeField] TMP_Text dialogueText;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private RectMask2D rectMask2D;
    private VerticalLayoutGroup VerticalLayout;
    private DialogueData nextNode = null;


    Vector2 offset;
    Vector2 preferredSize;
    Vector2 startSize;

    private Coroutine sizedUpCoroutine;

    private void Awake()
    {
        //this.gameObject.SetActive(false);
        canvasGroup = dialogueContainer.GetComponent<CanvasGroup>();
        rectTransform = dialogueContainer.GetComponent<RectTransform>();
        VerticalLayout = dialogueContainer.GetComponent<VerticalLayoutGroup>();
        rectMask2D = dialogueContainer.GetComponent<RectMask2D>();
        //canvasGroup.alpha = 0f;
        //canvasGroup.alpha = 1f;

    }

    public IEnumerator SizedUp(string text)
    {
        float elapsed = 0f;
        float resizeDuration = 0.1f;
        int animationStyle = 1;

        if (animationStyle == 0)
        {
            startSize = rectTransform.sizeDelta;
        }
        else if (animationStyle == 1)
        {
            startSize = new Vector2(preferredSize.x, 0f);
        }
        else
        {
            startSize = new Vector2(100f, 100f);
        }

        while (elapsed < resizeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / resizeDuration;

            t = Mathf.SmoothStep(0f, 1f, t);
            rectTransform.sizeDelta = Vector2.Lerp(startSize, preferredSize, t);

            yield return null; // รอเฟรมถัดไป
        }

        rectTransform.sizeDelta = preferredSize;

        StartCoroutine(TextAppear(preferredSize));

        sizedUpCoroutine = null;
    }

    public IEnumerator TextAppear(Vector2 preferredSize)
    {
        //hidden whole box
        //rectMask2D.padding = new Vector4(0f, 0f, preferredSize.x, 0f);
        Vector4 startPadding = new Vector4(0f, 0f, preferredSize.x, 0f);
        Vector4 preferredPadding = new Vector4(0f, 0f, 0f, 0f);

        float elapsed = 0f;
        float resizeDuration = 0.2f;

        while (elapsed < resizeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / resizeDuration;

            t = Mathf.SmoothStep(0f, 1f, t);
            rectMask2D.padding = Vector4.Lerp(startPadding, preferredPadding, t);

            yield return null; // รอเฟรมถัดไป
        }
        rectMask2D.padding = preferredPadding;
    }




    public void Setup(string speaker, string text)
    {
        //set up size of element (dialogue bubble)
        Vector2 tmpSize = dialogueText.GetPreferredValues(text); ;
        offset = new Vector2(VerticalLayout.padding.left + VerticalLayout.padding.right, VerticalLayout.padding.top + VerticalLayout.padding.bottom); ;
        preferredSize = tmpSize + offset;
        startSize = new Vector2(0f, 0f);

        dialogueText.text = text;
        rectMask2D.padding = new Vector4(0f, 0f, preferredSize.x, 0f);

        if (sizedUpCoroutine != null)
        {
            StopCoroutine(sizedUpCoroutine);
        }
        sizedUpCoroutine = StartCoroutine(SizedUp(text));

        //StartCoroutine(RebuildLayoutRoutine());
    }

    public void ChoiceSetup(DialogueChoiceData choice)
    {
        Setup("", choice.text);
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

        IEnumerator RebuildLayoutRoutine()
    {
        dialogueText.ForceMeshUpdate();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        yield return new WaitForEndOfFrame();

        canvasGroup.alpha = 1f;


    }
}
