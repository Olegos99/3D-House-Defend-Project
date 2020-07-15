using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MelleAIEnemyController : MonoBehaviour
{
    public int AttackDamage;
    public float AttackRate = 0.5f;

    public float lookRadius = 10f;
    public float AttackRadius = 1.5f;
    public float AttackRadiusForHouse = 8.5f;

    public bool AttackingNow = false;


    NavMeshAgent agent;

    private Transform PlayerTransform;
    private Transform HouseTransform;
    private Transform CurrentTarget;

    public Transform AttackPoint;

    public float AttackRange = 0.5f;

    public Animator Anim;

    private float TimeToNextAttack = 0f;
    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PlayerTransform = PlayerManager.instance.Player.transform;
        HouseTransform = House.instance._HouseGameObject.transform;
        Anim = GetComponentInChildren<Animator>();
        AttackDamage = GetComponent<EnemyStats>().GetEnemyDamage();
    }


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerTransform.position, transform.position) <= lookRadius) //may be we can add a searching script

        {
            CurrentTarget = PlayerTransform;
        }
        else
        {
            CurrentTarget = HouseTransform;
        }

        float distance = Vector3.Distance(CurrentTarget.position, transform.position);

        if(CurrentTarget == HouseTransform && distance <= AttackRadiusForHouse) //target point is inside the house (So needed solution to make enemyes attack house)
        {
            if (Time.time >= TimeToNextAttack)
            {
                Attack();
                TimeToNextAttack = Time.time + 1f / AttackRate;
            }
        }

        if (distance <= AttackRadius)
        {
            if (Time.time >= TimeToNextAttack)
            {
                Attack();
                TimeToNextAttack = Time.time + 1f / AttackRate;
            }
        }
        agent.SetDestination(CurrentTarget.position);
    }


    private void Attack()
    {
        Debug.Log("enemy Attacking");
        Anim.SetTrigger("Attack");
        Collider[] HitedEnemies = Physics.OverlapSphere(AttackPoint.position, AttackRange);
        foreach (Collider Enemy in HitedEnemies)
        {
            if(Enemy.gameObject.name == "Player")
            {
                Enemy.gameObject.GetComponent<PlayerStats>().ResiveDamage(AttackDamage);
            }
            if (Enemy.gameObject.tag == "House")
            {
                Enemy.gameObject.GetComponentInChildren<Housestates>().ResiveDamage(AttackDamage);
            }
        }
        //StartCoroutine("Attacking");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRadiusForHouse);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    //public IEnumerator Attack()
    //{
    //    Debug.Log("Enemy Attacks");
    //    AttackingNow = true;
    //    Anim.SetBool("Attacking", true);
    //    //yield return new WaitForSeconds(1f);
    //    yield return new WaitForSeconds(AttackAnimation.length);//change to 
    //    Anim.SetBool("Attacking", false);

    //    AttackingNow = false;
    //}
}
