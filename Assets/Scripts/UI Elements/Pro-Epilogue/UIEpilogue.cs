using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the epilogue screen.
/// On "beatGame" being set to true, the end screen will show and pause the game.
/// Once the screen is showing, any additional input will close the screen and continue the game.
/// 
/// To implement this, beatGame will need to be set to true from some other class, possibly once the objectives are all completed.
/// </summary>
public class UIEpilogue : MonoBehaviour
{
    public Canvas endingScreen;
    //public Button returnToHH;
    //public Button exitGame;

    public bool beatGame = false; //Needs to be set to true for end screen to show
    public bool showingScreen = false;
    // Start is called before the first frame update
    void Start()
    {
        //endingScreen.enabled = false;
        //returnToHH.gameObject.SetActive(true);
        //exitGame.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Tests the screen by setting beatGame to true when pressing "p"
        /*if(Input.GetKeyDown("p"))
        {
            beatGame = true;
        }


        if(beatGame)
        {
            showEndScreen();
        }
        else if(showingScreen)
        {
            if (Input.anyKeyDown)
            {
                returnToHerrington();
            }
        }*/
    }

    void showEndScreen()
    {
        endingScreen.enabled = true;
        Time.timeScale = 0f;
        showingScreen = true;
        //beatGame = false;
    }
    void hideEndScreen()
    {
        endingScreen.enabled = false;
        Time.timeScale = 1f;
        showingScreen = false;
    }

}
