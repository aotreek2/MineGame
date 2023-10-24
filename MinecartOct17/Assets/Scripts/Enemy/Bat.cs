using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public Transform player;

    private float distance;
    public float speed;
    public float distanceBetween = 7;

    private int batHealth = 10;
    public UnityEngine.UI.Slider batSlider;

    public Animator bat;
    private SpriteRenderer batSprite;
    private Rigidbody2D rb;
    public PickaxeManager pickaxeManager;

    public AudioSource flying;
    public AudioSource[] batDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        batSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);   // getting the distance between the player and the bat
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Deg2Rad; 
        // getting and calculating the correct direction for the bat to face
       

        if (distance < distanceBetween)
        {
            if (!flying.isPlaying)
            {
                flying.Play();
            }

            bat.Play("bat");
            Vector2 target = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime); //sets up when the zombie chases the player
            rb.velocity = direction * speed;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            if (direction.x > 0)
            {
                batSprite.flipX = true;
            }
            else
            {
                batSprite.flipX = false;
            }

        }
        else
        {
            flying.Stop();
        }
    }

    public void TakeDamage(int damage)
    {
        batHealth -= damage;
        batSlider.gameObject.SetActive(true);
        batSlider.value = batHealth;

        transform.GetChild(1).transform.GetComponent<ParticleSystem>().Play();
        batDamage[Random.Range(0, 3)].Play();

        if (batHealth <= 0)
        {
            transform.GetChild(1).transform.parent = transform.parent.transform;
            Death();
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        print(distance);
        if (pickaxeManager.active && distance <= 3)
        {
            pickaxeManager.SwingPickaxe(gameObject);
        }
    }
}
