using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization;

public class QuizManager : MonoBehaviour
{
    public List<Questions> QnA;
    public List<GameObject> options;
    private Scenes sceneManage;
    private ObjectiveManager obj;

    public int currentQuestion;
    private int score;
    private int totalQuestions;
    private int answeredQuestions = 1;
    private int optionsSize;

    public TMP_Text Num;
    public TMP_Text questionText;
    public TMP_Text scoreText;


    public GameObject quizPanel;
    public GameObject resultsPanel;

    public void Start()
    {
        totalQuestions = QnA.Count;
        resultsPanel.SetActive(false);
        generateQuestion();
        Num.SetText(answeredQuestions + "/" + totalQuestions + ":");
        sceneManage = Object.FindAnyObjectByType<Scenes>();
        obj = Object.FindAnyObjectByType<ObjectiveManager>();
    }

    //if answer is correct
    public void correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        answeredQuestions++;
        Num.SetText(answeredQuestions + "/" + totalQuestions + ":");
    }

    //if answer is wrong
    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        answeredQuestions++;
        Num.SetText(answeredQuestions + "/" + totalQuestions + ":");
    }

    //shows the results of the quiz and gives otpion to retry or exit the quiz
    public void gameOver()
    {
        quizPanel.SetActive(false);
        resultsPanel.SetActive(true);
        scoreText.text = score + "/" + totalQuestions;

        //If the quiz is the business scene
        if (SceneManager.GetSceneAt(1).buildIndex == 7)
        {

            if (score >= 1 && !ObjectiveManager.businessQuizQuestComplete && ObjectiveManager.advisorQuestComplete)
            {
                ObjectiveManager.businessQuizQuestComplete = true;
                ObjectiveManager.numQuestsCompleted++;
                ObjectiveManager.quizzesCompleted++;
                obj.completeBusinessQuest();
            }
            ScoreTracker.BusinessQuizScore = score;
        }

        //If the quiz is the game dev scene
        else if (SceneManager.GetSceneAt(1).buildIndex == 8)
        {
            if (score >= 4 && !ObjectiveManager.gameDevQuestComplete && ObjectiveManager.advisorQuestComplete)
            {
                ObjectiveManager.gameDevQuestComplete = true;
                ObjectiveManager.numQuestsCompleted++;
                ObjectiveManager.quizzesCompleted++;
                obj.completeGameDevQuiz();

            }
            ScoreTracker.GameDevQuizScore = score;

        }

        //If the quiz is the csharp scene
        else if (SceneManager.GetSceneAt(1).buildIndex == 9)
        {
            if (score >= 4 && !ObjectiveManager.cSharpQuestComplete && ObjectiveManager.advisorQuestComplete)
            {
                ObjectiveManager.cSharpQuestComplete = true;
                ObjectiveManager.numQuestsCompleted++;
                ObjectiveManager.quizzesCompleted++;
                obj.completeCSharpQuest();

            }
            ScoreTracker.CSharpQuizScore = score;
        }
        
    }

    //resets the scene
    public void retry()
    {
        sceneManage.restartScene(SceneManager.GetSceneAt(1).buildIndex);
    }

    //exits the quiz when ready, not implemented yet
    public void exit()
    {
        sceneManage.UnloadSelectedScene(SceneManager.GetSceneAt(1).buildIndex);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //sets the answers
    void setAnswers()
    {

        for (int i = 0; i < optionsSize; i++)
        {
            
            options[i].GetComponent<Answers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<Answers>().isCorrect = true;
            }
        }
    }

    //gets a question from the list
    void generateQuestion()
    {
        optionsSize = options.Count;

        for(int i = 0; i < options.Count; i++)
        {
            options[i].SetActive(false);
        }

        if (QnA.Count > 0)
        {

            currentQuestion = Random.Range(0, QnA.Count);

            questionText.text = QnA[currentQuestion].Question;

            optionsSize = QnA[currentQuestion].Answers.Length;

            for (int i = 0; i < optionsSize; i++)
            {
                options[i].SetActive(true);
            }
            setAnswers();
            
        }
        else
        {
            gameOver();
            Debug.Log("Out of questions");
        }
    }
}
