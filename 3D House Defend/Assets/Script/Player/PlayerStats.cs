﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Range(50f, 500f)]
    public int MaxHealth;
    [Range(100f, 1000f)]
    public int MaxEnergy;

    [Range(1f, 100f)]
    public int MelleAttackPower;
    [Range(1f, 100f)]
    public int RangeAttackPower;

    [Range(0f, 50f)]
    public int Armor;

    [SerializeField]
    private int CurrentHealth;
    [SerializeField]
    private int CurrentArmor;
    [SerializeField]
    private int CurrentEnergy;

    void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentEnergy = MaxEnergy;
        CurrentArmor = Armor;
    }

    public void ResiveDamage(int Amount)
    {
        int ActualDamageRecive = Amount - CurrentArmor;
        if (ActualDamageRecive > 0)
        {
            CurrentHealth -= ActualDamageRecive;
            if(CurrentHealth <= 0)
            {
                Death();
            }
        }
        else
            return;
    }

    private void RestoreHealth(int Amount)
    {
        if (CurrentHealth < MaxHealth )
        {
            CurrentHealth += Amount;
            if(CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
        else
            return;
    }

    private void RestoreEnergy(int Amount)
    {
        if (CurrentEnergy < MaxEnergy)
        {
            CurrentEnergy += Amount;
            if (CurrentEnergy > MaxEnergy)
            {
                CurrentEnergy = MaxEnergy;
            }
        }
        else
            return;
    }

    private void Death()
    {

    }


}
