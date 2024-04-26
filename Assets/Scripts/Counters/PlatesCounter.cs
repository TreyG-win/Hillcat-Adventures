using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatesCounter : BaseCounter
{

public event EventHandler OnPlateSpawned;//<= event to let us know a new plate has spawned.
public event EventHandler OnPlateRemoved;//<= event to let us know a plate has been removed.

 [SerializeField] private KitchenObjectSO plateKitchenObjectSO; //<= we could put anything here, but we want Plates.  
 private float spawnPlateTimer;
 private float spawnPlateTimerMax = 4f;
 private int platesSpawnAmount;
 private int platesSpawnedAmountMax = 4;
 private void Update(){
    spawnPlateTimer += Time.deltaTime;
    if(spawnPlateTimer > spawnPlateTimerMax){ //<=simple time function for our spawner.
        spawnPlateTimer = 0f;

        if(platesSpawnAmount < platesSpawnedAmountMax){
            platesSpawnAmount++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);//<=when we fire the event, we don't need to know anything. ARGS can be empty.
        }
    }
 }

  public override void Interact(Player player){
        if(!player.HasKitchenObject()){
            //player is carrying nothing.

            if(platesSpawnAmount > 0){
                //There is at least one plate availible.
                platesSpawnAmount--;//<= decrease the amount of plates.
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);//<=Spawn a plate int he player's hand.
                OnPlateRemoved?.Invoke(this, EventArgs.Empty); //<= send event OnPlateRemoved
                
            }
        }
    }
}
