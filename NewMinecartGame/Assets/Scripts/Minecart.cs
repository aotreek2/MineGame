using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Minecart : MonoBehaviour
{
    Rigidbody2D rb;

    public float fowardSpeed = 5f;
    public float maxSpeed = 20f;
    private float moveHorizontal;
    public float brakeForce = 100f;
    public float jumpForce = 20f;
    private float currentSpeed;
    int health = 100;
    public TMP_Text healthUI;
    


    // Start is called before the first frame update
    void Start()
    {
         

        rb = GetComponent<Rigidbody2D>();
        healthUI.text = "Health: " + health;
        
    }

    // Update is called once per frame
    void Update()
    {

        float speed = rb.velocity.magnitude;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");


        
        
            FixedUpdate();
            Debug.Log("Speed" + rb.velocity.normalized);
        
        // Apply braking force when 'A' is pressed
         if (moveHorizontal < 0)
        {
            
                Vector2 brakeDirection = -rb.velocity.normalized;
                Vector2 brakeForceVector = brakeDirection * brakeForce;
                rb.AddForce(brakeForceVector, ForceMode2D.Force);
                Debug.Log("Breaking");
            
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

      rb.AddForce(Vector2.right * fowardSpeed, 0);

      if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
            
        }
      
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce)); // makes the minecart jump
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Zombie"))        // Handles collisions of the enemies (Zombies, bats, etc.)
        {
            health = health - 10;
            healthUI.text = "Health: " + health;
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if(collision.gameObject.CompareTag("Hazard"))
        {
            health = health - 10;
            healthUI.text = "Health: " + health;
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);  //Handles collisions of the hazards.
            }

           
        }

    }
}


