using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Guns[] loadouts;
    [SerializeField] Transform weaponParent;

    private GameObject currentWeapon;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Equip(0);
    }

    void Equip(int loadoutIndex)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);
        
        GameObject weaponEquipped = Instantiate(loadouts[loadoutIndex].prefab,
                                                weaponParent.position,
                                                weaponParent.rotation,
                                                weaponParent) as GameObject;
        weaponEquipped.transform.localPosition = Vector3.zero;
        weaponEquipped.transform.localEulerAngles = Vector3.zero;

        currentWeapon = weaponEquipped;
    }
}
