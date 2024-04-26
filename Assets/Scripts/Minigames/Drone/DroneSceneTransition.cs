using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class DroneSceneTransition : MonoBehaviour
{

    private bool insideZone;

    [SerializeField] public TMP_Text tutorialText;

    private GameObject player;
    
    
    private void Start()
    {
        tutorialText.gameObject.SetActive(false);
        insideZone = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            insideZone = true;
            tutorialText.gameObject.SetActive(true);
            tutorialText.SetText("Press E to enter the drone minigame!");
            //Debug.Log("Press E to enter the drone minigame!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialText.gameObject.SetActive(false);
            insideZone = false;
        }
    }

    //Similar to the door opening scripting, when the player enters the area and
    //presses "E", it will load the drone minigame.
    void Update()
    {

        transform.Rotate(0, 7f * Time.deltaTime, 0);
        if (insideZone && Input.GetKey(KeyCode.E) && Managers.Player.requiredSanity < Managers.Player.sanity)
        {

            GetPlayerPosition(player);

            SceneManager.LoadScene("DroneRacing");
            PlayerPosManager.hasTeleported = true;
        }

        if (insideZone && Managers.Player.requiredSanity >= Managers.Player.sanity)
        {
            tutorialText.SetText("I am too exhausted to play this minigame...");
        }
    }

    public void GetPlayerPosition(GameObject player)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentScene);
        PlayerPrefs.SetFloat("PreviousXcord", player.transform.position.x);
        PlayerPrefs.SetFloat("PreviousYcord", player.transform.position.y);
        PlayerPrefs.SetFloat("PreviousZcord", player.transform.position.z);
        
    }
}
