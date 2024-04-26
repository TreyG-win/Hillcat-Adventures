using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is designed for testing out the ability to restore sanity inside of a zone.
 * This script/GameObject should most likely be removed and replaced with a different means of restoring sanity
 * by being in class (possibly through listening to a lecture?). 
 */

public class SanityGain : MonoBehaviour
{

    private IEnumerator coroutine;
    private bool insideZone;

    private void Start()
    {
        coroutine = HealSanity(0.4f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            insideZone = true;
            if (Managers.Player.sanity != Managers.Player.maxSanity)
            {
                StartCoroutine(coroutine);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            insideZone = false;
        }
    }

    private IEnumerator HealSanity(float delay)
    {
        while(insideZone && Managers.Player.sanity != Managers.Player.maxSanity)
        {
            Managers.Player.ChangeSanity(1);
            yield return new WaitForSeconds(delay);
        }
    }

}
