using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    private bool insideZone;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Congrats! You are victorious!");
            SceneManager.LoadScene(6);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Managers.Player.ChangeSanity(Managers.Player.maxSanity);
            ObjectiveManager.resetAllQuests();
            /*ObjectiveManager.entryQuestComplete = false;
            ObjectiveManager.droneQuestComplete = false;
            ObjectiveManager.FPSQuestComplete = false;
            ObjectiveManager.floppyPoQuestComplete = false;
            ObjectiveManager.numQuestsCompleted = 0;*/
            this.gameObject.SetActive(false);
        }
    }
}
