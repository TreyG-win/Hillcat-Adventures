using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFinishEnd : MonoBehaviour
{
private void OnTriggerEnter(Collider collision)
{
    if(collision.gameObject.tag == "Drone")
   Destroy(collision.gameObject);
   GameStateManager.MainState = MainState.Free;
}
}
