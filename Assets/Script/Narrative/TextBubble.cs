using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

public class TextBubble : MonoBehaviour
{
    //[SerializeField] GameObject dialogueContainer;
    [SerializeField] TMP_Text dialogueText;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private RectMask2D rectMask2D;
    private VerticalLayoutGroup VerticalLayout;
    //private DialogueData nextNode = null;
    [SerializeField] private int sizedUp_style = 1;
    [SerializeField] private int textAppear_style = 1;


    Vector2 offset;
    Vector2 preferredSize;
    Vector2 startSize;

    private Coroutine sizedUpCoroutine;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        VerticalLayout = gameObject.GetComponent<VerticalLayoutGroup>();
        rectMask2D = gameObject.GetComponent<RectMask2D>();

    }

    [ContextMenu("FitText")]
    private void SetSize()
    {
        //-----------------------Editor function---------------------------------------------------------------5814865147465srgpjewjpgjp
        //Awake();
        //Setup(dialogueText.text);

        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        VerticalLayout = gameObject.GetComponent<VerticalLayoutGroup>();
        rectMask2D = gameObject.GetComponent<RectMask2D>();

        //Vector2 tmpSize = dialogueText.GetPreferredValues(dialogueText.text, dialogueText.GetComponent<LayoutElement>().maxWidth, Mathf.Infinity);
        Vector2 tmpSize =  dialogueText.GetPreferredValues();
        offset = new Vector2(VerticalLayout.padding.left + VerticalLayout.padding.right, VerticalLayout.padding.top + VerticalLayout.padding.bottom);
        preferredSize = tmpSize + offset;


        rectTransform.sizeDelta = preferredSize;
        rectMask2D.padding = new Vector4(0f, 0f, 0f, 0f);
    }

    public IEnumerator SizedUp(string text)
    {
        float elapsed = 0f;
        float resizeDuration = 0.2f;
        

        if (sizedUp_style == 0) //intance
        {
            startSize = rectTransform.sizeDelta;
        }
        else if (sizedUp_style == 1) //pop up
        {
            startSize = new Vector2(preferredSize.x, 0f);
        }
        else if (sizedUp_style == 2) //pop up
        {
            startSize = new Vector2(0f, preferredSize.y);
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
        if(textAppear_style == 0)
        {
            resizeDuration = 0f;
        } 
        else if (textAppear_style == 1)
        {
            resizeDuration = 0.2f;
        }
        else
        {

        }



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




    public void Setup(string text)
    {
        //set up size of element (dialogue bubble)
        Vector2 tmpSize = dialogueText.GetPreferredValues(text, dialogueText.GetComponent<LayoutElement>().maxWidth, Mathf.Infinity);
        //Vector2 tmpSize = dialogueText.GetPreferredValues(text);
        offset = new Vector2(VerticalLayout.padding.left + VerticalLayout.padding.right, VerticalLayout.padding.top + VerticalLayout.padding.bottom); ;
        preferredSize = tmpSize + offset;
        startSize = new Vector2(0f, 0f);

        dialogueText.text = text;
        rectMask2D.padding = new Vector4(0f, 0f, preferredSize.x, 0f);
        //Done setup

        //Start Sizing up
        if (sizedUpCoroutine != null)
        {
            StopCoroutine(sizedUpCoroutine);
        }
        sizedUpCoroutine = StartCoroutine(SizedUp(text));
    }

    //public void ChoiceSetup(DialogueChoiceData choice)
    //{
    //    Setup("", choice.text);
    //    this.nextNode = choice.nextNode;
    //}

    //public void BTNClick()
    //{
    //    if (nextNode != null)
    //    {
    //        DialogueManager.dialogueManager.RunDialogue(nextNode);
    //        DialogueManager.dialogueManager.ClearChoice();
    //    }
    //    else
    //    {
    //        Debug.Log("Nextnode is null");
    //    }
    //}

}
