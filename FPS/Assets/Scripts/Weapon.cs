using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Guns[] loadouts;
    [SerializeField] Transform weaponParent;

    private GameObject currentWeapon;
    private int currentIndex;

    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] LayerMask canBeShot;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Equip(0);
        if (currentWeapon != null)
        {
            Aim(Input.GetMouseButton(1));

            if (Input.GetMouseButton(0))
                Shoot();
        }
    }

    void Equip(int loadoutIndex)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentIndex = loadoutIndex;
        GameObject weaponEquipped = Instantiate(loadouts[loadoutIndex].prefab,
                                                weaponParent.position,
                                                weaponParent.rotation,
                                                weaponParent) as GameObject;
        weaponEquipped.transform.localPosition = Vector3.zero;
        weaponEquipped.transform.localEulerAngles = Vector3.zero;

        currentWeapon = weaponEquipped;
    }

    void Aim(bool isAiming)
    {
        Transform tempAnchor = currentWeapon.transform.Find("Anchor");
        Transform temp_ads = currentWeapon.transform.Find("States/ADS");
        Transform temp_hip = currentWeapon.transform.Find("States/Hipfire");

        if(isAiming)
        {
            tempAnchor.position = Vector3.Lerp(tempAnchor.position,temp_ads.position,Time.deltaTime*loadouts[currentIndex].aimSpeed);
        }
        else
        {
            tempAnchor.position = Vector3.Lerp(tempAnchor.position, temp_hip.position, Time.deltaTime * loadouts[currentIndex].aimSpeed);
        }
    }

    void Shoot()
    {
        Transform rayOrigin = transform.Find("Cameras/Player Camera");

        RaycastHit hitInfo = new RaycastHit();
        if(Physics.Raycast(rayOrigin.position,rayOrigin.forward,out hitInfo,10000f,canBeShot))
        {
            GameObject newHole = Instantiate(bulletHolePrefab,
                                                hitInfo.point+hitInfo.normal*0.001f,
                                                Quaternion.identity) as GameObject;
            newHole.transform.LookAt(hitInfo.point+hitInfo.normal);
            Destroy(newHole,5f);
        }

    }
}
