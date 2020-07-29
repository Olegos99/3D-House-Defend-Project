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

    Transform Player;

    public Vector3 targetPosition;
    Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
       

        canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = PlayerManager.instance.Player.GetComponentInChildren<Camera>();
        Player = PlayerManager.instance.Player.transform;

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
        targetPosition = Player.position;
        var lookPos = targetPosition - canvas.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        canvas.transform.rotation = Quaternion.Slerp(canvas.transform.rotation, rotation, Time.deltaTime * 1);

        //canvas.transform.rotation = Quaternion.LookRotation(targetPosition - currentPosition, Vector3.up);
        //canvas.transform.LookAt(PlayersCamera);//something is wrong (thery wierd mowment of bar)
        //canvas.transform.rotation = Quaternion.Euler(canvas.transform.rotation.x, canvas.transform.rotation.y, canvas.transform.rotation.z);

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
