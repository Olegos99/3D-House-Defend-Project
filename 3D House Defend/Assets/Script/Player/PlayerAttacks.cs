using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{ 
    Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Anim.SetBool("Attack", false);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            Anim.SetBool("Attack", true);
        }
       else
            Anim.SetBool("Attack", false);     
    }

    public void DeliverDamage(GameObject target)
    {
        int ActualDamage = GetComponentInParent<PlayerStats>().Realdamage;
        target.GetComponentInChildren<EnemyStats>().ResiveDamage(ActualDamage);
    }
}
