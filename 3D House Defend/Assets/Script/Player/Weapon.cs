using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public Collider ThisWeaponAttackingCollider;
    public PlayerAttacks2 PA2;

    private void Start()
    {
        PA2 = this.gameObject.GetComponentInParent<PlayerAttacks2>();
    }

    private void Update()
    {
        
        if (PA2 == null)
        {
            PA2 = this.gameObject.GetComponentInParent<PlayerAttacks2>();
        }

        if (PA2 && PA2.isAttackingNow)
        {
            ThisWeaponAttackingCollider.enabled = true;
        }
        else
            ThisWeaponAttackingCollider.enabled = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon collided " + other.gameObject.name);
        //if (PA2.m_ClipName == "Attack")

        if (PA2 && PA2.isAttackingNow)
        {            
            if (other.gameObject.GetComponentInParent<EnemyStats>())//was bug with one shoot kills (because on 1 animation was more than one collision)
            {
                Debug.Log("Weapon hited " + other.gameObject.name);
                //this.GetComponentInParent<PlayerAttacks>().DeliverDamage(collision.gameObject, true);
                this.GetComponentInParent<PlayerAttacks2>().DeliverDamage(other.gameObject, true);
            }
        }
    }
}
