using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks2 : MonoBehaviour
{ 
    Animator Anim;
    public AnimatorClipInfo[] m_CurrentClipInfo;
    public string m_ClipName;

    public bool isAttackingNow;

    public float AttackRate = 0.5f;
    float TimeToNextAttack = 0f;

    //public Collider[] HitedEnemies;

    PlayerWeaponManager playerWeaponManager;

    public MouseLook mouseLook;
    MovementControl movementControl;

    ThirdPersonMovement thirdPersonMovement;
    CameraMovement cameraMovement;

    private int PlayerMelleDamage;
    private int PlayerRangeDamage;

    public int CurentMovmentSystem;

    // Start is called before the first frame update
    void Start()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();

        mouseLook = GetComponentInChildren<MouseLook>();
        movementControl = GetComponent<MovementControl>();

        thirdPersonMovement = GetComponentInParent<ThirdPersonMovement>();
        cameraMovement = GetComponentInChildren<CameraMovement>();

        Anim = GetComponentInChildren<Animator>();
        PlayerMelleDamage = GetComponentInParent<PlayerStats>().MelleAttackPower;
        PlayerRangeDamage = GetComponentInParent<PlayerStats>().RangeAttackPower;


        if(mouseLook == null && movementControl == null)
        {
            CurentMovmentSystem = 1;
        }
        else
        {
            CurentMovmentSystem = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= TimeToNextAttack && movementControl.isGrounded)
        //if (Time.time >= TimeToNextAttack && thirdPersonMovement.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MelleAttack();
                TimeToNextAttack = Time.time + playerWeaponManager.CurrentWeapon.Cooldoun;
            }
        }

        m_CurrentClipInfo = this.Anim.GetCurrentAnimatorClipInfo(0);
        m_ClipName = m_CurrentClipInfo[0].clip.name;

        if (isAttackingNow && !mouseLook.CameraFreeezeCoorutineIsStarted)
        //if (isAttackingNow && !cameraMovement.CameraFreeezeCoorutineIsStarted)
        {
            switch (CurentMovmentSystem)
            {
                case 1:
                    StartCoroutine(cameraMovement.FreezeMouseRotation(m_CurrentClipInfo[0].clip.length));
                    StartCoroutine(thirdPersonMovement.FreezeMovement(m_CurrentClipInfo[0].clip.length));
                    break;
                case 2:
                    StartCoroutine(mouseLook.FreezeMouseRotation(m_CurrentClipInfo[0].clip.length));
                    StartCoroutine(movementControl.FreezeMovement(m_CurrentClipInfo[0].clip.length));
                    break;
                default:
                    StartCoroutine(mouseLook.FreezeMouseRotation(m_CurrentClipInfo[0].clip.length));
                    StartCoroutine(movementControl.FreezeMovement(m_CurrentClipInfo[0].clip.length));
                    break;
            }     
        }
    }

    private void MelleAttack()
    {
        Anim.SetTrigger("Attack");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 20), "Clip Name : " + m_ClipName);        //Output the current Animation name and length to the screen
    }

    private void SetAttackBoolTrue()//for animation Event (DO NOT FORGET TO ADD TO ANIMATION CLIPS)
    {
        isAttackingNow = true;
    }

    private void SetAttackBoolFalse()
    {
        isAttackingNow = false;
    }

public void DeliverDamage(GameObject target, bool IsMelleDamage)
    {
        int ActualDamage = playerWeaponManager.CurrentWeapon.Damage;
        if (IsMelleDamage)
        {
            ActualDamage += PlayerMelleDamage;
        }
        else
        {
            ActualDamage += PlayerRangeDamage;
        }

        target.GetComponentInParent<EnemyStats>().ResiveDamage(ActualDamage);
        Debug.Log("Delivered " + ActualDamage + " damage to " + target.name);
    }

}
