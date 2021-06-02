using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Guns[] loadouts;
    [SerializeField] Transform weaponParent;

    private GameObject currentWeapon;
    private int currentIndex;
    
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
}
