using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UIHealthsBar : MonoBehaviour
{
    public Image BackGround;
    public Image Health;

    private float num = 1;

    private void Start()
    {
        Health.fillAmount = 1;
    }

    public void SetUIHealth(int CurrentHealth, int MaxHealth)
    {
        float vOut1 = (float)CurrentHealth;
        float vOut2 = (float)MaxHealth;
        num = (vOut1 / vOut2);
        Health.fillAmount = (num);
    }

}
