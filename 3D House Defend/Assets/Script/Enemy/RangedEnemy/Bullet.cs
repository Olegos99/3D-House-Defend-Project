using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int ThisBulletDamage;
    public float BulletLifeTime;

    private void Awake()
    {
        StartCoroutine(DestroyBullet(BulletLifeTime));
    }
    public void SetBulletDamage(int Amount)
    {
        ThisBulletDamage = Amount;
    }
    public void SetBulletLifeTime(float Amount)
    {
        BulletLifeTime = Amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<PlayerStats>().ResiveDamage(ThisBulletDamage);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "House")
        {
            collision.gameObject.GetComponentInChildren<Housestates>().ResiveDamage(ThisBulletDamage);
            Destroy(this.gameObject);
        }
        StopCoroutine("DestroyBullet");
    }

    public IEnumerator DestroyBullet(float LifeTime) //destroy after life time ends
    {
        yield return new WaitForSeconds(LifeTime);
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }
}
