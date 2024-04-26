using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


    [RequireComponent(typeof(DJI_Drone_Inputs))]
    public class DJI_Drone_Controller : DJI_RB_Base
    {

        #region Variables
        [Header("Control Properties")]
        [SerializeField]private float minMaxPitch = 30;
        [SerializeField]private float minmaxRoll = 30f;
        [SerializeField]private float yawPower = 4f;

        private float finalPitch;
        private float finalRoll;
        private float finalYaw;
        private float yaw;
        [SerializeField] private float lerpSpeed = 2f;
        private DJI_Drone_Inputs input;
        private List<IEngine> engines = new List<IEngine>();

        //private bool inZone = false;
        #endregion

        #region Required Functions

        void Start()
        {
            input = GetComponent<DJI_Drone_Inputs>();
            engines = GetComponentsInChildren<IEngine>().ToList<IEngine>();
        }
    #region Scoring System
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ScoringTargets" && gameObject.tag != "Drone Engine")
        {
            //inZone = true;
            Destroy(other.gameObject);

            ScoreTracker.DroneScore++;
            Debug.Log("The current score inside the minigame is " + ScoreTracker.DroneScore);
            /*if (inZone)
            {


                inZone = false;
            }*/


        }
    }
    #endregion

    #endregion

    #region  Physics Methods
    protected override void HandlePhysics(){
            HandleEngines();
            HandleControls();
            
        }
        protected virtual void HandleEngines(){
            //rb.AddForce(Vector3.up *(rb.mass * Physics.gravity.magnitude));
            foreach(IEngine engine in engines){
                engine.UpdateEngine(rb, input);
                engine.HandlePropellers();
            }

        }

        protected virtual void HandleControls(){
            float pitch = input.Cyclic.y * minMaxPitch;
            float roll = -input.Cyclic.x * minmaxRoll;
            yaw += input.Pedals * yawPower;
            finalPitch = Mathf.Lerp(finalPitch, pitch, Time.deltaTime * lerpSpeed);
            finalRoll = Mathf.Lerp(finalRoll, roll, Time.deltaTime * lerpSpeed);
            finalYaw = Mathf.Lerp(finalYaw, yaw, Time.deltaTime * lerpSpeed);

            Quaternion rot = Quaternion.Euler(finalPitch, finalYaw, finalRoll);
            rb.MoveRotation(rot);

        }


        #endregion
    }

