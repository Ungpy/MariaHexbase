using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager { get; private set; }

    [SerializeField] private GameObject DialogueContainer;
    [SerializeField] private GameObject ChoiceContainer;
    [SerializeField] private GameObject Prefab_DialogueLine;
    [SerializeField] private DialogueBubble prefab_DialogueBubble;
    [SerializeField] private DialogueBubble prefab_ChoiceBubble;
    [SerializeField] private TextAsset targetJsonFile;
    private Dictionary<string, DialogueData> DialogueMap;

    private DialogueData currentDialogue;
    bool isInDialog = false;
    bool isSelectingChoice = false;

    //DialogueData firstDialogue = null;
    private void Start()
    {
        currentDialogue = PrepareDialogue(targetJsonFile);
    }

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame){
            RunDialogue(currentDialogue);
        }
        if (!isSelectingChoice && Mouse.current.leftButton.wasPressedThisFrame)
        {
            RunDialogue(currentDialogue);
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
    public DialogueData PrepareDialogue(TextAsset jsonFile)
    {
        DialogueData firstDialogue = null;
        DialogueList allNodes = JsonUtility.FromJson<DialogueList>(jsonFile.text);
        bool firstActive = true;

        Dictionary<string, DialogueData> TempAllDialogueRef = new Dictionary<string, DialogueData>();

        //For Assign Dialogue obj ref later
        Dictionary<DialogueData, string> Temp_AllJumptoID = new Dictionary<DialogueData, string>();
        Dictionary<DialogueChoiceData, string> Temp_AllNextNodeID = new Dictionary<DialogueChoiceData, string>();

        DialogueData previousData = null;
        foreach (var JsonNode in allNodes.nodes)
        {
            DialogueData data = new DialogueData { id = JsonNode.id, speaker = JsonNode.speaker, text = JsonNode.text};
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
    public void RunDialogue(DialogueData targetDialouge)
    {
        isInDialog = true;

        if (targetDialouge == null)
        {
            EndDialogue();
            Debug.Log("End");
            return;
        }
        Debug.Log("Run targetDialog -> " + targetDialouge.text);

        AddDialogueLine(targetDialouge.speaker, targetDialouge.text);

        //if that dialogue line come with choice
        if(targetDialouge.choices.Count != 0)
        {
            foreach (var item in targetDialouge.choices)
            {
                AddChoice(item);
            }
        }

        currentDialogue = targetDialouge.nextNode;
    }

    public void EndDialogue()
    {
        foreach (Transform child in DialogueContainer.transform)
        {
            Destroy(child.gameObject);
        }
        isInDialog = false;
    }

    public void AddDialogueLine(string speaker, string text)
    {
        DialogueBubble dialogueBubble = Instantiate(prefab_DialogueBubble, DialogueContainer.transform);
        dialogueBubble.Setup(speaker, text);
    }

    public void AddChoice(DialogueChoiceData choice)
    {
        isSelectingChoice = true;
        DialogueBubble dialogueBubble = Instantiate(prefab_ChoiceBubble, ChoiceContainer.transform);
        dialogueBubble.ChoiceSetup(choice);
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