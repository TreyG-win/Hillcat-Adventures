using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/*we want the player to interact with this object to spawn an item.*/
public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

public override void Interact(Player player)
    {
        if(!player.HasKitchenObject()){
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
public override void InteractAlternate(Player player){

}

    
}
