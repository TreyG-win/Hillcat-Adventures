using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EpilogueBehavior : MonoBehaviour
{
    public Button returnToHH;
    public Button exitGame;

    public void returnToHerrington()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }

    public void endGame()
    {
        Application.Quit();
    }

    public void working()
    {
        Debug.Log("I am working!");
    }
}
