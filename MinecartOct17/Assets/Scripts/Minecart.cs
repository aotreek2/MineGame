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
    public float maxSpeed = 10f;
    private float moveHorizontal;
    public float brakeForce = 100f;
    public float jumpForce = 200f;

    public AudioSource crash;
    public AudioSource movement;

  

    //Caleb change -- added this bool for checking breaking.
    public bool isBreaking = false;
    public bool isJumping = false;

    public TMP_Text healthUI;
    public TMP_Text minecartHealthUI;
    public UnityEngine.UI.Slider healthSlider;
    public UnityEngine.UI.Slider minecartHealthSlider;

    public ParticleSystem FrontWheelParticle;
    public ParticleSystem BackWheelParticle;


    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        movement.Play();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            isBreaking = true;
            movement.Pause();
            if (rb.velocity.x > 2)
            {
                rb.AddForce(Vector2.left * brakeForce);
                if (!BackWheelParticle.isEmitting && !isJumping)
                {
                    BackWheelParticle.Play();
                    FrontWheelParticle.Play();
                }
            }
            else
            {
                BackWheelParticle.Stop();
                FrontWheelParticle.Stop();
            }

            transform.GetComponent<Animator>().Play("MinecartStopped");
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isBreaking = false;
            //movement.Pause();
            transform.GetComponent<Animator>().Play("MinecartRolling");

            BackWheelParticle.Stop();
            FrontWheelParticle.Stop();
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
            if (rb.velocity.magnitude >= maxSpeed)
            {

            }
            else
            {
                rb.AddForce(Vector2.right * fowardSpeed, ForceMode2D.Impulse);
            }
        }

        if (rb.velocity.x > 1)
        {
            movement.UnPause();
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
        else
        {
            
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))        // Handles collisions of the enemies (Zombies, bats, etc.)
        {
            Zombie zombie = collision.gameObject.GetComponent<Zombie>();


            minecartHealthSlider.value -= 10;
            minecartHealthUI.text = minecartHealthSlider.value + "/" + minecartHealthSlider.maxValue;

            if (minecartHealthSlider.value <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (zombie != null && rb.velocity.magnitude >= 5)
            {
                zombie.transform.parent.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                zombie.Death();  // calls the death method from the zombie script 
                crash.Play();

            }
            else
            {
                healthSlider.value -= 5;
                healthUI.text = healthSlider.value + "/" + healthSlider.maxValue;

                Vector2 pushBackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(pushBackDirection * 10, ForceMode2D.Impulse);   // Knockback when the cart hits the zombie at max speed
            }
        }

        if (collision.gameObject.CompareTag("Hazard") || collision.gameObject.CompareTag("rock"))
        {
            crash.Play();
            movement.Pause();

            minecartHealthSlider.value -= 10;
            minecartHealthUI.text = minecartHealthSlider.value + "/" + minecartHealthSlider.maxValue;

            if (healthSlider.value <= 0)
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


