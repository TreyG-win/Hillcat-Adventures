using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using Unity.VisualScripting;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Required Settings")]
    public Dialogue conversation;
    public QuestList QuestCharacter;
    [Tooltip("This would be the tutorial text itself. The path to this TMP will be in Canvas > Dialogue Prompt > DialogueTut.")]
    public TMP_Text dialogueTutorial;

    private GameObject dialoguePrompt;

    [Header("Optional Settings")]
    [SerializeField] public bool inPresentation;

    [Tooltip("Should the NPC rotate towards the player when in a conversation?")]
    [SerializeField] public bool RotateTowardsPlayer;

    [Tooltip("If a Cinemachine Virtual Camera is desired, then drag and drop one here. Otherwise, the camera will be the player camera by default if left to none. There is an example of this inside of the NPC parent object under 'NPC Cameras'.")]
    [SerializeField] public CinemachineVirtualCamera virtualCamera;


    private GameObject player;
    private ObjectiveManager obj;
    private Quaternion npcStartRotation;

    private bool insideZone;
    private bool inConversation;
    private bool noCamera;


    private void Awake()
    {
        dialoguePrompt = GameObject.FindGameObjectWithTag("DialoguePrompt");
        obj = Object.FindAnyObjectByType<ObjectiveManager>();
    }

    private void Start()
    {
        if (virtualCamera == null)
        {
            noCamera = true;
        }
        else
        {
            virtualCamera.Priority = 0;
        }

        insideZone = false;
        inConversation = false;

        dialoguePrompt.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        npcStartRotation = transform.rotation;
    }

    #region Boolean Checks
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            insideZone = true;

            dialoguePrompt.SetActive(true);
            if (inPresentation)
            {
                dialogueTutorial.SetText("Press F to listen to " + conversation.npcName + "'s lecture");
            }
            else
            {
                dialogueTutorial.SetText("Press E to talk to " + conversation.npcName);
            }


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialoguePrompt.SetActive(false);

            insideZone = false;
        }
    }

    #endregion

    #region Triggering Dialogue
    //Calls to the Dialogue Manager to start the dialogue, then updates the inConversation bool to be true.
    public void TriggerDialogue()
    {
        dialoguePrompt.SetActive(false);

        Object.FindAnyObjectByType<DialogueManager>().StartDialogue(conversation);
        inConversation = true;
    }

    #endregion

    private void Update()
    {
        //If the player is inside of the trigger for dialogue and is not currently
        //in a conversation, then move to the two input methods.
        if(insideZone && !inConversation)
        {
            //If the presentation option is selected, then a new key will be used to start.
            if (inPresentation && Input.GetKeyDown(KeyCode.F))
            {
                StartConvo();
            }

            //If the presentation option is not selected, then the traditional interact button is used.
            else if (!inPresentation && Input.GetKeyDown(KeyCode.E))
            {
                StartConvo();
            }
        }

        /*
         * This is messy, I know. But basically if the player is currently in a conversation
         * then the next dialogue will be displayed. If there are no more sentences left to
         * display, then the player will no longer be set in a conversation.
        */

        if (inConversation)
        {

            turnTowardsPlayer();

            //Of course, if the player is in a conversation and presses the left mouse button
            //then the next dialogue will be called if there is one available.
            if (Input.GetMouseButtonDown(0))
            {
                if (Object.FindAnyObjectByType<DialogueManager>().dialogueSentence.Count > 0)
                {
                    Object.FindAnyObjectByType<DialogueManager>().DisplayNextSentence();
                }
                //or else the conversation ends and the bool is updated accordingly
                else
                {
                    Object.FindAnyObjectByType<DialogueManager>().EndDialogue();

                    inConversation = false;

                    returnCamPosition();

                    //Cleans up the UI elements by bringing back the prompt after a conversation
                    if (!inConversation && insideZone)
                    {
                        dialoguePrompt.SetActive(true);
                    }

                    /*if (!inConversation && ComputerUITrigger.insideZone)
                    {
                        dialoguePrompt.SetActive(true);
                    }*/
                }
            }
        }

        returnToDefaultRotation();
    }

    #region Dialogue Interactions

    //Moves the camera to the target area and starts the conversation.
    void StartConvo()
    {
        if (!noCamera)
        {
            virtualCamera.Priority = 11;
        }

        if (QuestCharacter == QuestList.Advisor && !ObjectiveManager.advisorQuestComplete)
        {
            obj.completeAdvisorQuest();
            ObjectiveManager.numQuestsCompleted++;
        }

        if (QuestCharacter == QuestList.Faculty && !ObjectiveManager.facultyQuestComplete && ObjectiveManager.minigamesCompleted == 3) 
        {    
            obj.completeFacultyQuest();
            ObjectiveManager.numQuestsCompleted++;
        }
        TriggerDialogue();
    }
    //Returns the camera to the player, if a camera has been set.
    void returnCamPosition()
    {
        if (!noCamera)
        {
            virtualCamera.Priority = 0;
        }

    }

    //If the bool is checked inside of the inspector, then
    //then the NPC will rotate towards the player in dialogue.
    void turnTowardsPlayer()
    {
        if (RotateTowardsPlayer)
        {
            Quaternion lookAtPlayer = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtPlayer, Time.deltaTime);
        }
    }

    //If the NPC is supposed to rotate towards the player, then they will if the player is not
    //in a conversation. 
    void returnToDefaultRotation()
    {
        if (RotateTowardsPlayer && !inConversation)
        {
            Quaternion currRotation = Quaternion.Slerp(transform.rotation, npcStartRotation, Time.deltaTime);
            transform.rotation = currRotation;
        }
    }
    #endregion
}
