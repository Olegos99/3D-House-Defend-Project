using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to game object wich you want to be able to control

public class MovementControl : MonoBehaviour
{
    public CharacterController Controller;

    public bool isGrounded;
    public bool isMoving;

    public bool AllowMovment = true;
    public bool FreezeMovmentCoorutineIsStarted = false;


    [Range(5f, 30f)]
    public float speed = 5f;

    [Range(0f, 15f)]
    public float jumpHeight = 5f;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;


    public bool Movment2D;
    public bool Movment3D;
    public bool Fly;
    public bool DoubleJump;

    [Range(0f, 2f)]
    public float timeBetweenTheJumps = 0.1f;

    bool jumpedOnce = false;
    bool jumpedTwice = false;




    //public bool WASDmovement;
    void Start()
    {
        if(Movment2D && Movment3D || !Movment2D && !Movment3D)
        {
            Debug.Log("Choose type of movement");
        }
    }



    void Update()
    {
        if (AllowMovment)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //creatind invisible sphere to check if it touching the "groundMask" layer

            if (isGrounded && velocity.y < 0) // to avoid adding forse vector every frame while grounded
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");//multiplatform input recive
            float z = Input.GetAxis("Vertical");

            Vector3 move;

            if (Movment3D)
            {
                move = transform.right * x + transform.forward * z;
            }
            else
            {
                move = transform.right * x;
            }



            Controller.Move(move * speed * Time.deltaTime);//main movement vector

            if (Controller.velocity != new Vector3(0,0,0))
            {
                isMoving = true;
            }
            else 
            {
                isMoving = false;
            }

            

            if (DoubleJump)
            {

                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
                    StartCoroutine("WhaitBeforeNextJump");
                }
                if (Input.GetButtonDown("Jump") && jumpedOnce && !jumpedTwice)
                {
                    velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
                    jumpedTwice = true;
                }
                if (isGrounded)
                {
                    jumpedOnce = false;
                    jumpedTwice = false;
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }


                velocity.y += gravity * Time.deltaTime; //adding velocity to axis y (gravity)
                Controller.Move(velocity * Time.deltaTime); //acceleration a = f * (time^2)           
        }
        
    }

    public IEnumerator FreezeMovement(float SomeTime)
    {
        FreezeMovmentCoorutineIsStarted = true;
        AllowMovment = false;
        yield return new WaitForSeconds(SomeTime);
        AllowMovment = true;
        FreezeMovmentCoorutineIsStarted = false;
    }

    IEnumerator WhaitBeforeNextJump()
    {
        yield return new WaitForSeconds(timeBetweenTheJumps);
        jumpedOnce = true;
    }
}
