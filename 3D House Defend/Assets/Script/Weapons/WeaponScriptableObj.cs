using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Create new weapon")]
public class WeaponScriptableObj : ScriptableObject
{
    public new string name;
    public GameObject WeaponGameObject;
    public bool IsMelleWeapon;
    public int Damage;
    public float Cooldoun;
    public int Cost;



}
