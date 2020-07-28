using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(1f, 150f)]
    public float MouseSensitivity = 1f;

    public Transform PlayerBody, Target;
    MovementControl movementControl;

    public float CameraDistancecontroller;

    float xRotation = 0f;
    float yRotation = 0f;

    [Range(0f, 1000f)]
    public float smoth = 500f;

    float m_MouseX, m_MouseY;

    private float CameraMaxPitch = 45f;
    private float CameraMinPitch = -45f;

    public bool EnableMouseRotation = true;
    public bool CameraFreeezeCoorutineIsStarted = false;

    float CurrentDictanceBetwenCameraAndTarget;

    Vector3 StartingPosition;
    Vector3 CurrentPosition;

    private Vector3 DirectionFromCameraToTarget;

    private float MaxDictanceBetwenCameraAndTarget;
    private float MinDictanceBetwenCameraAndTarget = 2f;

    float LastFrameMouseY;
    public float DesiredDistanceFromTarget;

    public Transform MovingCameraPosition;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        StartingPosition = this.gameObject.transform.position;
        MaxDictanceBetwenCameraAndTarget = Vector3.Distance(Target.position, transform.position);
        Debug.Log("MaxDictanceBetwenCameraAndTarget is:  " + MaxDictanceBetwenCameraAndTarget);
        Cursor.lockState = CursorLockMode.Locked; //lock cursor add keep it invisible(Esc to see it again)
        movementControl = GetComponentInParent<MovementControl>();

        PlayerBody.rotation = Quaternion.RotateTowards(PlayerBody.rotation, Quaternion.Euler(0, m_MouseX - 180, 0), smoth * Time.deltaTime);
    }


    void Update()
    {
        CamControll();
    }


    void CamControll()
    {
        m_MouseX += Input.GetAxis("Mouse X") * MouseSensitivity;
        m_MouseY -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        m_MouseY = Mathf.Clamp(m_MouseY, CameraMinPitch, CameraMaxPitch);//camera rotation limitation

        Target.rotation = Quaternion.Euler(m_MouseY, m_MouseX, 0);
        transform.LookAt(Target);

        var Controller = Mathf.Clamp(m_MouseY, -100f, 100f);

        if (Controller > 0)
        {
            DesiredDistanceFromTarget = MaxDictanceBetwenCameraAndTarget - Controller / 10;
        }
        else
        {
            DesiredDistanceFromTarget = MaxDictanceBetwenCameraAndTarget + Controller / 10;
        }

        DesiredDistanceFromTarget = Mathf.Clamp(DesiredDistanceFromTarget, 2f, 6f);

        if (EnableMouseRotation)
        {

            if (movementControl.isMoving) // if make camera move around player while not moving this lines will apply player body rotation to veiw
            {

                //PlayerBody.Rotate(Vector3.up * MouseX);     
                PlayerBody.rotation = Quaternion.RotateTowards(PlayerBody.rotation, Quaternion.Euler(0, m_MouseX - 180, 0), smoth * Time.deltaTime); //need correction (i use local rotations and it turned all player body 180)    

                if (PlayerBody.rotation == Quaternion.Euler(0, m_MouseX - 180, 0))
                {
                    transform.position = Vector3.SmoothDamp(transform.position, MovingCameraPosition.position, ref velocity, smoothTime);
                }

                CameraMaxPitch = 10f;
                CameraMinPitch = -10f;

                //    PlayerBody.Rotate(Vector3.up * MouseX);
                //    PlayerBody.rotation = Quaternion.Euler(0, MouseX, 0);
            }
            else
            {
                CameraMaxPitch = 45f;
                CameraMinPitch = -45f;

                Vector3 NewCameraPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, DesiredDistanceFromTarget);
                
                //this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, NewCameraPosition, 1);
                this.transform.localPosition = Vector3.SmoothDamp(transform.localPosition, NewCameraPosition, ref velocity, smoothTime);
            }
        }




        //DirectionFromCameraToTarget = Target.position - transform.position;
        //CurrentDictanceBetwenCameraAndTarget = Vector3.Distance(Target.position, transform.position);

        
       


        //this.transform.position = Vector3.Lerp(this.transform.position, Target.position, (1 * Time.deltaTime));//make it first person))
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Target.rotation, (1 * Time.deltaTime));


        //transform.position = Vector3.Lerp(zoomstart.position, zoomend.position, zoom);


        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, DesiredDictanseFromTarget);
        //this.transform.position = DirectionFromCameraToTarget * DesiredDictanseFromTarget;

        //if (CurrentDictanceBetwenCameraAndTarget < MaxDictanceBetwenCameraAndTarget && CurrentDictanceBetwenCameraAndTarget > MinDictanceBetwenCameraAndTarget)
        //{
        //    if (Mathf.Clamp(m_MouseY, -1f, 1f) > 0)
        //    {
        //        if (m_MouseY > LastFrameMouseY)
        //        {
        //            this.transform.position += DirectionFromCameraToTarget / 100 * 0.5f;
        //        }
        //        if (m_MouseY < LastFrameMouseY)
        //        {
        //            this.transform.position -= DirectionFromCameraToTarget / 100 * 0.5f;
        //        }
        //    }
        //    else
        //    {
        //        if (m_MouseY > LastFrameMouseY)
        //        {
        //            this.transform.position += DirectionFromCameraToTarget / 100 * -0.5f;
        //        }
        //        if (m_MouseY < LastFrameMouseY)
        //        {
        //            this.transform.position -= DirectionFromCameraToTarget / 100 * -0.5f;
        //        }
        //    }

        //}
        //else if (CurrentDictanceBetwenCameraAndTarget > MaxDictanceBetwenCameraAndTarget)
        //{
        //    this.transform.position += DirectionFromCameraToTarget / 100 * 0.1f;
        //}
        //else if (CurrentDictanceBetwenCameraAndTarget < MinDictanceBetwenCameraAndTarget)
        //{
        //    this.transform.position -= DirectionFromCameraToTarget / 100 * 0.1f;
        //}

        //LastFrameMouseY = m_MouseY;

    }

    public IEnumerator FreezeMouseRotation(float SomeTime)
    {
        CameraFreeezeCoorutineIsStarted = true;
        EnableMouseRotation = false;
        yield return new WaitForSeconds(SomeTime);
        EnableMouseRotation = true;
        CameraFreeezeCoorutineIsStarted = false;
    }
//    public void FreezeMouseRotation()
//    {
//        EnableMouseRotation = false;
//    }
}
/*
6. Tracking: This camera tracks the doll along a pre-defined line in 3D space.
It may rotate, speed up or slow down, fall behind or even move ahead of the doll as required but the player has little or no control over its movements.
Tracking cameras are often used in linear action or platform games like God of War or the Kim Possible DS games.

7. Pushable: Pushable cameras occupy a default position (usually behind the doll) when not controlled, but the player can push them using a second thumb stick or mouse.
The camera then rotates around the doll.This kind of camera is very common in modern games.Pushable and tracking cameras are often casually grouped as ‘third person’ perspective.
*/