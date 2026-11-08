using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject DialogueContainer;
    [SerializeField] private GameObject Prefab_DialogueLine;
    [SerializeField] private DialogueBubble prefab_DialogueBubble;
    [SerializeField] private TextAsset jsonFile;
    private Dictionary<string, DialogueData> DialogueMap;

    DialogueData firstDialogue = null;
    private void Start()
    {
        PrepareDialogue();
        PrintDialogue();
    }
    public DialogueData PrepareDialogue()
    {
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

        return null;
    }

    public void PrintDialogue()
    {

        if (firstDialogue != null)
        {
            Debug.Log("first = " + firstDialogue.text);
            AddDialogueLine(firstDialogue.speaker, firstDialogue.text);
            DialogueData nextNode = firstDialogue.nextNode;

            while (nextNode != null)
            {
                Debug.Log(nextNode.text);
                AddDialogueLine(nextNode.speaker, nextNode.text);
                nextNode = nextNode.nextNode;
                
            }
        }
    }

    public void AddDialogueLine(string speaker, string text)
    {
        DialogueBubble dialogueBubble = Instantiate(prefab_DialogueBubble, DialogueContainer.transform);
        dialogueBubble.Setup(speaker, text);
    }
}