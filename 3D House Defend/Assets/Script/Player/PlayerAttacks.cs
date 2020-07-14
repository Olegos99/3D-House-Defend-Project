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



    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= TimeToNextAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                TimeToNextAttack = Time.time + 1f / AttackRate;
            }
        }
        m_CurrentClipInfo = this.Anim.GetCurrentAnimatorClipInfo(0);
        m_ClipName = m_CurrentClipInfo[0].clip.name;
    }

    private void Attack()
    {
        Debug.Log("Attacking");
        Anim.SetTrigger("Attack");
        Collider[] HitedEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange, EnemyLayers);
        foreach (Collider Enemy in HitedEnemies)
        {
            Debug.Log("Hited" + Enemy.name);
            DeliverDamage(Enemy.gameObject);
        }
        //StartCoroutine("Attacking");

    }

    void OnGUI()
    {
        //Output the current Animation name and length to the screen
        GUI.Label(new Rect(0, 0, 200, 20), "Clip Name : " + m_ClipName);
    }

    //IEnumerator Attacking()
    //{
    //    while(m_CurrentClipInfo[0].clip.name == "Attack")
    //    {
    //        HitedEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange, EnemyLayers);
    //        foreach (Collider Enemy in HitedEnemies)
    //        {
    //            Debug.Log("Hited" + Enemy.name);
    //            DeliverDamage(Enemy.gameObject);
    //        }
    //        yield return null;
    //    }
    //    yield return new WaitForSeconds(0.1f);
    //}

    public void DeliverDamage(GameObject target)
    {
        int ActualDamage = GetComponentInParent<PlayerStats>().Realdamage;
        target.GetComponentInParent<EnemyStats>().ResiveDamage(ActualDamage);
    }

    private void OnDrawGizmos() //draw helpful thing in the Sceene
    {
        if (AttackPoint == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
