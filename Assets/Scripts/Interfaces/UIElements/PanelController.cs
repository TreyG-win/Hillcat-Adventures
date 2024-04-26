using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject coursePanel; // Assign CoursePanel here
    public GameObject coursePanel1; // Assign CoursePanel1 here
    public GameObject coursePanel2; // Assign CoursePanel2 here

    void Start()
    {
        // Ensure the course panels are not visible at start
        coursePanel.SetActive(false);
        coursePanel1.SetActive(false);
        coursePanel2.SetActive(false);
    }

    void Update()
    {
        // Toggle CoursePanel visibility on 'K' press
        if (Input.GetKeyDown(KeyCode.K))
        {
            bool shouldBeVisible = !coursePanel.activeSelf;
            coursePanel.SetActive(shouldBeVisible);

            // If CoursePanel is being closed, close CoursePanel1 and CoursePanel2 as well
            if (!shouldBeVisible)
            {
                CloseAllCoursePanels();
            }
        }

        // Open CoursePanel1 on '1' key press, ensuring CoursePanel is open
        if (Input.GetKeyDown(KeyCode.Alpha1) && coursePanel.activeSelf)
        {
            OpenCoursePanel1();
        }

        // Open CoursePanel2 on '2' key press, ensuring CoursePanel is open
        if (Input.GetKeyDown(KeyCode.Alpha2) && coursePanel.activeSelf)
        {
            OpenCoursePanel2();
        }
    }

    // Method to open CoursePanel1 and ensure it's the only panel open
    public void OpenCoursePanel1()
    {
        Debug.Log("OpenCoursePanel1 called");
        coursePanel1.SetActive(true);
        coursePanel2.SetActive(false); // Ensure only one panel is open at a time
    }

    // Method to open CoursePanel2 and ensure it's the only panel open
    public void OpenCoursePanel2()
    {
        Debug.Log("OpenCoursePanel2 called");
        coursePanel2.SetActive(true);
        coursePanel1.SetActive(false); // Ensure only one panel is open at a time
    }

    // Helper method to close all course panels
    private void CloseAllCoursePanels()
    {
        coursePanel1.SetActive(false);
        coursePanel2.SetActive(false);
    }
}
