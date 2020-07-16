using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MAY BE NO NEED IN THIS SCRIPT

public class Weapon : MonoBehaviour
{
    [Range(1, 10)]
    public int WeaponDamage = 1;

    public bool Hited = false;

    public Animator Anim;

    public float attackTime;


    private void Start()
    {
        Anim = GetComponentInParent<Animator>();
        AnimationClip[] clips = Anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "PlayerAttack")
            {
                attackTime = clip.length;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && Hited == false)//was bug with one shoot kills (because on 1 animation was more than one collision)
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attacking") && !Hited)//this way after hitting one enemy weapon till the end of animation will no more deal damage
            {
                Hited = true;
                StartCoroutine(WhaitTillNextDamageDelivery(attackTime));
            }
            Debug.Log("Enemy hitted");
            this.GetComponentInParent<PlayerAttacks>().DeliverDamage(collision.gameObject, true);         
        }
    }


    public void Update()
    {
        ////if(Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attacking") && Hited)
        //if (Anim != null)
        //{
        //    if (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attacking"))
        //    {
        //        Hited = false;
        //    }
        //}
    }

    public IEnumerator WhaitTillNextDamageDelivery(float attackTime)
    {
        yield return new WaitForSeconds(attackTime);
        Hited = false;
    }

}
