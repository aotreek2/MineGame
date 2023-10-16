using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Diagnostics;

public class PlayerAimWeapon : MonoBehaviour
{

    [SerializeField] private Material weaponTracerMaterial;
    public GameObject gunAnimation;

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
            Vector3 mousePosition = GetMousePosition();
            onShoot?.Invoke(this, new onShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
                 BulletPosition = aimBulletPositionTransform.position,
            });
            Debug.Log(mousePosition);

            //makes muzzle flare
            gunAnimation.gameObject.SetActive(true);
            gunAnimation.GetComponent<Animator>().Play("MuzzleFlare");
        }
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

    private void WeaponTracer(Vector3 fromPosition, Vector3 targetPosition)
    {
        Vector3 direct = (targetPosition - fromPosition).normalized;
        float eulerZ = GetAngleFromVectorFloat(direct);
        float distance = Vector3.Distance(fromPosition, targetPosition);
        Vector3 tracerSpawnPosition = fromPosition + direct * distance * .5f;
        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPosition, eulerZ, 6f, distance, weaponTracerMaterial, null, 10000);
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
