using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Transform WeaponPlace;

    public WeaponScriptableObj[] WeaponsInInventory;
    public WeaponScriptableObj CurrentWeapon;
    private GameObject CurrentWeaponGameObject;

    private void Start()
    {
        CurrentWeapon = WeaponsInInventory[0];
        SetWeapon(1);
    }

    public WeaponScriptableObj GetCurrentWeapon()
    {
        if(CurrentWeapon == null)
        {
            return null;
        }
        return CurrentWeapon;
    }

    public bool IsCurrentWeaponIsMelle()
    {
        if(CurrentWeapon.IsMelleWeapon)
        {
            return true;
        }
        else
            return false;
    }

    private void SetWeapon(int WeaponIndex)
    {
        CurrentWeapon = WeaponsInInventory[WeaponIndex - 1];
        Destroy(CurrentWeaponGameObject);
        CurrentWeaponGameObject = Instantiate(CurrentWeapon.WeaponGameObject) as GameObject;
        CurrentWeaponGameObject.transform.SetParent(WeaponPlace, false);
    }

    private void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            SetWeapon(1);
        }
        if (Input.GetKeyDown("2"))
        {
            SetWeapon(2);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 20, 200, 20), "CurrentWeapon : " + CurrentWeapon.name);
        GUI.Label(new Rect(0, 40, 200, 20), "CurrentWeapondamage : " + CurrentWeapon.Damage);
    }

}
