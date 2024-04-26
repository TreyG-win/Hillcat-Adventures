using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles when and what objects of the UI are shown and 
/// handles exiting or restarting the minigame scene.
/// </summary>
public class UIHud: MonoBehaviour
{
    //Not currently used because build settings brought up an error

    //[SerializeField] private string mainGameScene = "HHRSU Project"; //Change to RSU scene name
    //[SerializeField] private string miniGameName = "RangeMinigame"; //Change to minigame scene name
    

    public Text scoreText;
    public Text timerText;
    public Text winText;
    public Text titleText;

    public Button PlayButton;
    public Button RestartButton; //Used to show or hide restart button
    public Button ExitButton; //Used to show or hide exit
    
    public GameBehaviour gameManager;
    public FPSController fpsControl;

    private bool isPaused = false;
    private bool isPlaying = false;

    //Starts the game on the pause / start screen
    void Start()
    {
        isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        fpsControl.canMove = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
        winText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //pauses or unpauses the game when escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.showWinScreen)
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

        scoreText.text = gameManager.Targets.ToString() + " Targets Hit";

        // Update the timer text
        timerText.text = "Time: " + gameManager.elapsedTime.ToString("F2");
        if (gameManager.Targets == 10) 
        {
            scoreText.text = "You got them all!";
        }
    }

    void PauseGame()
    {
        fpsControl.canMove = false;
        Time.timeScale = 0.0f;
        timerText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void UnpauseGame()
    {
        fpsControl.canMove = true;
        timerText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;

    }
    //Starts the game
    public void StartGameButton()
    {
        isPlaying = true;
        fpsControl.canMove = true;
        titleText.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    //Restarts the minigame's scene
    public void RestartGameButton()
    {
        gameManager.restarting = true;
        SceneManager.LoadScene(3); //Set to the number of the scene because trying to go off scene name presented error (probably 1 or 2)

        //Use if build settings doesn't bring up an error
        //SceneManager.LoadScene(miniGameName);
        
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
        gameManager.showWinScreen = true;
        fpsControl.canMove = false;

        if(!ObjectiveManager.FPSQuestComplete)
        {
            ObjectiveManager.FPSQuestComplete = true;
            ObjectiveManager.numQuestsCompleted++;
            ObjectiveManager.minigamesCompleted++;
            Debug.Log("Number of quests completed " + ObjectiveManager.numQuestsCompleted);
        }
        Time.timeScale = 0.0f;

        //The player's sanity will be drained when the victory screen is shown.
        Managers.Player.FPSSanityChanges(ScoreTracker.FPSScore);

        winText.text = "You Win!\n" + "You Scored " + gameManager.totalScore + " Points!";
        winText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

        if (Managers.Player.sanity > Managers.Player.requiredSanity)
        {
            RestartButton.gameObject.SetActive(true);
        }
        
        ExitButton.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }
}
