using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation_M : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Example method to start walking animation
    public void StartWalking()
    {
        animator.SetTrigger("walking");
    }

    // Example method to stop walking animation
    public void StopWalking()
    {
        animator.SetTrigger("Idle");
    }
}
