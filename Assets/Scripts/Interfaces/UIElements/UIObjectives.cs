using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIObjectives : MonoBehaviour
{
    [SerializeField] public GameObject ObjectiveList;
    [SerializeField] public GameObject ListTutorial;

    //public TMP_Text ObjectiveText;

    private void Start()
    {
        ObjectiveList.SetActive(false);
        ListTutorial.SetActive(true);
        
    }

    #region Toggling Tutorial Text
    // Shows/hides the control tutorial for the objective box text
    public void toggleListTutorial()
    {

        if (ListTutorial.activeInHierarchy)
        {
            ListTutorial.SetActive(false);
        }
        else
        {
            ListTutorial.SetActive(true);
        }

    }
    #endregion

    #region Toggling Objective Box
    // Shows/hides the objective box
    public void toggleObjectiveBox()
    {
        if (ObjectiveList.activeInHierarchy == false)
        {
            ObjectiveList.SetActive(true);
        }
        else
        {
            ObjectiveList.SetActive(false);
        }
    }
    #endregion
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            toggleObjectiveBox();
        }
    }
}
