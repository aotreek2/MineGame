using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 30f;
    public Rigidbody2D rb;
    private int damage = 4;

    private float lifetime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }


    private void OnCollisionEnter2D(Collision2D kill)
    {
      Zombie zombie = kill.gameObject.GetComponent<Zombie>();
        if (zombie != null)
        {
            zombie.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= 7)
        {
            Destroy(this.gameObject);
        }
    }
}
