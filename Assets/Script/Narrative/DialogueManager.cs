using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextAsset jsonFile;

    private void Start()
    {
        PrepareDialogue();
    }
    public DialogueData PrepareDialogue()
    {
        DialogueList allData = JsonUtility.FromJson<DialogueList>(jsonFile.text);
        //Debug.Log(allData.nodes[0].text);
        //Debug.Log("what");
        //bool fisrtActive = true;
        foreach (var item in allData.nodes)
        {
            Debug.Log(item.speaker + " พูดว่า: " + item.text);
        }
        return null;
    }
}
