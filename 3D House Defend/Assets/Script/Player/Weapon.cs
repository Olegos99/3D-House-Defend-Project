using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MAY BE NO NEED IN THIS SCRIPT

public class Weapon : MonoBehaviour
{
    public Collider ThisWeaponCollider;

    private void Start()
    {
        ThisWeaponCollider.enabled = false;
    }


    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Weapon collided " + collision.gameObject.name);
        if (collision.gameObject.GetComponentInParent<EnemyStats>())//was bug with one shoot kills (because on 1 animation was more than one collision)
        {
            Debug.Log("Weapon hited " + collision.gameObject.name);
            this.GetComponentInParent<PlayerAttacks>().DeliverDamage(collision.gameObject, true);         
        }
    }
}
