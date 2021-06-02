using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Guns : ScriptableObject
{
    public string weaponName;
    public float firerate;
    public float aimSpeed;
    public GameObject prefab;
}
