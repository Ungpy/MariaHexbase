using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextAsset jsonFile;

    DialogueData firstDialogue = null;
    private void Start()
    {
        PrepareDialogue();
        PrintDialogue();
    }
    public DialogueData PrepareDialogue()
    {
        DialogueList allData = JsonUtility.FromJson<DialogueList>(jsonFile.text);
        //Debug.Log(allData.nodes[0].text);
        //Debug.Log("what");

        //foreach (var item in allData.nodes)
        //{
        //    Debug.Log(item.speaker + " พูดว่า: " + item.text);
        //}

        bool firstActive = true;

        DialogueData previousData = null;
        foreach (var item in allData.nodes)
        {
            DialogueData data = new DialogueData { id = item.id, speaker = item.speaker, text = item.text, jumpTo = item.jumpTo };
            if (firstActive)
            {
                firstDialogue = data;
            }
            if (previousData != null)
            {
                previousData.nextNode = data;
            }
            previousData = data;
        }
        return null;
    }

    public void PrintDialogue()
    {
        if (firstDialogue != null)
        {
            DialogueData Tnextnode = firstDialogue.nextNode;
            Debug.Log(firstDialogue.text);

            while (Tnextnode != null)
            {
                Debug.Log(Tnextnode.text);
                Tnextnode = Tnextnode.nextNode;
            }
        }
    }
}