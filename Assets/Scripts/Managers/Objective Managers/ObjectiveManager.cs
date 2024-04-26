using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    [Header("Enter HH Quest")]
    public Image entryQuestCheck;
    public TMP_Text entryQuestDesc;
    public static bool entryQuestComplete;

    [Header("Talk to the advisor")]
    public Image advisorQuestCheck;
    public TMP_Text advisorQuestDesc;
    public static bool advisorQuestComplete;

    [Header("Take the game dev quiz")]
    public Image gameDevQuestCheck;
    public TMP_Text gameDevQuestDesc;
    public static bool gameDevQuestComplete;

    [Header("Take the CSharp quiz")]
    public Image cSharpQuestCheck;
    public TMP_Text cSharpQuestDesc;
    public static bool cSharpQuestComplete;

    [Header("Take the Business quiz")]
    public Image businessQuizQuestCheck;
    public TMP_Text businessQuizQuestDesc;
    public static bool businessQuizQuestComplete;

    [Header("Play the drone minigame")]
    public Image droneQuestCheck;
    public TMP_Text droneQuestDesc;
    public static bool droneQuestComplete;

    [Header("Play the FPS minigame")]
    public Image FPSQuestCheck;
    public TMP_Text FPSQuestDesc;
    public static bool FPSQuestComplete;

    [Header("Play FloppyPo")]
    public Image floppyPoQuestCheck;
    public TMP_Text floppyPoQuestDesc;
    public static bool floppyPoQuestComplete;

    [Header("Talk to the faculty")]
    public Image facultyQuestCheck;
    public TMP_Text facultyQuestDesc;
    public static bool facultyQuestComplete;

    [Header("Misc dependencies")]
    public TMP_Text tipsUI;
    public TMP_Text parkinglotText;
    public GameObject victoryTrigger;

    //These are ints to track the number of quests completed (minus the enter HH quest)
    public static int numQuestsCompleted;
    public static int minigamesCompleted;
    public static int quizzesCompleted;
    private int numberOfQuests = 8;

    //These are the backgrounds that are being grabbed by their tags
    private GameObject entryQuestBackground;
    private GameObject advisorBackground;
    private GameObject gameDevBackground;
    //private GameObject CSharpBackground;
    private GameObject facultyBackground;
    //private GameObject BusinessBackground;
    private GameObject minigameBackground;
    private GameObject[] minigameTriggers;
    private GameObject advisor1;
    private GameObject advisor2;
    private GameObject facultyNPC;
    void Awake()
    {
        entryQuestBackground = GameObject.FindGameObjectWithTag("EnterBuildingQuest");
        advisorBackground = GameObject.FindGameObjectWithTag("AdvisorQuest");
        gameDevBackground = GameObject.FindGameObjectWithTag("GameDevQuizQuest");

        //CSharpBackground = GameObject.FindGameObjectWithTag("GameDevQuizQuest");
        //BusinessBackground = GameObject.FindGameObjectWithTag("GameDevQuizQuest");
        facultyBackground = GameObject.FindGameObjectWithTag("FacultyQuest");
        advisor1 = GameObject.FindGameObjectWithTag("Advisor1");
        advisor2 = GameObject.FindGameObjectWithTag("Advisor2");
        facultyNPC = GameObject.FindGameObjectWithTag("FacultyNPC");

        minigameBackground = GameObject.FindGameObjectWithTag("MinigamesQuest");
        minigameTriggers = GameObject.FindGameObjectsWithTag("MinigameTrigger");

        entryQuestCheck.gameObject.SetActive(false);

        advisorQuestCheck.gameObject.SetActive(false);
        advisorQuestDesc.gameObject.SetActive(false);
        advisorBackground.gameObject.SetActive(false);

        gameDevQuestCheck.gameObject.SetActive(false);
        gameDevQuestDesc.gameObject.SetActive(false);
        gameDevBackground.gameObject.SetActive(false);

        if (!gameDevQuestComplete)
        {
            foreach (GameObject item in minigameTriggers)
            {
                item.SetActive(false);
            }
        }

        cSharpQuestCheck.gameObject.SetActive(false);
        cSharpQuestDesc.gameObject.SetActive(false);
        //CSharpBackground.gameObject.SetActive(false);

        businessQuizQuestCheck.gameObject.SetActive(false);
        businessQuizQuestDesc.gameObject.SetActive(false);
        //BusinessBackground.gameObject.SetActive(false);

        droneQuestCheck.gameObject.SetActive(false);
        droneQuestDesc.gameObject.SetActive(false);
        minigameBackground.gameObject.SetActive(false);

        FPSQuestCheck.gameObject.SetActive(false);
        FPSQuestDesc.gameObject.SetActive(false);

        floppyPoQuestCheck.gameObject.SetActive(false);
        floppyPoQuestDesc.gameObject.SetActive(false);

        facultyQuestCheck.gameObject.SetActive(false);
        facultyQuestDesc.gameObject.SetActive(false);
        facultyBackground.gameObject.SetActive(false);

        tipsUI.gameObject.SetActive(false);
        parkinglotText.gameObject.SetActive(false);

        victoryTrigger.SetActive(false);

        if (quizzesCompleted == 3)
        {
            advisor1.SetActive(false);
            advisor2.SetActive(true);
        }
        else
        {
            advisor1.SetActive(true);
            advisor2.SetActive(false);
        }

        if(minigamesCompleted == 3)
        {
            facultyNPC.SetActive(true);
        }
        else
        {
            facultyNPC.SetActive(false);
        }
        Debug.Log("Number of quests completed: " + numQuestsCompleted);

    }

    void Start()
    {
        if (entryQuestComplete)
        {
            completeEntryQuest();

            if (advisorQuestComplete)
            {
                completeAdvisorQuest();
                //minigameBackground.SetActive(true);

                if (gameDevQuestComplete)
                {
                    completeGameDevQuiz();
                }

                if(cSharpQuestComplete)
                {
                    completeCSharpQuest();
                }

                if(businessQuizQuestComplete)
                {
                    completeBusinessQuest();
                }

                if (droneQuestComplete)
                {
                    completeDroneQuest();
                }

                if (FPSQuestComplete)
                {
                    completeFPSQuest();
                }

                if (floppyPoQuestComplete)
                {
                    completeFloppyPoQuest();
                }

            }
        }
        if(facultyQuestComplete)
        {
            completeFacultyQuest();
        }

    }
    //Flags entering the building quest as completed
    public void completeEntryQuest()
    {
        entryQuestCheck.gameObject.SetActive(true);
        entryQuestComplete = true;
        entryQuestBackground.SetActive(false);

        advisorQuestDesc.gameObject.SetActive(true);
        advisorBackground.SetActive(true);
    }

    //Flags the advisor conversation quest as completed
    public void completeAdvisorQuest()
    {
        advisorQuestCheck.gameObject.SetActive(true);
        advisorQuestComplete = true;
        advisorBackground.SetActive(false);

        gameDevQuestDesc.gameObject.SetActive(true);
        cSharpQuestDesc.gameObject.SetActive(true);
        businessQuizQuestDesc.gameObject.SetActive(true);
        gameDevBackground.SetActive(true);

    }
    //Flags the game dev quiz quest as completed
    public void completeGameDevQuiz()
    {
        gameDevQuestCheck.gameObject.SetActive(true);
        gameDevQuestComplete = true;


        if (quizzesCompleted == 3)
        {
            foreach (GameObject item in minigameTriggers)
            {
                item.SetActive(true);
            }
            gameDevBackground.SetActive(false);
            minigameBackground.SetActive(true);
            droneQuestDesc.gameObject.SetActive(true);
            FPSQuestDesc.gameObject.SetActive(true);
            floppyPoQuestDesc.gameObject.SetActive(true);
            tipsUI.gameObject.SetActive(true);

            if (advisor1.activeInHierarchy)
            {
                advisor1.SetActive(false);
            }
            advisor2.SetActive(true);
        }

    }

    public void completeCSharpQuest()
    {
        cSharpQuestCheck.gameObject.SetActive(true);
        cSharpQuestComplete = true;


        if (quizzesCompleted == 3)
        {
            foreach (GameObject item in minigameTriggers)
            {
                item.SetActive(true);
            }
            gameDevBackground.SetActive(false);
            minigameBackground.SetActive(true);
            droneQuestDesc.gameObject.SetActive(true);
            FPSQuestDesc.gameObject.SetActive(true);
            floppyPoQuestDesc.gameObject.SetActive(true);
            tipsUI.gameObject.SetActive(true);

            if (advisor1.activeInHierarchy)
            {
                advisor1.SetActive(false);
            }
            advisor2.SetActive(true);
        }
    }

    public void completeBusinessQuest()
    {
        businessQuizQuestCheck.gameObject.SetActive(true);
        businessQuizQuestComplete = true;

        if (quizzesCompleted == 3)
        {
            foreach (GameObject item in minigameTriggers)
            {
                item.SetActive(true);
            }
            gameDevBackground.SetActive(false);
            minigameBackground.SetActive(true);
            droneQuestDesc.gameObject.SetActive(true);
            FPSQuestDesc.gameObject.SetActive(true);
            floppyPoQuestDesc.gameObject.SetActive(true);
            tipsUI.gameObject.SetActive(true);

            if (advisor1.activeInHierarchy)
            {
                advisor1.SetActive(false);
            }
            
            advisor2.SetActive(true);
        }
    }

    //Flags the drone minigame quest as completed
    public void completeDroneQuest()
    {
        droneQuestCheck.gameObject.SetActive(true);
        droneQuestComplete = true;

        if (minigamesCompleted >= 3)
        {
            minigameBackground.SetActive(false);
            facultyQuestDesc.gameObject.SetActive(true);
            facultyBackground.SetActive(true);
        }
    }

    //Flags the FPS minigame quest as completed
    public void completeFPSQuest()
    {
        FPSQuestCheck.gameObject.SetActive(true);
        FPSQuestComplete = true;

        if (minigamesCompleted >= 3)
        {
            minigameBackground.SetActive(false);
            facultyQuestDesc.gameObject.SetActive(true);
            facultyBackground.SetActive(true);
        }
        //numQuestsCompleted++;
        //Debug.Log("Number of quests completed " + numQuestsCompleted);
    }

    //Flags the FloppyPo quest as completed
    public void completeFloppyPoQuest()
    {
        floppyPoQuestCheck.gameObject.SetActive(true);
        floppyPoQuestComplete = true;

        if (minigamesCompleted >= 3)
        {
            minigameBackground.SetActive(false);
            facultyQuestDesc.gameObject.SetActive(true);
            facultyBackground.SetActive(true);
        }
        //numQuestsCompleted++;
        //Debug.Log("Number of quests completed " + numQuestsCompleted);
    }

    public void completeFacultyQuest()
    {
        facultyQuestCheck.gameObject.SetActive(true);
        facultyQuestComplete = true;
        facultyBackground.SetActive(false);
        if (!victoryTrigger.activeInHierarchy)
        {
            parkinglotText.gameObject.SetActive(true);
            victoryTrigger.SetActive(true);
        }
    }

    //Resets all of the quest booleans and the number of quests completed
    public static void resetAllQuests()
    {
        entryQuestComplete = false;
        advisorQuestComplete = false;
        gameDevQuestComplete = false;
        cSharpQuestComplete = false;
        businessQuizQuestComplete = false;
        droneQuestComplete = false;
        FPSQuestComplete = false;
        floppyPoQuestComplete = false;
        facultyQuestComplete = false;

        quizzesCompleted = 0;
        numQuestsCompleted = 0;
        minigamesCompleted = 0;
    }
}
