using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is used to transition the player to the range minigame.
/// To use this script, it should be placed on something within the Esports room that has a collider with isTrigger selected.
/// 
/// For now it is being placed on an empty game object that is surronding the back row of computer desks within the esports room.
/// </summary>
public class MinigameLoader : MonoBehaviour
{
    private bool atComputer = false;

    // Update is called once per frame
    void Update()
    {
        if(atComputer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            atComputer = true;
            Debug.Log("Press E to play the Range minigame!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            atComputer = false;
        }
    }
}
