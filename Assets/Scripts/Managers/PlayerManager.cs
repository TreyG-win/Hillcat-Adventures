using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IGameManagers
{
    public ManagerStatus status {  get; private set; }

    //The current sanity value, this will also act as the starting value
    public int sanity { get; private set; }

    //The limit for the sanity value
    public int maxSanity { get; private set; }

    //The required amount of sanity to play a minigame.
    public int requiredSanity { get; private set; }

    public Image SanityBar;
    public Image RequirementBar;

    public GameObject sanityReminder;

    private static bool hasChangedSanity;

    public void Startup()
    {
        Debug.Log("Player manager starting...");
        maxSanity = 100;
        requiredSanity = 40;

        sanityReminder.SetActive(false);


        //If the sanity has never been changed (being only possibly if the method hasn't been called),
        //then set the sanity to the maximum possible value.
        if (!hasChangedSanity)
        {
            sanity = maxSanity;
        }
        else
        {
            sanity = PlayerPrefs.GetInt("Sanity");
        }
        SanityBar.fillAmount = sanity/100f;
        RequirementBar.fillAmount = requiredSanity / 100f;

        status = ManagerStatus.Started;
    }

    void Update()
    {
        if (sanity < requiredSanity)
        {
            sanityReminder.SetActive(true);
        }
        else { sanityReminder.SetActive(false);}
    }

    //This method will change the value of the player's sanity. Place a positive number
    //if you want the value to go up or place a negative when you want it to fall.
    //The minimum will be 0 and the max is determine by maxSanity. 
    public void ChangeSanity(int value)
    {
        sanity += value;

        if (sanity > maxSanity)
        {
            sanity = maxSanity;
        }
        else if (sanity < 0)
        {
            sanity = 0;
        }
        hasChangedSanity = true;
        PlayerPrefs.SetInt("Sanity", sanity);

        SanityBar.fillAmount = sanity / 100f;
        Debug.Log($"Sanity: {sanity}/{maxSanity}");
    }

    //This allows the player to be punished less for performing well on the drone minigame based on score.
    //The "default" value ensures that even if the drone minigame has more targets, then the most the player can
    //lose will be 15% of their sanity.
    public void DroneSanityChanges(int scoreVal)
    {
        switch (scoreVal)
        {

            case < 2:
                ChangeSanity(-40);
                break;

            case 2:
                ChangeSanity(-30);
                break;

            case >= 3:
                ChangeSanity(-15);
                break;

        }
        
    }
    /*
    This allows the player to be punished less for performing well on the FPS minigame based on score.
    These values will definitely need to be looked at depending on balance. However, these are just some
    ideas that I threw out there. 

    The lowest possible sanity punishment would be 15%.
    */
    public void FPSSanityChanges(int scoreVal)
    {
        switch (scoreVal)
        {
            case <= 800:
                ChangeSanity(-40);
                break;

            case <= 1100:
                ChangeSanity(-25);
                break;

            case > 1100:
                ChangeSanity(-15);
                break;
        }

    }

    /*
    This allows the player to be punished less for performing well on FloppyPo based on score.
    These values will definitely need to be looked at depending on balance. However, these are just some
    ideas that I threw out there.

    The lowest possible sanity punishment would be 15%.
    */
    public void FloppyPoSanityChanges(int scoreVal)
    {
        switch (scoreVal)
        {
            case <= 2:
                ChangeSanity(-40);
                break;

            case <= 4:
                ChangeSanity(-25);
                break;

            case >= 6:
                ChangeSanity(-15);
                break;
        }

    }


}
