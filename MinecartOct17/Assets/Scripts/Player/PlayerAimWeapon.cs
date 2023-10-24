using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAimWeapon : MonoBehaviour
{
    public GameObject gunAnimation;
    public GameObject bullletPrefab;

    public bool shooting = false;
    public bool reloading = false;
   
    public TMP_Text ammoUI;
    public int maxAmmo = 8;
    public int magazineAmmo = 0;
    public int reservedAmmo = 36;

    public float reloadTime = 0f;

    public AudioSource gunFire;
    public AudioSource reload;

   

    public event EventHandler<onShootEventArgs> onShoot;
    public class onShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 BulletPosition;
    }

    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimBulletPositionTransform;


    

    private void Awake()
    {
        aimTransform = transform.Find("Aim").transform.GetChild(0);
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");

        reservedAmmo = 0;
        magazineAmmo = 0;
        reservedAmmo = MultiSceneScores.ammo;
        Reload();
        reloadTime = 0;
    }

    private void Update()
    {
        HandleAiming();
        handleShooting();

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (magazineAmmo < 8)
            {
                Reload();
            }
        }
    }

    private void FixedUpdate()
    {
        if (reloadTime >= 0)
        {
            reloadTime -= Time.deltaTime;
        }
        else
        {
            reloading = false;
        }
    }


    private void HandleAiming()
    {
        Vector3 mousePosition = GetMousePosition();

        Vector2 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void handleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!shooting && !reloading)
            {
                // Checks if the magazine has bullets in it.
                if(magazineAmmo>0)
                {
                    magazineAmmo--;
                    shooting = true;
                    gunFire.Play();

                    Vector3 mousePosition = GetMousePosition();
                    onShoot?.Invoke(this, new onShootEventArgs
                    {
                        gunEndPointPosition = aimGunEndPointTransform.position,
                        shootPosition = mousePosition,
                        BulletPosition = aimBulletPositionTransform.position,

                    });

                    // Spawns bullet
                    Instantiate(bullletPrefab, aimGunEndPointTransform.position, aimGunEndPointTransform.rotation);


                    //makes muzzle flare, and in the animation it sets shooting back to false
                    gunAnimation.gameObject.SetActive(true);
                    gunAnimation.GetComponent<Animator>().Play("MuzzleFlare");

                    UpdateAmmo();
                }
                else if(magazineAmmo <= 0) // If there are no bullets in the magazine, this happens.
                {
                    Reload();
                }
            }
        }
    }

    public void Reload()
    {
        if (reservedAmmo >= 8) // If there is at least a full magazine of ammo in the reserve, it reloads that.
        {
            reload.Play();
            reservedAmmo -= (maxAmmo-magazineAmmo);
            magazineAmmo += (maxAmmo-magazineAmmo);
            reloading = true;
            reloadTime = 1f;
        }
        else if (reservedAmmo > 0) // If there is less than a full magazine but at least some in the reserve, whatever is left is loaded.
        {
            reload.Play();
            if (reservedAmmo >= (maxAmmo-magazineAmmo))
            {
                reservedAmmo -= (maxAmmo-magazineAmmo);
                magazineAmmo = maxAmmo;
            }
            else
            {
                magazineAmmo += reservedAmmo;
                reservedAmmo = 0;
            }
            reloading = true;
            reloadTime = 1f;
        }
        else
        {
            Debug.Log("EMPTY");
            //Click sound
        }
        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        ammoUI.text = "[" + magazineAmmo + "/" + maxAmmo + "]-[" + reservedAmmo + "]";
        MultiSceneScores.ammo = magazineAmmo + reservedAmmo;
    }

    public void GainAmmo()
    {
        reservedAmmo += UnityEngine.Random.Range(8, 24);
        UpdateAmmo();
    }


    public static Vector3 GetMousePosition()
    {
        Vector3 vec = GetMousePositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMousePositionWithZ()
    {
        return GetMousePositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMousePositionWithZ(Camera worldCamera)
    {
        return GetMousePositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMousePositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
