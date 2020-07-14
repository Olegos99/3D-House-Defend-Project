using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(50f, 150f)]
    public float MouseSensitivity = 100f;

    public Transform PlayerBody;

    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock cursor add keep it invisible(Esc to see it again)
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        PlayerBody.Rotate(Vector3.up * MouseX);
    }
}
/*
6. Tracking: This camera tracks the doll along a pre-defined line in 3D space.
It may rotate, speed up or slow down, fall behind or even move ahead of the doll as required but the player has little or no control over its movements.
Tracking cameras are often used in linear action or platform games like God of War or the Kim Possible DS games.

7. Pushable: Pushable cameras occupy a default position (usually behind the doll) when not controlled, but the player can push them using a second thumb stick or mouse.
The camera then rotates around the doll.This kind of camera is very common in modern games.Pushable and tracking cameras are often casually grouped as ‘third person’ perspective.
*/