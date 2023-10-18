using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Minecart : MonoBehaviour
{
    Rigidbody2D rb;

    public float forwardSpeed = 5f;
    public float maxSpeed = 5f;
    private float moveHorizontal;
    public float brakeForce = 100f;
    public float jumpForce = 200f;
    int cartHealth = 100;
    public TMP_Text healthUI;
    public AudioSource movement;
    public AudioSource crash;



    //Caleb change -- added this bool for checking breaking.
    public bool isBreaking = false;
    public bool isJumping = false;



    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        healthUI.text = "Health: " + cartHealth;
        movement.Play();


    }

    // Update is called once per frame
    void Update()
    {
        FixedUpdate();

        if (Input.GetKey(KeyCode.A))
        {
            isBreaking = true;
            
        }
        else
        {
            isBreaking = false;
            
            
        }



        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }


    private void FixedUpdate()
    {
        if (!isBreaking)
        {
            if(rb.velocity.x >= maxSpeed)
            {

            }
            else
            {
                rb.AddForce(Vector2.right * forwardSpeed, 0);

            }       
        }

    }

    private void Jump()
    {
        if (!isJumping)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce)); // makes the minecart jump
            transform.GetComponent<Animator>().Play("MinecartJump");
            isJumping = true;
            movement.Pause();
        }
        else
        {
            movement.Play();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Zombie"))        // Handles collisions of the enemies (Zombies, bats, etc.)
        {
            Zombie zombie = collision.gameObject.GetComponent<Zombie>();

            cartHealth -= 10;
            healthUI.text = "Health: " + cartHealth;
            if (cartHealth == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (zombie != null && rb.velocity.magnitude >= 5)
            {
                cartHealth -= 10;
                zombie.Death(); // calls the death method from the zombie script 
                Vector2 pushBackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(pushBackDirection * 10, ForceMode2D.Impulse);   // Knockback when the cart hits the zombie at max speed
            }
        }

        if (collision.gameObject.CompareTag("Hazard"))
        {
            crash.Play();
            cartHealth -= 10;
            healthUI.text = "Health: " + cartHealth;
            if (cartHealth == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);  //Handles collisions of the hazards.
            }


        }

        if (isJumping)
        {
            if(collision.gameObject.CompareTag("track"))
            {
                isJumping = false;
                movement.Play();
            }
        }

    }
}


