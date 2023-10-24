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

    private float damageCooldown = 0;
    private float cartDamageCooldown = 0;

    public AudioSource[] minerDamage;

    public MultiSceneScores multiSceneScores;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement.Play();

        // Sets their health to the starting value.
        healthSlider.value = MultiSceneScores.totalHealth;
        healthUI.text = healthSlider.value + "/" + healthSlider.maxValue;
        minecartHealthSlider.value = MultiSceneScores.totalMinecartHealth;
        minecartHealthUI.text = minecartHealthSlider.value + "/" + minecartHealthSlider.maxValue;

        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            isBreaking = true;
            movement.Pause();
            if (rb.velocity.x > 1.3)
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
            if (!isJumping)
            {
                transform.GetComponent<Animator>().Play("MinecartRolling");
            }
            
        }

        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
        }
        if (cartDamageCooldown > 0)
        {
            cartDamageCooldown -= Time.deltaTime;
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

    public void TakeHealthDamage(int amount)
    {
        if (damageCooldown <= 0)
        {
            minerDamage[Random.Range(0, 3)].Play();
            healthSlider.value -= amount;
            healthUI.text = healthSlider.value + "/" + healthSlider.maxValue;

            MultiSceneScores.totalHealth = (int)healthSlider.value;
            damageCooldown = 1f;

            if (healthSlider.value <= 0)
            {
                multiSceneScores.ResetStats();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Dying
            }
        }
    }

    public void TakeCartDamage(int amount)
    {
        if (cartDamageCooldown <= 0)
        {
            minecartHealthSlider.value -= amount;
            minecartHealthUI.text = minecartHealthSlider.value + "/" + minecartHealthSlider.maxValue;

            MultiSceneScores.totalMinecartHealth = (int)minecartHealthSlider.value;
            cartDamageCooldown = 0.3f;

            if (minecartHealthSlider.value <= 0)
            {
                multiSceneScores.ResetStats();
                print(MultiSceneScores.totalMinecartHealth);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Dying
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie")) // If player hits ZOMBIE
        {
            Zombie zombie = collision.gameObject.GetComponent<Zombie>();

            TakeCartDamage(10);


            if (zombie != null && rb.velocity.magnitude >= 5) //Hitting at max speed
            {
                zombie.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                zombie.Death();  // calls the death method from the zombie script 
                crash.Play();
            }
            else
            {
                TakeHealthDamage(5);

                Vector2 pushBackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(pushBackDirection * 10, ForceMode2D.Impulse);   // Knockback when the cart hits the zombie
            }
        }
        else if (collision.gameObject.CompareTag("StrongZombie")) // Strong zombie
        {
            Zombie strongZombie = collision.gameObject.GetComponent<Zombie>();
            TakeCartDamage(10);

            if (strongZombie != null && rb.velocity.magnitude >= 5)
            {
                strongZombie.TakeDamage(10);
                crash.Play();
                Vector2 pushBackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(pushBackDirection * 55, ForceMode2D.Impulse);
            }
            else
            {
                TakeHealthDamage(5);

                Vector2 pushBackDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(pushBackDirection * 20, ForceMode2D.Impulse);   // Knockback when the cart hits the zombie
            }
        }

        if (collision.gameObject.CompareTag("Hazard") || collision.gameObject.CompareTag("rock")) // If player hits ROCK
        {
            crash.Play();
            movement.Pause();
            transform.GetComponent<Animator>().Play("MinecartStopped");

            TakeCartDamage(10);
        }
        else if(collision.gameObject.CompareTag("ammoBox"))
        {
            crash.Play();
            movement.Pause();
            transform.GetComponent<Animator>().Play("MinecartStopped");

            transform.GetChild(2).transform.GetChild(0).GetComponent<PlayerAimWeapon>().GainAmmo();
            if(MultiSceneScores.machineGunUnlocked)
            {
                transform.GetChild(3).transform.GetChild(0).GetComponent<PlayerAimMachineGun>().GainAmmo();
            }

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("bat")) // If player hits BAT
        {
            Bat bat = collision.gameObject.GetComponent<Bat>();


            TakeHealthDamage(5);

            Vector2 pushBackDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(pushBackDirection * 10, ForceMode2D.Impulse);   // Knockback when the cart hits the bat
        }

        if (isJumping)
        {
            if (collision.gameObject.CompareTag("track"))
            {
                isJumping = false;
            }
        }

    }
}


