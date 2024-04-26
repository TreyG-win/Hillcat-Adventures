using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComputerLoadingUI : MonoBehaviour
{
    public GameObject loadingPanel; // Reference to the loading panel
    public GameObject computerUIPanel; // Reference to the computer UI panel

    private bool computerUIActive = false;

    void Start()
    {
        // Initially disable both panels
        loadingPanel.SetActive(false);
        computerUIPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!computerUIActive)
            {
                // Player pressed 'C' to open the computer UI

                // Activate the loading panel
                loadingPanel.SetActive(true);

                // Start coroutine to show computer UI after one second
                StartCoroutine(ShowComputerUIAfterDelay());
            }
            else
            {
                // Player pressed 'C' to close the computer UI

                // Deactivate the computer UI panel
                computerUIPanel.SetActive(false);
                computerUIActive = false;
            }
        }
    }

    IEnumerator ShowComputerUIAfterDelay()
    {
        // Wait for one second
        yield return new WaitForSeconds(1f);

        // Deactivate the loading panel
        loadingPanel.SetActive(false);

        // Activate the computer UI panel
        computerUIPanel.SetActive(true);
        computerUIActive = true;
    }
}
