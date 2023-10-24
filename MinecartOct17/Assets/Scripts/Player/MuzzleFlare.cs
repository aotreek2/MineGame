using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MuzzleFlare : MonoBehaviour
{
    public bool machineGun = false;
    public PlayerAimWeapon gunScript;
    public PlayerAimMachineGun gunScript2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuzzleFlareFade()
    {
        if (machineGun)
        {
            gunScript2.shooting = false;
            gameObject.SetActive(false);
        }
        else
        {
            gunScript.shooting = false;
            gameObject.SetActive(false);
        }
    }
}
