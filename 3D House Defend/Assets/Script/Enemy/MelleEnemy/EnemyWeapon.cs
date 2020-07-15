using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MAY BE NO NEED IN THIS SCRIPT
public class EnemyWeapon : MonoBehaviour
{
    public int WeaponDamage; 

    private void OnCollisionEnter(Collision collision)
    {
        WeaponDamage = GetComponentInParent<EnemyStats>().GetEnemyDamage();
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<PlayerStats>().ResiveDamage(WeaponDamage);
        }
        if (collision.gameObject.tag == "House")
        {
            collision.gameObject.GetComponentInChildren<Housestates>().ResiveDamage(WeaponDamage);
        }
    }
}
