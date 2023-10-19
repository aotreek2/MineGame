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
    public TMP_Text ammoUI;
    private int maxAmmo = 8;
    public int magazineAmmo = 8;
    public int totalAmmo = 36;

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

        UpdateAmmo();
    }

    private void Update()
    {
        HandleAiming();
        handleShooting();
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
            if (!shooting)
            {
                // Checks if the magazine has bullets in it.
                if(magazineAmmo>0)
                {
                    magazineAmmo--;
                                        shooting = true;
                    Vector3 mousePosition = GetMousePosition();
                    onShoot?.Invoke(this, new onShootEventArgs
                    {
                        gunEndPointPosition = aimGunEndPointTransform.position,
                        shootPosition = mousePosition,
                        BulletPosition = aimBulletPositionTransform.position,

                    });
                    Debug.Log(mousePosition);

                    Instantiate(bullletPrefab, aimGunEndPointTransform.position, aimGunEndPointTransform.rotation);

                    //makes muzzle flare
                    gunAnimation.gameObject.SetActive(true);
                    gunAnimation.GetComponent<Animator>().Play("MuzzleFlare");
                }
                else if(magazineAmmo == 0) // If there are no bullets in the magazine, this happens.
                {
                    if(totalAmmo >= 8) // If there is at least a full magazine of ammo in the reserve, it reloads that.
                    {
                        totalAmmo -= 8;
                        magazineAmmo += 8;
                    }
                    else if(totalAmmo > 0) // If there is less than a full magazine but at least some in the reserve, whatever is left is loaded.
                    {
                        magazineAmmo = totalAmmo;
                        magazineAmmo = 0;
                    }
                    else
                    {
                        Debug.Log("EMPTY");
                        //Click sound
                    }
                }

                UpdateAmmo();
                
            }
        }
    }

    public void UpdateAmmo()
    {
        ammoUI.text = "[" + magazineAmmo + "/" + maxAmmo + "] - [" + totalAmmo + "]";
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
