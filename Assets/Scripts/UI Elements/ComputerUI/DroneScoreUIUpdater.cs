using UnityEngine;
using UnityEngine.UI;

public class DroneScoreUIUpdater : MonoBehaviour
{
    public Text droneScoreText; // Reference to the UI Text element

    void Update()
    {
        // Check if the droneScoreText is assigned and the ScoreTracker script exists

        droneScoreText.text = ScoreTracker.DroneScore.ToString();

    }
}
