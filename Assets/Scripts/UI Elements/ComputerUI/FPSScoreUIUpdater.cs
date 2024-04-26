using UnityEngine;
using UnityEngine.UI;

public class FPSScoreUIUpdater : MonoBehaviour
{
    public Text FPSScoreText; // Reference to the UI Text element

    void Update()
    {
        // Check if the FPSScoreText is assigned and the ScoreTracker script exists
        
        {
            // Update the UI Text with the current FPSScore
            FPSScoreText.text = ScoreTracker.FPSScore.ToString();
        }
    }
}
