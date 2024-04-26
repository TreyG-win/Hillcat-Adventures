using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the amount of targets hit, game score, and win screen.
/// </summary>
public class GameBehaviour : MonoBehaviour
{
    public bool showWinScreen = false;
    public bool restarting = false;
    public float totalScore = 0; //What will end up being the high score, should probably be brought back to the main game when possible
    private int targetsHit = 0;
    public float elapsedTime = 0;

    //Keeps track of the amount of targets hit
    public int Targets
    {
        get { return targetsHit; }

        set
        {
            targetsHit = value;
            Debug.LogFormat( "Targets: {0}", targetsHit );
        }
    }

    void Update()
    {

        //Checks if the game is restarting
        if (restarting)
        {
            resetValues();
        }

        //Time since the game started (timeSinceLevelLoad seems to run slow but easily resets on reload)
        elapsedTime = Time.timeSinceLevelLoad;

        //Score earned while game is running
        totalScore = (targetsHit * 100);
        if (targetsHit == 10)
        {
            totalScore = totalScore + 1000;
        }

        //Adjusts score based on time taken
        if (elapsedTime < 10)
        {
            totalScore = totalScore + 500;
        }
        else if (elapsedTime < 20)
        {
            totalScore = totalScore + 250;

        }
        else if (elapsedTime < 30)
        {
            totalScore = totalScore + 100;

        }

        ScoreTracker.FPSScore = (int)totalScore;
    }

    /// <summary>
    /// Resets the values when the game restarts
    /// </summary>
    void resetValues()
    {
        showWinScreen = false;
        restarting = false;
        totalScore = 0;
        targetsHit = 0;
    }
}
