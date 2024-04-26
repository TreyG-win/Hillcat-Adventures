using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [RequireComponent(typeof(BoxCollider))]
    public class DJI_Drone_Engine : MonoBehaviour, IEngine
    {
        #region Variables
        [Header("Engine Properties")]
        [SerializeField]private float maxPower = 4f;

        [Header("Propeller Properties")]
        [SerializeField] private Transform propeller;
        [SerializeField] private float propRotationSpeed = 300f;
        #endregion
        #region Interface Functions
        public void InitEngine(){

        }

        public void UpdateEngine(Rigidbody rb, DJI_Drone_Inputs input){
            //Debug.Log("Running Engine:  " + gameObject.name);
            Vector3 upVector = transform.up;
            upVector.x = 0f;
            upVector.z = 0f;
            float diff = 1 - upVector.magnitude;
            float finalDifference = Physics.gravity.magnitude * diff;

            Vector3 engineForce = Vector3.zero;
            engineForce = transform.up * ((rb.mass * Physics.gravity.magnitude + finalDifference) + (input.Throttle * maxPower))/4f;

            rb.AddForce(engineForce, ForceMode.Force);
        }

        public void HandlePropellers(){
            if(!propeller){
                return;
            }
            propeller.Rotate(Vector3.up, propRotationSpeed);

        }
        #endregion
    }

