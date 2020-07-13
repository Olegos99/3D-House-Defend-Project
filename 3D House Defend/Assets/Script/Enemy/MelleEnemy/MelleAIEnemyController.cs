using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MelleAIEnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float AttackRadius = 1.5f;

    public bool AttackingNow = false;

    Transform SecondaryTarget;
    NavMeshAgent agent;

    public Transform MainTarget;
    public GameObject m_MainTarget;

    public Animator Anim;
    public Animation Anima;
    public AnimationClip AttackAnimation;

    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SecondaryTarget = PlayerManager.instance.Player.transform;
        MainTarget = House.instance._House.transform;
        Anim = GetComponentInChildren<Animator>();
        Anima = GetComponentInChildren<Animation>();
    }

    public void Awake()
    {
        foreach (AnimationState AC in Anima)
        {

            Debug.Log("searching");
            if (AC.clip.name == "EnemyAttack")
            {
                AttackAnimation = AC.clip;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(SecondaryTarget.position, transform.position);      

        if (distance <= AttackRadius)
        {
            if(!AttackingNow)
            {
                StartCoroutine("Attack");
            }
        }

        if (distance <= lookRadius)
        {
            agent.SetDestination(SecondaryTarget.position);
        }
        else
        {
            agent.SetDestination(MainTarget.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    public IEnumerator Attack()
    {
        Debug.Log("Enemy Attacks");
        AttackingNow = true;
        Anim.SetBool("Attacking", true);
        //yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(AttackAnimation.length);//change to 
        Anim.SetBool("Attacking", false);

        AttackingNow = false;
    }
}
