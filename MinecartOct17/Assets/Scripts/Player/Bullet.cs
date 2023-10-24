using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0f;
    public Rigidbody2D rb;
    public int damage = 0;

    private float lifetime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }


    private void OnCollisionEnter2D(Collision2D kill)
    {
      Zombie zombie = kill.gameObject.GetComponent<Zombie>();
      Bat bat = kill.gameObject.GetComponent<Bat>();

        if (zombie != null)
        {
            zombie.TakeDamage(damage);
        }

        if(bat != null)
        {
            bat.TakeDamage(damage);
        }


        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        lifetime -= Time.deltaTime;
        if (lifetime >= 7)
        {
            Destroy(this.gameObject);
        }
    }
}
