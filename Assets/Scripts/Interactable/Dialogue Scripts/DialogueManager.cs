using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{

    public Queue<string> dialogueSentence;

    //This should be the dialogue box from the canvas
    [SerializeField] public GameObject Dialogue;

    //[Tooltip("This should be the Player Input system located inside of the scripts directory. The purpose of calling it is to toggle the player's ability to move when in/out of conversations.")]
    //The player input from the new InputSystem
    private PlayerInput playerInput;
    public TMP_Text npcName;
    public TMP_Text npcSentence;

    //Located inside of UIObjectives.cs
    //[Tooltip("This is the script UIObjectives.cs. The purpose of calling it here is to interact with the objective menu when the player starts a conversation.")]
    private UIObjectives obj;

    private ComputerUITrigger[] compUI;

    void Start()
    {
        obj = Object.FindAnyObjectByType<UIObjectives>();
        playerInput = Object.FindAnyObjectByType<PlayerInput>();
        //compUI = Object.FindAnyObjectByType<ComputerUITrigger>();
        compUI = Object.FindObjectsByType<ComputerUITrigger>(FindObjectsSortMode.None);

        Dialogue.SetActive(false);
        dialogueSentence = new Queue<string>();
    }
    #region Conversation Parts
    #region Starting Dialogue
    //Begins the dialogue sequence with the target
    public void StartDialogue(Dialogue dialogue)
    {
        //Hides the tutorial text for the objective box
        obj.toggleListTutorial();

        //If the objective box is visible, hide it.
        if(obj.ObjectiveList.activeInHierarchy == true )
        {
            obj.toggleObjectiveBox();
        }
        foreach (ComputerUITrigger item in compUI)
        {
            if (item.tutorialText.IsActive())
            {
                item.tutorialText.gameObject.SetActive(false);
                item.quizText.gameObject.SetActive(false);
            }
            item.exitComputerUI();
            item.enabled = false;
        }

        
        //Disables the objective and computer UI elements
        obj.enabled = false;
        

        //Disables the player's movement when in dialogue
        playerInput.enabled = false;


        Dialogue.SetActive(true);
        Debug.Log("Starting conversation with " + dialogue.npcName);

        npcName.SetText(dialogue.npcName + ":");

        dialogueSentence.Clear();

        //Places all of the assigned sentences into the Queue.
        foreach (string sentence in dialogue.sentences)
        {
            dialogueSentence.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    #endregion

    #region Showing the next sentence
    //Upon some kind of interaction, the next sentence will be loaded if there are
    //any available.
    public void DisplayNextSentence()
    {
        if (dialogueSentence.Count == 0)
        {
            Debug.Log("Oops, we are out of things to say!");
            EndDialogue();
            return;
        }

        string sentence = dialogueSentence.Dequeue();
        npcSentence.SetText(sentence);
        
    }
    #endregion

    #region Ending Dialogue
    //Of course, this will end the dialogue sequence and set the inConversation bool to false.
    public void EndDialogue()
    {
        Dialogue.SetActive(false);
        
        //Re-enables the player's movement when the dialogue is complete
        playerInput.enabled = true;

        //Re-enables the objective UI elements
        obj.enabled = true;

        //Re-enables the computer UI elements
        foreach (ComputerUITrigger item in compUI)
        {
            item.enabled = true;
            if (item.insideZone)
            {
                item.tutorialText.gameObject.SetActive(true);
                item.quizText.gameObject.SetActive(true);
            }
        }
        


        

        //Re-enables the objective tutorial text
        obj.toggleListTutorial();
    }
    #endregion

    #endregion
}
