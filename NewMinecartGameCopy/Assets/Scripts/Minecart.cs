using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Minecart : MonoBehaviour
{
    Rigidbody2D rb;

    public float fowardSpeed = 5f;
    public float maxSpeed = 20f;
    private float moveHorizontal;
    public float brakeForce = 100f;
    public float jumpForce = 200f;
    private float currentSpeed;
    int health = 100;

  

    //Caleb change -- added this bool for checking breaking.
    public bool isBreaking = false;
    public bool isJumping = false;

    public TMP_Text healthUI;
    public TMP_Text minecartHealthUI;
    public UnityEngine.UI.Slider healthSlider;
    public UnityEngine.UI.Slider minecartHealthSlider;



    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        float speed = rb.velocity.magnitude;

        // This apparently wasn't being used?
        //float moveHorizontal = Input.GetAxisRaw("Horizontal");


        if (Input.GetKey(KeyCode.A))
        {
            isBreaking = true;
            if (rb.velocity.x > 1)
            {
                rb.AddForce(Vector2.left * brakeForce);
            }

            
        }
        else
        {
            isBreaking = false;
        }


        FixedUpdate();
        Debug.Log("Speed" + rb.velocity.normalized + "");



        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }


    private void FixedUpdate()
    {
        if (!isBreaking)
        {
            // cart movement

            // This apparently wasn't being used?
            //float moveHorizontal = Input.GetAxisRaw("Horizontal");



            //rb.AddForce(Vector2.right * fowardSpeed, 0);

            //Caleb change - by directly setting the value of velocity, some of the addforce from Jump() is overridden.

            //if (rb.velocity.magnitude > maxSpeed)
            //{
            //    rb.velocity = rb.velocity.normalized * maxSpeed;
            //}


            // Using this seems to do the same thing without taking away jump momentum while the player moves
            if (rb.velocity.magnitude > maxSpeed)
            {

            }
            else
            {
                rb.AddForce(Vector2.right * fowardSpeed, 0);
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
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Zombie"))        // Handles collisions of the enemies (Zombies, bats, etc.)
        {
            healthSlider.value -= 5;
            healthUI.text = healthSlider.value + "/" + healthSlider.maxValue;

            if (healthSlider.value <= healthSlider.minValue)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (collision.gameObject.CompareTag("Hazard"))
        {
            healthSlider.value -= 5;
            healthUI.text = healthSlider.value + "/" + healthSlider.maxValue;

            if (healthSlider.value <= healthSlider.minValue)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }


        }

        if (isJumping)
        {
            if(collision.gameObject.CompareTag("track"))
            {
                isJumping = false;
            }
        }

    }
}


