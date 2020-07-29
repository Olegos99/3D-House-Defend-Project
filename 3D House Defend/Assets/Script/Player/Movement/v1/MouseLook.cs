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

    [Range(0f, 1000f)]
    public float smoth = 500f;

    float m_MouseX, m_MouseY;

    private float CameraMaxPitch = 45f;
    private float CameraMinPitch = -45f;

    public bool EnableMouseRotation = true;
    public bool CameraFreeezeCoorutineIsStarted = false;

    private float MaxDictanceBetwenCameraAndTarget;
    private float MinDictanceBetwenCameraAndTarget = 2f;

    public float DesiredDistanceFromTarget;

    public Transform MovingCameraPosition;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private bool SmoothViewAngleControlIncreaseCoorutineIsRunning;
    private bool SmoothViewAngleControlDecreaseCoorutineIsRunning;

    void Start()
    {
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

        DesiredDistanceFromTarget = Mathf.Clamp(DesiredDistanceFromTarget, MinDictanceBetwenCameraAndTarget, MaxDictanceBetwenCameraAndTarget);

        if (EnableMouseRotation)
        {

            if (movementControl.isMoving) // if make camera move around player while not moving this lines will apply player body rotation to veiw
            {   
                PlayerBody.rotation = Quaternion.RotateTowards(PlayerBody.rotation, Quaternion.Euler(0, m_MouseX, 0), smoth * Time.deltaTime); //need correction (i use local rotations and it turned all player body 180)    

                if (PlayerBody.rotation == Quaternion.Euler(0, m_MouseX, 0))
                {
                    Vector3 NewCameraPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -DesiredDistanceFromTarget);   
                    this.transform.localPosition = Vector3.SmoothDamp(transform.localPosition, NewCameraPosition, ref velocity, smoothTime);
                }

                if (SmoothViewAngleControlIncreaseCoorutineIsRunning)
                {
                    StopCoroutine("SmoothViewAngleControlIncrease");
                    SmoothViewAngleControlIncreaseCoorutineIsRunning = false;
                }
                if (!SmoothViewAngleControlDecreaseCoorutineIsRunning && CameraMaxPitch > 10f || CameraMinPitch > -10f)
                {
                    StartCoroutine(SmoothViewAngleControlDecrease(-10, 10, 0.05f));
                }
            }
            else
            {
                if (SmoothViewAngleControlDecreaseCoorutineIsRunning)
                {
                    StopCoroutine("SmoothViewAngleControlDecrease");
                    SmoothViewAngleControlDecreaseCoorutineIsRunning = false;
                }
                if (!SmoothViewAngleControlIncreaseCoorutineIsRunning && CameraMaxPitch < 45f || CameraMinPitch > -45f)
                {
                    StartCoroutine(SmoothViewAngleControlIncrease(-45, 45, 0.05f));
                }
                Vector3 NewCameraPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -DesiredDistanceFromTarget);
                this.transform.localPosition = Vector3.SmoothDamp(transform.localPosition, NewCameraPosition, ref velocity, smoothTime);
            }
        }  
    }
    public IEnumerator SmoothViewAngleControlIncrease(float min, float max, float SomeTime)
    {
        SmoothViewAngleControlIncreaseCoorutineIsRunning = true;
            while (CameraMaxPitch < max || CameraMinPitch > min)
            {
                if (CameraMaxPitch < max)
                {
                    CameraMaxPitch += 1f;
                }
                if (CameraMinPitch > min)
                {
                    CameraMinPitch -= 1f;
                }
                yield return new WaitForSeconds(SomeTime);
            }
        SmoothViewAngleControlIncreaseCoorutineIsRunning = false;
        yield return null;

    }
    public IEnumerator SmoothViewAngleControlDecrease(float min, float max, float SomeTime)
    {
        SmoothViewAngleControlDecreaseCoorutineIsRunning = true;
            while (CameraMaxPitch > max || CameraMinPitch < min)
            {
                if (CameraMaxPitch > max)
                {
                    CameraMaxPitch -= 1f;
                }
                if (CameraMinPitch < min)
                {
                    CameraMinPitch += 1f;
                }
                yield return new WaitForSeconds(SomeTime);
            }
        SmoothViewAngleControlDecreaseCoorutineIsRunning = false;
        yield return null;

    }
    public IEnumerator FreezeMouseRotation(float SomeTime)
    {
        CameraFreeezeCoorutineIsStarted = true;
        EnableMouseRotation = false;
        yield return new WaitForSeconds(SomeTime);
        EnableMouseRotation = true;
        CameraFreeezeCoorutineIsStarted = false;
    }
}
/*
6. Tracking: This camera tracks the doll along a pre-defined line in 3D space.
It may rotate, speed up or slow down, fall behind or even move ahead of the doll as required but the player has little or no control over its movements.
Tracking cameras are often used in linear action or platform games like God of War or the Kim Possible DS games.

7. Pushable: Pushable cameras occupy a default position (usually behind the doll) when not controlled, but the player can push them using a second thumb stick or mouse.
The camera then rotates around the doll.This kind of camera is very common in modern games.Pushable and tracking cameras are often casually grouped as ‘third person’ perspective.
*/