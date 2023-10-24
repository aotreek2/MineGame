using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MinerTest : MonoBehaviour
{
    private Animator animator;
    private Collider2D standingCollider;

    private Switching switchScript;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        standingCollider = GetComponent<Collider2D>();

        switchScript = transform.parent.GetComponent<Switching>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // when player presses S, miner ducks
        {
            if (!switchScript.isDucking)
            {
                animator.Play("Miner Ducking");
                switchScript.isDucking = true;
                standingCollider.enabled = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (switchScript.isDucking)
            {
                animator.Play("Miner Rising");
                switchScript.isDucking = false;
                standingCollider.enabled = true;
            }
        }
    }
}

