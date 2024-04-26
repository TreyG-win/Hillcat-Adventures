using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizScene : MonoBehaviour
{
    private Scenes sceneManager;

    public int sceneSelection;

    private void Start()
    {
        
        sceneManager = Object.FindAnyObjectByType<Scenes>();
    }

    public void startQuiz()
    {
        if (Input.GetKey(KeyCode.C))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            sceneManager.LoadSelectedScene(sceneSelection);
        }
    }
}
