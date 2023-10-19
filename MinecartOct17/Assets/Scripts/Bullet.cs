using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;

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
}
