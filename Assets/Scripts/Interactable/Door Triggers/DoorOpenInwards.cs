using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is designed to be placed onto
 * a door that is desired to be opened or closed.
 * 
 * Note: This will be better if you place this script
 * onto the hinge of a door with an empty game object
 * acting as the hinge. Otherwise, the door's pivot point
 * might make it rotate in the wrong manner. More specifically,
 * the empty game object should be the parent of the door so that
 * when the empty game object rotates, it takes the door with it.
 * 
 */
public class DoorOpenInwards : MonoBehaviour
{
    /* 
     * How far the door will rotate open.
     * This can be adjusted in the inspector.
     */
    [Header("Target Rotation")]
    [SerializeField] float MaxRotationAngle;

    [SerializeField] Collider doorCollider;

    Transform localTrans;

    //Is the door open or not?
    private bool open;

    private void Start()
    {
        localTrans = GetComponent<Transform>();
    }

    //Opens the door if it is not open already.
    public void Activate()
    {
        //Moves the door in the direction assigned in the inspector
        //then assigns the door as open.
        if (!open)
        {
            open = true;
        }
    }
    //Closes the door if it is not closed already.
    public void Deactivate()
    {
        //Moves the door in the direction assigned in the inspector
        //then assigns the door as closed.
        if (open)
        {
            open = false;
        }
    }
    /*
     * This clamps the rotation of an object by
     * a desired axis. In this case, it is the y axis.
     * If you want the door to rely on a different axis, then
     * every instance of ".y" should be swapped to that instead.
     */
    private void RotLock()
    {
        Vector3 doorEurlerAngle = localTrans.rotation.eulerAngles;

        doorEurlerAngle.y = (doorEurlerAngle.y > 180) ? doorEurlerAngle.y - 360 : doorEurlerAngle.y;
        doorEurlerAngle.y = Mathf.Clamp(doorEurlerAngle.y, MaxRotationAngle, 0);

        localTrans.rotation = Quaternion.Euler(doorEurlerAngle);
    }

    private void Update()
    {
        //When the player enters the box collider, then the door will open.
        if (open)
        {
            doorCollider.enabled = false;
            transform.Rotate(Vector3.up, MaxRotationAngle * Time.deltaTime);
            RotLock();
        }

        //When the player exits the collider, then the door will close.
        else if (!open)
        {
            doorCollider.enabled = true;
            transform.Rotate(Vector3.up, -MaxRotationAngle * Time.deltaTime);
            RotLock();

        }
    }
}
