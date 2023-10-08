using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minecart : MonoBehaviour
{
    Rigidbody2D rb;

    public float fowardSpeed = 5f;
    private float moveHorizontal;
    public float brakeForce = 2f;
    public float jumpForce = 20f;
    int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");


        // Apply forward force when 'D' is pressed
        if (moveHorizontal > 0)
        {
            FixedUpdate();
        }
        // Apply braking force when 'A' is pressed
        else if (moveHorizontal < 0)
        {
            rb.velocity *= 0.8f;
        }


        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }


    private void FixedUpdate()
    {
        // cart movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        rb.AddForce(Vector2.right * moveHorizontal * fowardSpeed, 0);


    }

    private void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce)); // makes the minecart jump
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Hazard"))
        {
            health = health - 100;
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);  //Handles collisions of the hazards.
            }
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))        // Handles collisions of the enemies (Zombies, bats, etc.)
        {
            health = health - 100;
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }
}


