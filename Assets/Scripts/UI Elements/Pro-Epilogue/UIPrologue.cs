using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the prologue.
/// On start the game will pause and play this scene, switching the text and image on each input
/// on the final screen once input is recieved it will hide the prologue canvas and resume the game.
/// </summary>
public class UIPrologue : MonoBehaviour
{
    public Canvas explanationScreen;
    public TMP_Text explanation;
    public Image PrologueImage;
    public Sprite ESports;
    public Sprite DroneRoom;
    private bool isFirstTextShown = false;
    private bool isSecondTextShown = false;

    void Start()
    {
        //Pauses the game to show prologue
        explanation.text = "Herrington Hall at Rogers State University " +
            "serves as a vibrant hub for academic and technological activities.";
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!isFirstTextShown)
            {
                explanation.text = "Here students can learn from a wide range of " +
                    "business courses and game development classes by utilizing cutting-edge technology.";
                PrologueImage.sprite = ESports;
                isFirstTextShown = true;
            }
            else if (!isSecondTextShown)
            {
                explanation.text = "Some of the technological activities that can be found here are workshops, guest lectures, " +
                    "and collaborative projects that show students the real-world applications of their studies.";
                PrologueImage.sprite = DroneRoom;
                isSecondTextShown = true;
            }
            else
            {
                explanationScreen.enabled = false;
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SceneManager.LoadScene(1);
            }
        }
    }
}
