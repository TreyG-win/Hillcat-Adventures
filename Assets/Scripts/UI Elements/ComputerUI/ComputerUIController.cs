using UnityEngine;
using UnityEngine.UI;

public class ComputerUIController : MonoBehaviour
{
    public GameObject uiCanvas; // Reference to the canvas containing the UI text
    

    private bool playerInRange = false;

    void Start()
    {
        // Deactivate the UI canvas at the start
        uiCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            uiCanvas.SetActive(true); // Activate the UI canvas
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            uiCanvas.SetActive(false); // Deactivate the UI canvas
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.C))
        {
            // Add code here to open the computer UI
            Debug.Log("Computer UI opened!");
            uiCanvas.SetActive(false);
        }
    }
}
