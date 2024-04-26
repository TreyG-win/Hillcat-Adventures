using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class blink : MonoBehaviour
{
    public Image image;

    float yet;
    public float interval = 1.0f;

    void Update()
    {
        yet += Time.deltaTime;

        if (yet >= interval)
        {
            yet -= interval;

            blinking();
        }

    }

    private void blinking()
    {
        if (image.enabled == true)
        {
            image.enabled = false;
        } else
        {
            image.enabled = true;
        }
    }
}
