using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public Text currentLapText; // UI Text for displaying the current lap
    private int currentCheckpointIndex = 0;
    private int currentLap = 0;
    private bool raceStarted = false;
    private bool raceFinished = false;

    void OnTriggerEnter(Collider other)
    {
        // Ensure the race is active and not finished
        if (raceFinished) return;

        if (other.CompareTag("StartLine"))
        {
            if (!raceStarted)
            {
                // Starting the race
                Debug.Log("Race started!");
                raceStarted = true;
                currentLap = 1;
                UpdateLapUI();
            }
            else
            {
                // Completing a lap
                FinishLap();
            }
        }
        else if (other.CompareTag("CheckPoint") && raceStarted)
        {
            // Passing a checkpoint
            currentCheckpointIndex++;
            Debug.Log("Passed checkpoint #" + currentCheckpointIndex);
        }
    }

    private void FinishLap()
    {
        // Increment lap count and update UI
        currentLap++;
        UpdateLapUI();

        // Reset checkpoint index for the next lap
        currentCheckpointIndex = 0;

        Debug.Log("Lap " + currentLap + " completed.");

        // Example condition to end the race after 3 laps
        if (currentLap >= 3)
        {
            raceFinished = true;
            Debug.Log("Race finished!");
            // Additional logic to handle the race ending
        }
    }

    private void UpdateLapUI()
    {
        // Update the lap count on the UI
        currentLapText.text = "Lap: " + currentLap.ToString();
    }

    // Method to start the race, could be called from a UI button
    public void StartRaceManually()
    {
        if (raceStarted) return;

        raceStarted = true;
        raceFinished = false;
        currentLap = 0;
        currentCheckpointIndex = 0;
        Debug.Log("Race manually started.");
        UpdateLapUI();
    }
}
