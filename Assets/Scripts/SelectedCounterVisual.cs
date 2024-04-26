using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        Player.Instance.onSelectedCounterChange += Player_onSelectedCounterChange;
    }

    private void Player_onSelectedCounterChange(object sender, Player.onSelectedCounterChangeEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
         foreach (GameObject visualGameObject in visualGameObjectArray)
        visualGameObject.SetActive(false);
    }
}
