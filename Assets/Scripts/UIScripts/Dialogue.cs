using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct DialogueLine
{
    public string speaker;
    public string line;
    public string eventId;
}

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    public string endEventId;
}