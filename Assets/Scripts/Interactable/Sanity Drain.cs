using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityDrain : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Managers.Player.ChangeSanity(-25);
        }

    }
}
