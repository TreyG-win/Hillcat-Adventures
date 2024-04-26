using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    /*enums are a limited list of things like states or options 
    that you want to select only one of at a time.*/
    private enum Mode{
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted

    }

    [SerializeField] private Mode mode; //<=creates a drop down menu of options in the inspector.

    /*transform object to point at camera on a late update, after the target camera has moved.*/
    private void LateUpdate(){
        switch(mode){
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform); //<= Note: Camera.main is now cached in Unity, and no longer causes the script to cycle through all objects in a scene.
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }

            }
}
