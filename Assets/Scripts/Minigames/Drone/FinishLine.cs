using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private bool insideFinishLine;
    public UIElements elements;

    private void Start()
    {
        //elements = GetComponent<UIElements>();
        insideFinishLine = false;
    }


    void OnTriggerEnter(Collider other)
    {
        elements.ShowWinScreen();
        insideFinishLine = true;
    }

    void Update()
    {
        if(insideFinishLine)
        {

            
            //Managers.Player.DroneSanityChanges(ScoreTracker.DroneScore);
            //SceneManager.LoadScene(0);

        }
    }
}
