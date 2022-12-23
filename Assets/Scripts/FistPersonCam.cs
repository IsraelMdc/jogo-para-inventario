using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPersonCam : MonoBehaviour
{
    public Transform characterBody;
    public Transform characterHead;

    float rotationX = 0;
    float rotationY = 0;

    float SensibilityX = 0.5f;
    float SensibilityY = 0.5f;

    float angleYMin = -90;
    float angleYMax = 90;

    float smootRotx = 0;
    float smootRoty = 0;

    float smoothCoefy = 0.005f;
    float smoothCoefx = 0.005f;
   
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        transform.position = characterHead.position;
    }

    void Update()
    {
        float verticalDelta = Input.GetAxisRaw("Mouse Y") * SensibilityY;
        float horizontalDelta = Input.GetAxisRaw("Mouse X") * SensibilityX;

        smootRotx = Mathf.Lerp(smootRotx,horizontalDelta, smoothCoefx); 
        smootRoty = Mathf.Lerp(smootRoty,verticalDelta, smoothCoefy);

        rotationX += horizontalDelta;
        rotationY += verticalDelta;

        rotationY = Mathf.Clamp(rotationY, angleYMin, angleYMax);

        characterBody.localEulerAngles = new Vector3(0, rotationX, 0);

        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
}
