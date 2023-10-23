using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MinerTest : MonoBehaviour
{
    private Animator animator;
    private bool isDucking;
    private Collider2D standingCollider;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        standingCollider = GetComponent<Collider2D>();
        
        //healthMinerUI.text = "Health: " + health;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // when player presses S, miner ducks
        {
            if (!isDucking)
            {
                // Trigger the "Duck" animation
                //animator.SetTrigger("Duck");
                animator.Play("Miner Ducking");
                isDucking = true;
                standingCollider.enabled = false;
                
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (isDucking)
            {
                // Trigger the "RiseFromDuck" animation
                //animator.SetTrigger("Rising");
                animator.Play("Miner Rising");
                isDucking = false;
                standingCollider.enabled = true;
            }
        }
    }
}

