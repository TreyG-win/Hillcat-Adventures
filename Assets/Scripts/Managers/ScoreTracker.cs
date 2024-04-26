using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{

    public static int DroneScore { get; set; }
    public static int FPSScore {  get; set; }

    public static int FloppyPoScore { get; set; }

    public static int BusinessQuizScore { get; set; }
    public static int GameDevQuizScore { get; set; }

    public static int CSharpQuizScore { get; set; }

    private int totalDroneScore = 0;
    private int totalFPSScore = 0;
    private int totalFloppyPoScore = 0;
    private int totalBusinessQuizScore = 0;

    private int currentIndex;

    void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;

        //The index of the FPS minigame
        if (currentIndex == 3) {
            FPSScore = 0;
            PlayerPrefs.SetInt("Total FPS Score", FPSScore);

        }
        //The index of the Drone minigame
        if (currentIndex == 4)
        {
            DroneScore = 0;
            PlayerPrefs.SetInt("Total Drone Score", DroneScore);

        }
        //The index of the FloppyPo minigame
        if(currentIndex == 5)
        {
            FloppyPoScore = 0;
            PlayerPrefs.SetInt("Total FloppyPo Score", FloppyPoScore);
        }
        //The index of the business quiz scene
        if(currentIndex == 7)
        {
            BusinessQuizScore = 0;
            PlayerPrefs.SetInt("Total Business Quiz Score", BusinessQuizScore);
        }
        //The index of the game dev quiz scene
        if(currentIndex == 8)
        {
            GameDevQuizScore = 0;
            PlayerPrefs.SetInt("Total GameDev Quiz Score", GameDevQuizScore);
        }
        //The index of the csharp scene
        if(currentIndex == 9)
        {
            CSharpQuizScore = 0;
            PlayerPrefs.SetInt("Total CSharp Quiz Score", CSharpQuizScore);
        }

        else
        {
            totalDroneScore = PlayerPrefs.GetInt("Total Drone Score");
            totalFPSScore = PlayerPrefs.GetInt("Total FPS Score");
            totalFloppyPoScore = PlayerPrefs.GetInt("Total FloppyPo Score");
            totalBusinessQuizScore = PlayerPrefs.GetInt("Total Business Quiz Score");

        }


        Debug.Log("Total drone score is " + totalDroneScore);
        Debug.Log("Total FPS score is " + totalFPSScore);
        Debug.Log("Total FloppyPo score is " + totalFloppyPoScore);
        Debug.Log("Total Business Quiz Score is " + totalBusinessQuizScore);
    }


    void Update()
    {

        PlayerPrefs.SetInt("Total Drone Score", DroneScore);
        PlayerPrefs.SetInt("Total FPS Score", FPSScore);
        PlayerPrefs.SetInt("Total FloppyPo Score", FloppyPoScore);
        PlayerPrefs.SetInt("Total Business Quiz Score", BusinessQuizScore);
        PlayerPrefs.SetInt("Total GameDev Quiz Score", GameDevQuizScore);
        PlayerPrefs.SetInt("Total CSharp Quiz Score", CSharpQuizScore);

    }
}
