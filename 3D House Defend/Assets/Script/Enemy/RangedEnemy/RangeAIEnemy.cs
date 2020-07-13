using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeAIEnemy : MonoBehaviour
{
    public float LookRadius = 30f;
    public float ShootRadius = 20f;


    Transform SecondaryTarget;
    NavMeshAgent agent;

    public Transform MainTarget;

    public GameObject ShootingAmmoType;

    [Range(0.1f,5f)]
    public float TimeBetweenShoots;

    [Range(1f, 1000f)]
    public float ShootingSpeed;

    [Range(1f, 5f)]
    public float BulletLifeTime;

    [Range(1f, 5f)]
    public int ShootingDamage;


    float NextTimeForShooting = 0f;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SecondaryTarget = PlayerManager.instance.Player.transform;
        MainTarget = House.instance._House.transform;
    }

    // Update is called once per frame
    void Update()
    {

        float DistanceToPlayer = Vector3.Distance(SecondaryTarget.position, transform.position);
        float DistanceToMainTarget = Vector3.Distance(MainTarget.position, transform.position);

        if (DistanceToPlayer <= LookRadius)
        {
            agent.SetDestination(SecondaryTarget.position);
            this.transform.LookAt(SecondaryTarget);
            if(DistanceToPlayer <= ShootRadius)
            {
                ShootAtTarget(this.transform, SecondaryTarget);
            }
        }
        else if (DistanceToMainTarget <= ShootRadius)
        {
            ShootAtTarget(this.transform, MainTarget);
        }
        else
        {
            agent.SetDestination(MainTarget.position);
        }

    }

    private void OnDrawGizmos() //draw helpful thing in the Sceene
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }

    public void ShootAtTarget(Transform From, Transform Target) 
    {
      
        if (Time.time < NextTimeForShooting)
            return;
        else
        {
            NextTimeForShooting = Time.time + TimeBetweenShoots;
            Vector3 UpdatedFrom = new Vector3(From.position.x, From.position.y + 2f, From.position.z); // make enemy shoot frome above the head
            GameObject Bullet = Instantiate(ShootingAmmoType, UpdatedFrom, Quaternion.identity);
            Bullet.GetComponentInChildren<Bullet>().SetBulletDamage(ShootingDamage);
            Bullet.GetComponentInChildren<Bullet>().SetBulletLifeTime(BulletLifeTime);
            Bullet.GetComponentInChildren<Rigidbody>().AddForce((Target.position - this.transform.position) * ShootingSpeed); //vector to the target * trust
            //StartCoroutine(Bullet.GetComponentInChildren<Bullet>().DestroyBullet(BulletLifeTime));
        }
    }


}
