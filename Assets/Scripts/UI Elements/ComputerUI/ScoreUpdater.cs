using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreUpdater : MonoBehaviour
{
    [Header("Required Dependencies")]
    [Tooltip("This is the drone score text. It is located in the Canvas > ComputerUI > ComputerUI > Background > Drone > Dronescore.")]
    public Text droneScoreText;
    [Tooltip("This is the FPS score text. It is located in the Canvas > ComputerUI > ComputerUI > Background > Esport > esportscore.")]
    public Text FPSScoreText;
    [Tooltip("This is the FloppyPo score text. It is located in the Canvas > ComputerUI > ComputerUI > Background > FloppyPo > Floppyscore.")]
    public Text FloppyPoText;
    [Tooltip("This is the GameDev score text. It is located in the Canvas > ComputerUI > ComputerUI > Background > Course > GameDevQuizScore.")]
    public TMP_Text GameDevQuizText;

    public TMP_Text cSharpQuizText;

    public TMP_Text BusinessQuizText;

    void Update()
    {
        droneScoreText.text = ScoreTracker.DroneScore.ToString();
        FPSScoreText.text = ScoreTracker.FPSScore.ToString();
        FloppyPoText.text = ScoreTracker.FloppyPoScore.ToString();
        GameDevQuizText.SetText(ScoreTracker.GameDevQuizScore.ToString());
        cSharpQuizText.SetText(ScoreTracker.CSharpQuizScore.ToString());
        BusinessQuizText.SetText(ScoreTracker.BusinessQuizScore.ToString());
    }
}
