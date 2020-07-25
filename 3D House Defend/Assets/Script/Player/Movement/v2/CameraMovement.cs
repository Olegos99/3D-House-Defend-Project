using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float MouseSensitivity = 1;

    [Range(0f,1000f)]
    public float smoth = 500f;

    public Transform PlayerBody, Target;
    ThirdPersonMovement thirdPersonMovement;

    float MouseX, MouseY;

    public bool EnableMouseRotation = true;
    public bool CameraFreeezeCoorutineIsStarted = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock cursor add keep it invisible(Esc to see it again)
        thirdPersonMovement = GetComponentInParent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CamControll();
    }

    void CamControll()
    {
        MouseX += Input.GetAxis("Mouse X") * MouseSensitivity;
        MouseY -= Input.GetAxis("Mouse Y") * MouseSensitivity;

        MouseY = Mathf.Clamp(MouseY, -45f, 60f);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(MouseY, MouseX, 0);

        //Vector3 DesiredRotation = new Vector3(MouseY, MouseX, 0f);
        //Target.rotation = Quaternion.RotateTowards(Target.rotation, Quaternion.Euler(DesiredRotation), smoth * Time.deltaTime);

        if (EnableMouseRotation)
        {
            if (thirdPersonMovement.isMoving) // if make camera move around player while not moving this lines will apply player body rotation to veiw
            {
                //smoth = 150;
                PlayerBody.rotation = Quaternion.RotateTowards(PlayerBody.rotation, Quaternion.Euler(0, MouseX, 0), smoth * Time.deltaTime);
            }
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
}
