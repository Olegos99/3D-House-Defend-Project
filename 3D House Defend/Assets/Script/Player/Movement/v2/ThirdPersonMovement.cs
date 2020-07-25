using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float speed;
    public bool isGrounded;


    public Rigidbody rigidbody;
    [Range(0f, 15f)]
    public float JumpForce = 10;

    CameraMovement cameraMovment;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isMoving;

    public bool AllowMovment = true;
    public bool FreezeMovmentCoorutineIsStarted = false;

    Vector3 m_EulerAngleVelocity;

    private void Start()
    {
        cameraMovment = GetComponentInChildren<CameraMovement>();
    }

    // Start is called before the first frame update
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //creatind invisible sphere to check if it touching the "groundMask" layer

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
        }

        if (isGrounded && AllowMovment)
        {
            PlayerMovement();
        }
    }


    void PlayerMovement()
    {
        float horisontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(horisontal, 0f, vertical).normalized * speed * Time.deltaTime;
        if(playerMovement != new Vector3(0,0,0))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //rigidbody.MovePosition(transform.position + playerMovement);


        transform.Translate(playerMovement, Space.Self);
        //rigidbody.AddForce(playerMovement * speed, ForceMode.Impulse);
    }


    public IEnumerator FreezeMovement(float SomeTime)
    {
        FreezeMovmentCoorutineIsStarted = true;
        AllowMovment = false;
        yield return new WaitForSeconds(SomeTime);
        AllowMovment = true;
        FreezeMovmentCoorutineIsStarted = false;
    }
}
