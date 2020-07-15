using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Range(1, 10)]
    public int MaxHealth;

    public int CurrentHealth;
    [Range(1, 10)]

    [SerializeField]
    private int EnemyDamage = 2;

    public void Start()
    {
        CurrentHealth = MaxHealth;      
    }

    public int GetEnemyDamage()
    {
        return EnemyDamage;
    }

    public void ResiveDamage(int Amount)
    {
        CurrentHealth -= Amount;
    }

    private void Update()
    {
        if(CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }

}
