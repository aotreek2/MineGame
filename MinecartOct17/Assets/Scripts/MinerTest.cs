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
    private Collider2D duckingCollider;
    int health = 50;
    public TMP_Text healthMinerUI;

    void Start()
    {
        animator = GetComponent<Animator>();
        standingCollider = GetComponent<Collider2D>();
        healthMinerUI.text = "Health: " + health;

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

        //if (Input.GetKeyDown(KeyCode.W)) // when player presses W, miner rises
        //{
        //    if (isDucking)
        //    {
        //        // Trigger the "RiseFromDuck" animation
        //        animator.SetTrigger("Rising");
        //        isDucking = false;
        //        standingCollider.enabled = true;
        //        
        //    }
        //}

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))        // Handles collisions of the enemies (Zombies, bats, etc.)
        {
            health = health - 10;
            healthMinerUI.text = "Health: " + health;
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (collision.gameObject.CompareTag("Hazard"))
        {
            health = health - 10;
            healthMinerUI.text = "Health: " + health;
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);  //Handles collisions of the hazards.
            }
        }

    }
}

