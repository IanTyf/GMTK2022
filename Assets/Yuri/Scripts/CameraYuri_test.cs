using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYuri_test : MonoBehaviour
{
    // Start is called before the first frame update
    //FPS
    public float sensX, sensY;

    public Transform orientation;

    private float xRotation, yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region Rotate camera

        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90f,90f);
        
        transform.rotation = Quaternion.Euler(xRotation,0,0);
        orientation.rotation = Quaternion.Euler(0,yRotation,0);
        #endregion
    }
}
