using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string npcName;

    [Tooltip("Determines how many lines an NPC should have. You can edit the lines inside of the element boxes after deciding how many sentences this NPC should have. This system will use a First-In, First-Out (FIFO) manner of displaying the sentences.")]
    [TextArea(3, 10)]
    public string[] sentences;
}

public enum QuestList
{
    None,
    Advisor,
    Faculty
};
