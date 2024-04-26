using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if(!HasKitchenObject()){
            //The counter top has no item
            if(player.HasKitchenObject()){
                //The player is carrying an item.
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }else{
                //player has nothing   
            }
        }else{
            //Counter top already has an item.
            if(player.HasKitchenObject()){
                //the player is carrying an item

            }else{
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(Player player){
    
}

}
