using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Housestates : MonoBehaviour
{
    [Range(10f, 500f)]
    public int MaxHealth;

    public int CurrentHealth;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void ResiveDamage(int Amount)
    {
        int ActualDamageRecive = Amount;

        if (ActualDamageRecive > 0)
        {
            CurrentHealth -= ActualDamageRecive;
        }
        else
            return;
    }


    public void Update()
    {
        if(CurrentHealth<=0)
        {
            OnHouseDestroy();
        }
    }

    public void OnHouseDestroy()
    {
        Destroy(this.gameObject);
        /*
         * particle system to add
         *  camera move frome player to house to see it destroyes
         *  UI for replay the day(level)
        */

    }
}
