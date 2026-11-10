using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoiceData
{
    public string text;
    public DialogueData nextNode;
}

public class DialogueData
{
    public string id;
    public string speaker;
    public string text;
    public string jumpToID;
    public DialogueData nextNode;
    public List<DialogueChoiceData> choices;
}

[System.Serializable]
public class JsonDialogueData
{
    public string id;
    public string speaker;
    public string text;
    public string jumpTo;
    public List<JsonDialogueChoiceData> choices; 
}

[System.Serializable]
public class JsonDialogueChoiceData
{
    public string text;
    public string nextNodeId;
}


[System.Serializable]
public class DialogueList
{
    // สำหรับ Json --- ชื่อตัวแปร "nodes" ต้องตรงกับใน JSON
    public List<JsonDialogueData> nodes;
}
