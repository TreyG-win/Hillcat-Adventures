using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;

public class UIElements : MonoBehaviour
{
    [SerializeField] public GameObject ScorePanel;
    [SerializeField] public TMP_Text score;

    [SerializeField] public CinemachineVirtualCamera playerCam;

    public GameObject titleObject;
    public GameObject victoryObject;
    public GameObject pauseScreen;
    public TMP_Text victoryText;

    public Button PlayButton;
    public Button RestartButton; //Used to show or hide restart button
    public Button ExitButton; //Used to show or hide exit

    public PlayerInput droneControl;

    private bool isPaused = false;
    private bool isPlaying = false;
    private bool showingWinScreen;

    //Starts the game on the pause / start screen
    void Start()
    {
        playerCam.Priority = 0;
        isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        droneControl.enabled = false;
        victoryObject.SetActive(false);
        ScorePanel.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        pauseScreen.SetActive(false);
        showingWinScreen = false;

    }

    // Update is called once per frame
    void Update()
    {
        //pauses or unpauses the game when escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !showingWinScreen)
        {
            if (isPlaying)
            {
                if (!isPaused)
                {
                    isPaused = true;
                    PauseGame();
                }
                else
                {
                    isPaused = false;
                    UnpauseGame();
                }
            }
        }

        score.SetText(ScoreTracker.DroneScore.ToString());
    }

    void PauseGame()
    {
        droneControl.enabled = false;
        Time.timeScale = 0.0f;
        ScorePanel.SetActive(false);
        RestartButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        pauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void UnpauseGame()
    {
        droneControl.enabled = true;
        ScorePanel.SetActive(true);
        RestartButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;

    }
    //Starts the game
    public void StartGameButton()
    {

        isPlaying = true;
        droneControl.enabled = true;

        titleObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        ScorePanel.SetActive(true);
        playerCam.Priority = 11;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    //Restarts the minigame's scene
    public void RestartGameButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(4);

    }

    //Exits the minigame's scene back to the main game
    public void ExitGameButton()
    {
        UnpauseGame();

        //If we want to rebalance the minigame to not drain sanity until exiting, then we
        //can uncomment the method below and comment out the same method inside of ShowWinScreen(). 

        //Managers.Player.FPSSanityChanges(ScoreTracker.FPSScore);
        //Change to number of main scene if build settings brings up error (Probably 0 or 1)
        SceneManager.LoadScene(1);
    }

    //Shows the win screen
    public void ShowWinScreen()
    {
        showingWinScreen = true;
        droneControl.enabled = false;

        if (!ObjectiveManager.droneQuestComplete)
        {
            ObjectiveManager.droneQuestComplete = true;
            ObjectiveManager.numQuestsCompleted++;
            ObjectiveManager.minigamesCompleted++;
            Debug.Log("Number of quests completed " + ObjectiveManager.numQuestsCompleted);
        }
        
        Time.timeScale = 0.0f;

        //The player's sanity will be drained when the victory screen is shown.
        Managers.Player.DroneSanityChanges(ScoreTracker.DroneScore);

        
        victoryObject.SetActive(true);
        victoryText.SetText("Congratulations!\n" + "You Scored " + ScoreTracker.DroneScore + " Points!");

        ScorePanel.SetActive(false);

        if (Managers.Player.sanity > Managers.Player.requiredSanity)
        {
            RestartButton.gameObject.SetActive(true);
        }

        ExitButton.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }
}
