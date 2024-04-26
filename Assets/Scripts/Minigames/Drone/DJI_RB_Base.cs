using System.Collections;
using System.Collections.Generic;
using UnityEngine;


     [RequireComponent(typeof(Rigidbody))]
    public class DJI_RB_Base : MonoBehaviour
    {
       #region  Variables
       [Header("Rigidbody Properties")]
       [SerializeField] private float WeightInlbs = 1f;


       const float lbsToKg = 0.454f;
       protected Rigidbody rb;
       protected float startDrag;
       protected float startAngularDrag;

       #endregion

       #region Required Functions
           void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if(rb){
                rb.mass = WeightInlbs * lbsToKg;
                startDrag = rb.drag;
                startAngularDrag = rb.angularDrag;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(!rb){
                return;
            }

            HandlePhysics();
        }
        #endregion

        #region Physics Methods
        protected virtual void HandlePhysics(){
        }
        #endregion
    }

