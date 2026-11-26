using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public string speakerName;
    public GameObject speaker;
    [TextArea(2, 5)]
    public string dialogue;

    public bool isPlayer = false;
}