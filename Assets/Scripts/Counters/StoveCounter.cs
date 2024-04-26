using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; //<=sets up the event handler with the IHasProgress interface. Events let other scripts know what happened.
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged; //<= sets up the state changed event.
    public class OnStateChangedEventArgs : EventArgs{
        public State state; //<=passes on the FSM state through the event.
    }

    /*enums for the finite state machine, enums here help keep track of what state the stovecounter is in.*/
    public enum State{
        Idle,
        Frying,
        Fried,
        Burned
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;//<=variable that holds the current state of our FSM

    private float fryingTimer;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;
    private FryingRecipeSO fryingRecipeSO;

    private void Start(){
        state = State.Idle; //<= set the default state when the game starts.
    }

    /*Counter changes the item after a defined period of time.*/
    private void Update(){
        if(HasKitchenObject()){ //<=is there an object on the stovecounter?
            switch(state){
                case State.Idle:
                    break;
                case State.Frying:
                fryingTimer += Time.deltaTime; //<=how much time has passed
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax});
                if(fryingTimer > fryingRecipeSO.fryingTimerMax){  //<=compare the time passed with the max time allowed.
                    //fried
                    GetKitchenObject().DestroySelf();//<=Destroy old object when timer is up
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);//<Spawn new object when timer is up, grab reference from the recipe SO.
                    
                    state = State.Fried; //<=change state after stovecounter is done frying.
                    burningTimer = 0f;//<=reset the burning timer.
                    burningRecipeSO = GetBurningingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = state}); //<= invoke the event on state changed, and pass This state on.
                }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime; //<=how much time has passed
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{progressNormalized = burningTimer / burningRecipeSO.BurningTimerMax});
                if(burningTimer > burningRecipeSO.BurningTimerMax){  //<=compare the time passed with the max time allowed.
                    //fried
                    GetKitchenObject().DestroySelf();//<=Destroy old object when timer is up
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);//<Spawn new object when timer is up, grab reference from the recipe SO.
                    state = State.Burned; //<=change state after stovecounter is done frying.
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = state}); //<= invoke the event on state changed, and pass This state on.
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{progressNormalized = 0f});
                }
                    break;
                case State.Burned:
                    break;
                }
            }
        }

public override void Interact(Player player){
    if(!HasKitchenObject()){
            //The counter top has no item
            if(player.HasKitchenObject()){
                                //The player is carrying an item.
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                        //The player has an item that can be cut.
                player.GetKitchenObject().SetKitchenObjectParent(this);
                fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());//<=Get the recipe SO
                state = State.Frying; //<=set the state to frying.
                fryingTimer = 0f;//<=reset elapsed time. 
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = state}); //<= invoke the event on state changed, and pass This state on.
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax});
                }
            }else{
                //player has nothing   
            }
        }else{
            //Counter top already has an item.
            if(player.HasKitchenObject()){
                //the player is carrying an item

            }else{
                //the player is not currently carrying anything.
                GetKitchenObject().SetKitchenObjectParent(player);//<= pick up the object.
                state = State.Idle;//<=reset the state to Idle.
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = state}); //<= invoke the event on state changed, and pass This state on.
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{progressNormalized = 0f});//<=sets the progressbar back to 0.
            }
        }
   }

   /*The usual mutators for the recipe SO.*/
   private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO){
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
    return fryingRecipeSO != null;
    }

   /*function to find the object we have placed on the counter so we can match it to a recipe*/
   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
    if(fryingRecipeSO != null){
        return fryingRecipeSO.output;
    }else{
        return null;
    }
   }
   private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray){
        if (fryingRecipeSO.input == inputKitchenObjectSO){
            return fryingRecipeSO;
        }
    }
    return null;

   }

      private BurningRecipeSO GetBurningingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray){
        if (burningRecipeSO.input == inputKitchenObjectSO){
            return burningRecipeSO;
        }
    }
    return null;

   }
}

