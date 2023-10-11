using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerTest : MonoBehaviour
{
    private Animator animator;
    private bool isDucking;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // when player presses S, miner ducks
        {
            if (!isDucking)
            {
                // Trigger the "Duck" animation
                animator.SetTrigger("Duck");
                isDucking = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) // when player presses W, miner rises
        {
            if (isDucking)
            {
                // Trigger the "RiseFromDuck" animation
                animator.SetTrigger("Rising");
                isDucking = false;
            }
        }

    }
}
