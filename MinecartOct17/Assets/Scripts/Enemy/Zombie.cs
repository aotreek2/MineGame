using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zombie : MonoBehaviour
{
    public Transform player;

    private float distance;
    public float speed;
    public float distanceBetween = 7;

    public Animator zombie;
    private Rigidbody2D rb;
    public AudioSource zombieGroan;

    public AudioSource[] zombieDamage;

    private int zombieHealth = 10;
    public UnityEngine.UI.Slider zombieSlider;
    public PickaxeManager pickaxeManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);   // getting the distance between the player and the zombie
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan(direction.x);    // getting and calculating the correct direction for the zombie to face

        if (distance < distanceBetween)
        {
            if (!zombieGroan.isPlaying)
            {
                zombieGroan.Play();
            }


            zombie.Play("zombie move");

            Vector2 target = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime); //sets up when the zombie chases the player
            rb.velocity = direction * speed;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            //zombie.SetTrigger("Vision");
        }
        else
        {
            zombieGroan.Stop();
        }
    }

    public void TakeDamage (int damage)
    {
        zombieHealth -= damage;
        zombieSlider.gameObject.SetActive(true);
        zombieSlider.value = zombieHealth;
        zombieDamage[Random.Range(0, 3)].Play();

        transform.GetChild(1).transform.GetComponent<ParticleSystem>().Play();

        if (zombieHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        transform.GetChild(1).transform.parent = transform.parent.transform;
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

