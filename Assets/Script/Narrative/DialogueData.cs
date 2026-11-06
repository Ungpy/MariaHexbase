using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoiceData
{
    public string text;
    public DialogueData nextNode;
}

[System.Serializable]
public class DialogueData
{
    public string id;
    public string speaker;
    public string text;
    public DialogueData nextNode;
    public List<DialogueChoiceData> choice = new List<DialogueChoiceData>();
}

[System.Serializable]
public class DialogueList
{
    // ชื่อตัวแปร "nodes" ต้องตรงกับใน JSON
    public List<DialogueData> nodes;
}
