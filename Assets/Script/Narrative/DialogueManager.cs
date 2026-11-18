using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class DialogueManager : MonoBehaviour
{


    public static DialogueManager dialogueManager { get; private set; }

    [SerializeField] private CharSprite rightSpeakerSprite;
    [SerializeField] private CharSprite leftSpeakerSprite;

    [SerializeField] private GameObject DialogueContainer;
    [SerializeField] private GameObject ChoiceContainer;
    [SerializeField] private GameObject Prefab_DialogueLine;
    //Ehat???
    [SerializeField] private DialogueLine prefab_DialogueLine;
    [SerializeField] private DialogueChoice prefab_DialogueChoice;
    [SerializeField] private TextAsset targetJsonFile;
    private Dictionary<string, DialogueData> DialogueMap;

    [SerializeField] private Dictionary<string, EDialogueSpriteAnim> DAnimMap; //ไปเพิ่มในหน้า inspcetor ด้วยตัวเองถ้าเพิ่ม enum ใหม่มา

    private DialogueLine currentDialogueLine;
    private DialogueData nextDialogue;
    bool isInDialog = false;
    bool isSelectingChoice = false;

    //DialogueData firstDialogue = null;
    private void Start()
    {
        //nextDialogue = PrepareDialogue(targetJsonFile);

        foreach (Transform child in DialogueContainer.transform) //Clear Mockup
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in ChoiceContainer.transform) 
        {
            Destroy(child.gameObject);
        }

        //RunDialogue(targetJsonFile);
    }

    private void Update()
    {
        if (!isSelectingChoice && isInDialog && Mouse.current.leftButton.wasPressedThisFrame)
        {
            NextDialogue(nextDialogue);
        }
    }

    private void Awake()
    {
        if(dialogueManager == null)
        {
            dialogueManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //input = JsonFile / Output = head of dialogue linklist
    public void RunDialogue(TextAsset jsonFile)
    {
        isInDialog = true;

        UIManager.Instance.Toggle_UI(EMainUI.MainMenu, false, 0.5f);
        rightSpeakerSprite.PlayAnimation(EDialogueSpriteAnim.SlideIn);

        NextDialogue(PrepareDialogue(jsonFile));
    }
    private DialogueData PrepareDialogue(TextAsset jsonFile)
    {
        bool firstActive = true;
        DialogueData firstDialogue = null;
        DialogueList allNodes = JsonUtility.FromJson<DialogueList>(jsonFile.text);

        //For Assign Dialogue obj ref later
        Dictionary<string, DialogueData> TempAllDialogueRef = new Dictionary<string, DialogueData>();
        Dictionary<DialogueData, string> Temp_AllJumptoID = new Dictionary<DialogueData, string>();
        Dictionary<DialogueChoiceData, string> Temp_AllNextNodeID = new Dictionary<DialogueChoiceData, string>();

        DialogueData previousData = null;
        foreach (var JsonNode in allNodes.nodes)
        {
            DialogueData data = new DialogueData { id = JsonNode.id, speaker = JsonNode.speaker, posIndex = JsonNode.posIndex, text = JsonNode.text};
            //Debug.Log("Added : " + data.text);
            TempAllDialogueRef.Add(data.id, data);

            //assign first dialog for use
            if (firstActive)
            {
                firstDialogue = data;
                //Debug.Log("firstDialogue === " + data.text);
                firstActive = false;
            }

            //Mark ChoiceNode For assign Choice After have all node data
            if(JsonNode.choices != null)
            {
                data.choices = new List<DialogueChoiceData>();
                foreach (var JsonChoice in JsonNode.choices)
                {
                    DialogueChoiceData choice = new DialogueChoiceData { text = JsonChoice.text };
                    data.choices.Add(choice);
                    Temp_AllNextNodeID.Add(choice, JsonChoice.nextNodeId);
                }
            }

            if (JsonNode.jumpTo != "" && JsonNode.jumpTo != null)
            {
                //Debug.Log(JsonNode.id + " : " + JsonNode.jumpTo);
                Temp_AllJumptoID.Add(data, JsonNode.jumpTo);
            }

            if (JsonNode.anim != "" && JsonNode.anim != null)
            {
                data.anim = DAnimMap[JsonNode.anim];
            }


            if (previousData != null)
            {
                previousData.nextNode = data;
            }


            //set previousData for link with next node
            previousData = data;

        //end of foreach
        }

        //Check itemin TempDict And assign Ref
        foreach (var item in Temp_AllJumptoID)
        {
            //Debug.Log(item.Value);
            item.Key.nextNode = TempAllDialogueRef[item.Value];
        }

        foreach (var item in Temp_AllNextNodeID)
        {
            item.Key.nextNode = TempAllDialogueRef[item.Value];
        }


        if(firstDialogue == null)
        {
            Debug.Log("first Dialogue Link is null????");
        }
        return firstDialogue;
    }
    public void NextDialogue(DialogueData targetDialouge)
    {
        if (targetDialouge == null)
        {
            EndDialogue();
            return;
        }

        if(currentDialogueLine != null)
        {
            currentDialogueLine.FadeDown();
        }

        Debug.Log("Run targetDialog -> " + targetDialouge.text);

        AddDialogueLine(targetDialouge.speaker, targetDialouge.text);

        //if that dialogue line come with choice
        if(targetDialouge.choices.Count != 0)
        {
            isSelectingChoice = true;
            StartCoroutine(ShowChoice(targetDialouge));
        }

        nextDialogue = targetDialouge.nextNode;
    }

    public IEnumerator ShowChoice(DialogueData targetDialouge)
    {
        yield return new WaitForSeconds(.5f);

        foreach (var item in targetDialouge.choices)
        {
            AddChoice(item);
        }

        //yield return null;
    }

    public void EndDialogue()
    {
        Debug.Log("EndDialogue");
        isInDialog = false;

        rightSpeakerSprite.PlayAnimation(EDialogueSpriteAnim.SlideOut);
        UIManager.Instance.Toggle_UI(EMainUI.MainMenu, true, 0.5f);

        foreach (Transform child in DialogueContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddDialogueLine(string speaker, string text)
    {
        currentDialogueLine = Instantiate(prefab_DialogueLine, DialogueContainer.transform);
        currentDialogueLine.Setup(speaker, text);
    }

    public void AddChoice(DialogueChoiceData choice)
    {
        DialogueChoice dialogueChoice = Instantiate(prefab_DialogueChoice, ChoiceContainer.transform);
        dialogueChoice.Setup(choice);
    }
    public void ClearChoice()
    {
        foreach (Transform child in ChoiceContainer.transform)
        {
            Destroy(child.gameObject);
        }
        isSelectingChoice = false;
    }

    public void DebugPrintDialogue(DialogueData startDialogue)
    {

        if (startDialogue == null)
        {
            Debug.Log("Error!? startDialogue is null");
            return;
        }

        //Debug.Log("first = " + startDialogue.text);
        AddDialogueLine(startDialogue.speaker, startDialogue.text);
        DialogueData nextNode = startDialogue.nextNode;

        while (nextNode != null)
        {
            Debug.Log(nextNode.text);
            AddDialogueLine(nextNode.speaker, nextNode.text);
            nextNode = nextNode.nextNode;
        }
    }


}