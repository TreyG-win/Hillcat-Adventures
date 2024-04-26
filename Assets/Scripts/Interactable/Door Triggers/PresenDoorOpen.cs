using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is a script that is designed to be used
 * for the game test session to open/close any target door.
 * 
 * This script should be placed on a box collider, then set to
 * target the desired door in the inspector. You can target multiple
 * doors at once with a single collider, but I don't know why you would
 * do so.
 */
public class PresenDoorOpen : MonoBehaviour
{

    //The target in this case will be the door hinge, specifically.
    [Header("The target door(s) that you want to open with this collider")]
    [SerializeField] GameObject[] targets;
    [SerializeField] GameObject TutorialText;

    private bool insideZone;

    private void Start()
    {
        insideZone = false;
        TutorialText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks to see if the gameObject that has touched the collider is a player by
        //their tag in the inspector.
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject target in targets)
            {
                //This debug log is for a temporary usecase until UI elements are introduced.
                TutorialText.SetActive(true);
                insideZone = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //Checks to see if the gameObject has the player tag assigned to them.
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject target in targets)
            {
                //Sends a message out to the gameobject that is the target, running the method of a matching name.
                //In this case, DoorOpenDevice.cs will be instructed to run the Deactivate method.
                target.SendMessage("Deactivate");
                TutorialText.SetActive(false);
                insideZone = false;
            }
        }

    }

    private void Update()
    {
        //If the player is inside of the zone and presses "E", then the door will open.
        if (insideZone && Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject target in targets)
            {
                //Sends a message to the DoorOpenDevice object to run the Activate method.
                //Thus, opening the door.
                target.SendMessage("Activate");
            }

        }
    }

}
