using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{ 
    Animator Anim;
    AnimatorClipInfo[] m_CurrentClipInfo;
    string m_ClipName;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask EnemyLayers;

    public float AttackRate = 0.5f;
    float TimeToNextAttack = 0f;

    public Collider[] HitedEnemies;

    PlayerWeaponManager PlayerWeaponManager;

    private int PlayerMelleDamage;
    private int PlayerRangeDamage;


    // Start is called before the first frame update
    void Start()
    {
        PlayerWeaponManager = GetComponent<PlayerWeaponManager>();

        Anim = GetComponentInChildren<Animator>();
        PlayerMelleDamage = GetComponentInParent<PlayerStats>().MelleAttackPower;
        PlayerRangeDamage = GetComponentInParent<PlayerStats>().RangeAttackPower;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= TimeToNextAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MelleAttack();
                TimeToNextAttack = Time.time + PlayerWeaponManager.CurrentWeapon.Cooldoun;
            }
        }
        m_CurrentClipInfo = this.Anim.GetCurrentAnimatorClipInfo(0);
        m_ClipName = m_CurrentClipInfo[0].clip.name;
    }

    private void MelleAttack()
    {
        Debug.Log("Attacking");


        Anim.SetTrigger("Attack");

        Collider[] HitedEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange, EnemyLayers);
        foreach (Collider Enemy in HitedEnemies)
        {
            Debug.Log("Hited" + Enemy.name);
            DeliverDamage(Enemy.gameObject, true);//currently only melle damage
        }

        // StartCoroutine("Attacking");

    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 20), "Clip Name : " + m_ClipName);        //Output the current Animation name and length to the screen
    }

    //IEnumerator Attacking()
    //{
    //    while (m_CurrentClipInfo[0].clip.name == "Attack")
    //    {
    //        HitedEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange, EnemyLayers);
    //        yield return new WaitForFixedUpdate();
    //    }
    //    foreach (Collider Enemy in HitedEnemies)
    //    {
    //        Debug.Log("Hited" + Enemy.name);
    //        DeliverDamage(Enemy.gameObject, true);
    //    }
        
    //}

    public void DeliverDamage(GameObject target, bool IsMelleDamage)
    {
        int ActualDamage = PlayerWeaponManager.CurrentWeapon.Damage;
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


    private void OnDrawGizmos() //draw helpful thing in the Sceene
    {
        if (AttackPoint == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
