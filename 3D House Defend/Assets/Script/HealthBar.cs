using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Owner;
    public Image image1;
    public Image image2;
    public Canvas can;

    private float CurrentHealth;
    private float MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        can = GetComponentInChildren<Canvas>();
        can.worldCamera = PlayerManager.instance.Player.GetComponentInChildren<Camera>();

        if (Owner.tag == "House")
        {
            MaxHealth = Owner.GetComponentInChildren<Housestates>().MaxHealth;
        }
        if (Owner.tag == "Enemy")
        {
            MaxHealth = Owner.GetComponentInChildren<EnemyStats>().MaxHealth;
        }

    }

    // Update is called once per frame
    void Update()
    {
        can.transform.LookAt(PlayerManager.instance.Player.transform);//something is wrong (thery wierd mowment of bar)

        if (Owner.tag == "House")
        {
            CurrentHealth = Owner.GetComponentInChildren<Housestates>().CurrentHealth;
        }
        if (Owner.tag == "Enemy")
        {
            CurrentHealth = Owner.GetComponentInChildren<EnemyStats>().CurrentHealth;
        }
        float num = (CurrentHealth / MaxHealth);
        image1.fillAmount = (num);
    }
}
