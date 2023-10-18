using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTracer : MonoBehaviour
{

    [SerializeField] private PlayerAimWeapon playerAimWeapon;
    [SerializeField] private Material weaponTracerMaterial;

    public GameObject gunAnimation;
    // Start is called before the first frame update
    void Start()
    {
        playerAimWeapon.OnShoot += PlayerAimWeapon_OnShoot;
    }

    private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        WeaponTracer(e.gunEndPointPosition, e.shootPosition);
        gunAnimation.gameObject.SetActive(true);
        gunAnimation.GetComponent<Animator>().Play("MuzzleFlare");
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
