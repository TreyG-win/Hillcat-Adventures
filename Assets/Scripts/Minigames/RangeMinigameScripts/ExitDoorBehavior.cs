using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Forces win screen upon player reaching the exit door
/// </summary>
public class ExitDoorBehavior : MonoBehaviour
{
    public GameBehaviour gameManager;
    private UIHud UIinfo;

    private void Start()
    {
        UIinfo = gameManager.GetComponent<UIHud>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            UIinfo.ShowWinScreen();

        }
    }
}
