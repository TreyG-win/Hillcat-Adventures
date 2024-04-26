using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPrompt : MonoBehaviour
{
    [SerializeField] public GameObject DroneText;
    [SerializeField] public GameObject FloppyPoText;
    [SerializeField] public GameObject FPSText;
    //[SerializeField] public GameObject LoadingScreen;

    public TMP_Text DroneInfo;
    public TMP_Text FPSInfo;
    public TMP_Text FloppyPoInfo;

    private void Start()
    {
        DroneText.SetActive(false);
        FPSText.SetActive(false);
        FloppyPoText.SetActive(false);
        //LoadingScreen.SetActive(false);
    }
    void Update()
    {
        DroneInfo.SetText("Total Drone score is: " + ScoreTracker.DroneScore);
        FPSInfo.SetText("Total FPS score is: " + ScoreTracker.FPSScore);
        FloppyPoInfo.SetText("Total FloppyPo score is: " + ScoreTracker.FloppyPoScore);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (DroneText.activeInHierarchy == false)
            {
                DroneText.SetActive(true);
                FPSText.SetActive(true);
                FloppyPoText.SetActive(true);
            }
            else
            {
                DroneText.SetActive(false);
                FPSText.SetActive(false);
                FloppyPoText.SetActive(false);
            }
        }
    }

    //Currently setting up for a loading screen just in case
    public void loadingScreen()
    {
        //LoadingScreen.SetActive(true);
    }
}
