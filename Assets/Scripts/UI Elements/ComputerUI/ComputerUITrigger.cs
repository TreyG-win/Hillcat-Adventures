using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComputerUITrigger : MonoBehaviour
{
    [Header("Quiz Setup")]
    [Tooltip("What course is this quiz for? Note: Do not include 'course' in the name.")]
    public string quizName;
    [Tooltip("What is the index of the scene for this quiz? Example: The Business quiz is on index 7.")]
    public int sceneInt;

    [Header("UI Setup")]
    [Tooltip("This is the minigame prompt in the canvas.")]
    [SerializeField] public TMP_Text tutorialText;
    [Tooltip("This is the QuizTut text in the canvas.")]
    [SerializeField] public TMP_Text quizText;
    [Tooltip("This is the button to allow for exiting the computer. The path to this game object is Canvas > ComputerUI > Exit Button.")]
    public Button exitButton;

    [Header("Misc")]
    [Tooltip("This is a bool to check to see if the player is in the area of the computerUI trigger. Due to time, this bool needed to be public to work. As a result, please leave it be.")]
    public bool insideZone;

    public GameObject loadingPanel; // Reference to the loading panel
    public GameObject computerUIPanel; // Reference to the computer UI panel

    private PlayerInput playerInput;
    private Scenes sceneSelection;

    private bool computerUIActive = false;

    private void Start()
    {
        //loadingPanel = GameObject.FindGameObjectWithTag("ComputerUI Loading");
        //computerUIPanel = GameObject.FindGameObjectWithTag("ComputerUI");

        playerInput = Object.FindAnyObjectByType<PlayerInput>();
        sceneSelection = Object.FindAnyObjectByType<Scenes>();

        tutorialText.gameObject.SetActive(false);
        quizText.gameObject.SetActive(false);
        loadingPanel.SetActive(false);
        computerUIPanel.SetActive(false);
        exitButton.gameObject.SetActive(false);
        insideZone = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            insideZone = true;
            tutorialText.gameObject.SetActive(true);
            quizText.gameObject.SetActive(true);
            tutorialText.SetText("Press E to check your minigame scores!");
            quizText.SetText("Press C to take the " + quizName + " quiz");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialText.gameObject.SetActive(false);
            quizText.gameObject.SetActive(false);
            insideZone = false;
        }
    }

    //Similar to the door opening scripting, when the player enters the area and
    //presses "E", it will load the drone minigame.
    void Update()
    {

        transform.Rotate(0, 7f * Time.deltaTime, 0);
        if (insideZone && Input.GetKey(KeyCode.E) && !computerUIActive)
        {

            // Activate the loading panel
            loadingPanel.SetActive(true);
            playerInput.enabled = false;

            // Start coroutine to show computer UI after one second
            StartCoroutine(ShowComputerUIAfterDelay());

        }

        if (insideZone && Input.GetKey(KeyCode.C) && !computerUIActive)
        {
            startQuiz();
        }
    }

    IEnumerator ShowComputerUIAfterDelay()
    {
        // Wait for one second
        yield return new WaitForSeconds(1f);

        // Deactivate the loading panel
        loadingPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Activate the computer UI panel
        computerUIPanel.SetActive(true);
        exitButton.gameObject.SetActive(true);
        computerUIActive = true;
    }

    public void exitComputerUI()
    {
        // Deactivate the computer UI panel
        computerUIPanel.SetActive(false);
        computerUIActive = false;
        exitButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInput.enabled = true;
    }

    void startQuiz()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        sceneSelection.LoadSelectedScene(sceneInt);

    }
}
