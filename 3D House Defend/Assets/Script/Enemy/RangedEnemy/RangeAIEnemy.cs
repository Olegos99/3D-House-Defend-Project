using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeAIEnemy : MonoBehaviour
{
    public float LookRadius = 30f;
    public float ShootRadius = 20f;

    private NavMeshAgent agent;

    private Transform PlayerTransform;
    private Transform HouseTransform;
    private Transform CurrentTarget;

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
        PlayerTransform = PlayerManager.instance.Player.transform;
        HouseTransform = House.instance._HouseGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(PlayerTransform.position, transform.position) <= LookRadius)
        {
            CurrentTarget = PlayerTransform;
        }
        else
        {
            CurrentTarget = HouseTransform;
        }

        agent.SetDestination(CurrentTarget.position);

        var newRotation = Quaternion.LookRotation( CurrentTarget.position - transform.position, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.z = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 5);

        if (Vector3.Distance(CurrentTarget.position, transform.position) <= ShootRadius)
        {
            ShootAtTarget(this.transform, CurrentTarget);
        }

    }

    private void OnDrawGizmos() //draw helpful thing in the Sceene
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }

    private void ShootAtTarget(Transform ShootStartTransform, Transform ShootTargetTransform) 
    {
      
        if (Time.time < NextTimeForShooting)
            return;
        else
        {
            NextTimeForShooting = Time.time + TimeBetweenShoots;
            Vector3 UpdatedFrom = new Vector3(ShootStartTransform.position.x, ShootStartTransform.position.y + 2f, ShootStartTransform.position.z); // make enemy shoot frome above the head
            GameObject Bullet = Instantiate(ShootingAmmoType, UpdatedFrom, Quaternion.identity);
            Bullet.GetComponentInChildren<Bullet>().SetBulletDamage(ShootingDamage);
            Bullet.GetComponentInChildren<Bullet>().SetBulletLifeTime(BulletLifeTime);
            Bullet.GetComponentInChildren<Rigidbody>().AddForce((ShootTargetTransform.position - this.transform.position) * ShootingSpeed); //vector to the target * trust
            //StartCoroutine(Bullet.GetComponentInChildren<Bullet>().DestroyBullet(BulletLifeTime));
        }
    }


}
