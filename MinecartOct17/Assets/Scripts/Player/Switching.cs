using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switching : MonoBehaviour
{

    public GameObject[] PlayerState;
    public Animator torch;
    public Animator pick;
    public Animator gun;
    public Animator machineGun;

    public bool isDucking = false;


    // Start is called before the first frame update
    void Start()
    {
        // Torch by default

        PlayerState[0].SetActive(true);
        PlayerState[1].SetActive(false);
        PlayerState[2].SetActive(false);
        PlayerState[3].SetActive(false);

        torch = PlayerState[0].GetComponent<Animator>();
        pick = PlayerState[1].GetComponent<Animator>();
        gun = PlayerState[2].GetComponent<Animator>();
        machineGun = PlayerState[3].GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Switching
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!pick.GetCurrentAnimatorStateInfo(0).IsName("PickaxeStill"))
            {
                pick.Play("PickaxeStill");
            }
            else if (!gun.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                gun.Play("Idle");
            }
            else if (!machineGun.GetCurrentAnimatorStateInfo(0).IsName("MachineGunIdle"))
            {
                machineGun.Play("MachineGunIdle");
            }

            PlayerState[0].SetActive(true);
            PlayerState[1].SetActive(false);
            PlayerState[1].GetComponent<PickaxeManager>().active = false;
            PlayerState[2].SetActive(false);
            PlayerState[3].SetActive(false);

            if (isDucking)
            {
                torch.Play("MinerDuckingStationary");
                PlayerState[0].GetComponent<Collider2D>().enabled = false;
                PlayerState[1].GetComponent<Collider2D>().enabled = true;
                PlayerState[2].GetComponent<Collider2D>().enabled = true;
                PlayerState[3].GetComponent<Collider2D>().enabled = true;
                PlayerState[2].transform.GetChild(0).gameObject.SetActive(true);
                PlayerState[3].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!torch.GetCurrentAnimatorStateInfo(0).IsName("Miner Idle"))
            {
                pick.Play("Miner Idle");
            }
            else if (!gun.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                gun.Play("Idle");
            }
            else if (!machineGun.GetCurrentAnimatorStateInfo(0).IsName("MachineGunIdle"))
            {
                machineGun.Play("MachineGunIdle");
            }

            PlayerState[0].SetActive(false);
            PlayerState[1].SetActive(true);
            PlayerState[2].SetActive(false);
            PlayerState[3].SetActive(false);

            if (isDucking)
            {
                pick.Play("PickaxeDuckingStationary");
                PlayerState[0].GetComponent<Collider2D>().enabled = true;
                PlayerState[1].GetComponent<Collider2D>().enabled = false;
                PlayerState[2].GetComponent<Collider2D>().enabled = true;
                PlayerState[3].GetComponent<Collider2D>().enabled = true;
                PlayerState[2].transform.GetChild(0).gameObject.SetActive(true);
                PlayerState[3].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                PlayerState[1].GetComponent<PickaxeManager>().active = true;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!pick.GetCurrentAnimatorStateInfo(0).IsName("PickaxeStill"))
            {
                pick.Play("PickaxeStill");
            }
            else if (!torch.GetCurrentAnimatorStateInfo(0).IsName("Miner Idle"))
            {
                torch.Play("Miner Idle");
            }
            else if (!machineGun.GetCurrentAnimatorStateInfo(0).IsName("MachineGunIdle"))
            {
                machineGun.Play("MachineGunIdle");
            }

            PlayerState[0].SetActive(false);
            PlayerState[1].SetActive(false);
            PlayerState[1].GetComponent<PickaxeManager>().active = false;
            PlayerState[2].SetActive(true);
            PlayerState[3].SetActive(false);
            PlayerState[2].transform.GetChild(0).GetComponent<PlayerAimWeapon>().UpdateAmmo();

            if (isDucking)
            {
                gun.Play("GunDuckingStationary");
                PlayerState[0].GetComponent<Collider2D>().enabled = true;
                PlayerState[1].GetComponent<Collider2D>().enabled = true;
                PlayerState[2].GetComponent<Collider2D>().enabled = false;
                PlayerState[3].GetComponent<Collider2D>().enabled = true;
                PlayerState[2].transform.GetChild(0).gameObject.SetActive(false);
                PlayerState[3].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && MultiSceneScores.machineGunUnlocked == true)
        {
            if (!pick.GetCurrentAnimatorStateInfo(0).IsName("PickaxeStill"))
            {
                pick.Play("PickaxeStill");
            }
            else if (!torch.GetCurrentAnimatorStateInfo(0).IsName("Miner Idle"))
            {
                torch.Play("Miner Idle");
            }
            else if (!gun.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                gun.Play("Idle");
            }

            PlayerState[0].SetActive(false);
            PlayerState[1].SetActive(false);
            PlayerState[1].GetComponent<PickaxeManager>().active = false;
            PlayerState[2].SetActive(false);
            PlayerState[3].SetActive(true);
            PlayerState[3].transform.GetChild(0).GetComponent<PlayerAimMachineGun>().UpdateAmmo();

            if (isDucking)
            {
                machineGun.Play("MachineGunDuckingStationary");
                PlayerState[0].GetComponent<Collider2D>().enabled = true;
                PlayerState[1].GetComponent<Collider2D>().enabled = true;
                PlayerState[2].GetComponent<Collider2D>().enabled = true;
                PlayerState[3].GetComponent<Collider2D>().enabled = false;
                PlayerState[2].transform.GetChild(0).gameObject.SetActive(true);
                PlayerState[3].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
