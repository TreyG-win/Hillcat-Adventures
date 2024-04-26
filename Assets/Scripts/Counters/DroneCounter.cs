using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/*we want the player to interact with this object to spawn an item.*/
public class DroneCounter : BaseCounter
{
   

[SerializeField] private GameObject DronePrefab;
[SerializeField] private Transform DroneStartPoint;

public override void Interact(Player player)
    {
        if(!player.HasKitchenObject() && GameStateManager.MainState == MainState.Free){
            GameStateManager.MainState = MainState.DroneFree;//<= sets the game state to fly the drone, disables player and changes camera.
            

        //KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        //Transform DroneVisualTransform = Instantiate(DronePrefab, DroneStartPoint);
         GameObject newDrone = Instantiate(DronePrefab,DroneStartPoint.transform.position, Quaternion.identity) as GameObject;
        //OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
public override void InteractAlternate(Player player){//<=Effectivly exits out of drone mode, since the player should be stuck facing the counter.

GameStateManager.MainState = MainState.Free; 
    //We want to destroy the active drone here.
}

}



    
