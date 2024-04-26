using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    public event EventHandler<onSelectedCounterChangeEventArgs> onSelectedCounterChange;
    public class onSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersMask;
    [SerializeField] private Transform KitchenObjectholdPoint;

    private bool isWalking;
   
    private Vector3 lastInteractionDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one player instance!");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
       
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

        private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

       

       

    private void Update()
    {
        //HandleMovement();
        HandleIntereactions();
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleIntereactions()
    {
        Vector2 inputVector = gameInput.GetMovmentVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractionDir = transform.forward;
        }
        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactDistance, countersMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //has clear counter.
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);

                   
                }

            }
            else
            {
                SetSelectedCounter(null);
                
            }

        }
        else
        {
            SetSelectedCounter(null);
        }
        Debug.DrawRay(transform.position,
        transform.forward * 5f, Color.red);
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovmentVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerWidth = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, moveDir, moveDistance);

        if (!canMove)
        {
            // cannot move towards direction 
            // attempt X movment
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, moveDirX, moveDistance);
            if (canMove)
            {
                //can only move on X
                moveDir = moveDirX;
            }
            else
            {
                //cannot move on X
                //Attempt Z movement 
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerWidth, moveDirZ, moveDistance);
                if (canMove)
                {
                    //can move on Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move


                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
         
        }
        isWalking = moveDir != Vector3.zero;

        float rotSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotSpeed);
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        onSelectedCounterChange?.Invoke(this, new onSelectedCounterChangeEventArgs { selectedCounter = selectedCounter });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectholdPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
