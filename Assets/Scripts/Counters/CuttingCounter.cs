using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;//<=use the IHasProgress event call
    public event EventHandler OnCut;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

   public override void Interact(Player player){
    if(!HasKitchenObject()){
            //The counter top has no item
            if(player.HasKitchenObject()){
                                //The player is carrying an item.
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                        //The player has an item that can be cut.
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax});
                }
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
    if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
        //There is an item here AND it can be cut.
        cuttingProgress ++;
        OnCut?.Invoke(this, EventArgs.Empty);
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax});
        if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax){

        KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }

    }
   }

   private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
    return cuttingRecipeSO != null;
    }

   /*function to find the object we have placed on the counter so we can match it to a recipe*/
   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
    if(cuttingRecipeSO != null){
        return cuttingRecipeSO.output;
    }else{
        return null;
    }
   }
   private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray){
        if (cuttingRecipeSO.input == inputKitchenObjectSO){
            return cuttingRecipeSO;
        }
    }
    return null;

   }

}
