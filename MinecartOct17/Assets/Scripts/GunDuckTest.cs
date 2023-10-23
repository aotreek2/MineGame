using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDuckTest : MonoBehaviour
{
    private Animator animator;
    private Collider2D standingCollider;

    private Switching switchScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        standingCollider = GetComponent<Collider2D>();

        switchScript = transform.parent.GetComponent<Switching>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // when player presses S, miner ducks
        {
            if (!switchScript.isDucking)
            {
                animator.Play("gunduck");
                transform.GetChild(0).gameObject.SetActive(false);
                switchScript.isDucking = true;
                standingCollider.enabled = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (switchScript.isDucking)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                animator.Play("gunrising");
                switchScript.isDucking = false;
                standingCollider.enabled = true;           
                //StartCoroutine("WaitforRising");      
            }
        }
    }

 
    private IEnumerator WaitforRising()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == true);
        {
            transform.GetChild(0).gameObject.SetActive(true); // when S is released, the arm waits until the miners body is back before being active again.

        }
    }

    public void ArmAppear()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
