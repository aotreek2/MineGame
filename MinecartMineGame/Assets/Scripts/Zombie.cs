using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Transform player;
    private float distance;
    public float speed;
    public float distanceBetween;
    public Animator zombie;
   


    // Start is called before the first frame update
    void Start()
    {
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
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime); //sets up when the zombie chases the player 
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            zombie.SetTrigger("Vision");
        }
    }
}

