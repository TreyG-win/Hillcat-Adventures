using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;      
    public KitchenObjectSO GetKitchenObjectSO() { return KitchenObjectSO; }

    /*SetKitchenObjectParent: 'kitchenObjectParent' is the new object being passed into it. 'this' is the object it already has.
    If we want to add a new object we first need to clear 'this' before we can place a 'kitchenObjectParent' object.*/
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null){
            this.kitchenObjectParent.ClearKitchenObject(); //<= clear the old object if we have one.
        }

        this.kitchenObjectParent = kitchenObjectParent; //<= take the kitchenObjectParent object and set it to 'this'

        if (kitchenObjectParent.HasKitchenObject()){
            Debug.LogError("Counter already has a KitchenObject!");
        }else{

        kitchenObjectParent.SetKitchenObject(this); //<= place 'this'

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform(); //<= place on the child transform.
        transform.localPosition = Vector3.zero; //<= set local transform to 0.
        }
    }
    public IKitchenObjectParent GetKitchenObjectParent() { return kitchenObjectParent; }  //<= Return our object.


    public void DestroySelf(){
        kitchenObjectParent.ClearKitchenObject(); //<= don't forget to clear the counter.
        Destroy(gameObject); //<= destroys the game object. 
    }

    /*spawn new object after previous is destroyed.*/ 
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent){
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab); 
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;

    }
}


