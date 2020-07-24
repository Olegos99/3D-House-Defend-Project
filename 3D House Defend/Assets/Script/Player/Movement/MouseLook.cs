using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(50f, 150f)]
    public float MouseSensitivity = 100f;

    public Transform PlayerBody;
    MovementControl movementControl;

    float xRotation = 0f;
    float yRotation = 0f;

    public bool EnableMouseRotation = true;
    public bool CameraFreeezeCoorutineIsStarted = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock cursor add keep it invisible(Esc to see it again)
        movementControl = GetComponentInParent<MovementControl>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (EnableMouseRotation)
        //{
            float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -45f, 20f);

        //yRotation += MouseX;


        if (EnableMouseRotation)
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


            //if (movementControl.isMoving) // if make camera move around player while not moving this lines will apply player body rotation to veiw
            //{
                PlayerBody.Rotate(Vector3.up * MouseX);
            //}
        }
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