using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosManager : MonoBehaviour
{

    private GameObject player;

    public static bool hasTeleported;
    private float PosX { get; set; }
    private float PosY {get; set; }
    private float PosZ { get; set; }


    DroneSceneTransition playerCoords = new DroneSceneTransition();

    private string currScene;

    void Start()
    {
        Debug.Log("What is my current sanity? " + Managers.Player.sanity + "%");
        //Debug.Log("Have I teleported yet? " + hasTeleported);
        player = GameObject.FindGameObjectWithTag("Player");

        if (!hasTeleported)
        {
            playerCoords.GetPlayerPosition(player);
        }

        currScene = PlayerPrefs.GetString("PreviousScene");

        PosX = PlayerPrefs.GetFloat("PreviousXcord");
        PosY = PlayerPrefs.GetFloat("PreviousYcord");
        PosZ = PlayerPrefs.GetFloat("PreviousZcord");

        //Debug.Log("The player's X cord inside of the manager " + PosX);

    }

    void FixedUpdate()
    {
        if (hasTeleported)
        {
            if (currScene == SceneManager.GetActiveScene().name)
            {

                player.transform.position = new Vector3(PosX, PosY, PosZ);
                hasTeleported = false;
                Debug.Log("The player's X cord inside of the manager after tele " + PosX);
            }

        }
    }


}
