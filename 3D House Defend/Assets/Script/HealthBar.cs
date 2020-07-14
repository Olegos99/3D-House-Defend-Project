using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Owner;
    public Image image1;
    public Image image2;
    public Canvas canvas;

    private float CurrentHealth;
    private float MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = PlayerManager.instance.Player.GetComponentInChildren<Camera>();

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

        canvas.transform.LookAt(PlayerManager.instance.Player.GetComponentInChildren<Camera>().transform);//something is wrong (thery wierd mowment of bar)
        canvas.transform.localRotation = Quaternion.Euler(0f , transform.rotation.y , 0f );

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
