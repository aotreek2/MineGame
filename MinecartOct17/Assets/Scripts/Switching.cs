using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switching : MonoBehaviour
{

    public GameObject[] PlayerState;
 

    // Start is called before the first frame update
    void Start()
    {
        // Torch by default
        PlayerState[0].SetActive(true);
        PlayerState[1].SetActive(false);
        PlayerState[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Animator torch = PlayerState[0].GetComponent<Animator>();
        Animator pick = PlayerState[1].GetComponent<Animator>();
        Animator gun = PlayerState[2].GetComponent<Animator>();

        if (torch.GetCurrentAnimatorStateInfo(0).IsName("Miner Idle") || pick.GetCurrentAnimatorStateInfo(0).IsName("PickaxeStill") || gun.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            // Switching
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerState[0].SetActive(true);
                PlayerState[1].SetActive(false);
                PlayerState[1].GetComponent<PickaxeManager>().active = false;
                PlayerState[2].SetActive(false);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlayerState[0].SetActive(false);
                PlayerState[1].SetActive(true);
                PlayerState[1].GetComponent<PickaxeManager>().active = true;
                PlayerState[2].SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlayerState[0].SetActive(false);
                PlayerState[1].SetActive(false);
                PlayerState[1].GetComponent<PickaxeManager>().active = false;
                PlayerState[2].SetActive(true);
            }
        }
    }
}
