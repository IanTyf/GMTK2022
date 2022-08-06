using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float xSens;
    public float ySens;

    private float xRot;
    private float yRot;

    private bool enableMovement;

    // Start is called before the first frame update
    void Start()
    {
        // initRot is called in roll's start after rotation adjustment is complete
    }

    // Update is called once per frame
    void Update()
    {
        if (!enableMovement) return;

        float x = Input.GetAxisRaw("Mouse X") * xSens;
        float y = Input.GetAxisRaw("Mouse Y") * ySens;

        xRot -= y;
        yRot += x;

        if (xRot < -75f) xRot = -75f;
        if (xRot > 75f) xRot = 75f;

        transform.rotation = Quaternion.Euler(xRot, yRot, 0f);
    }

    public void InitRot()
    {
        xRot = transform.eulerAngles.x;
        yRot = transform.eulerAngles.y;
    }

    public void EnableMovement()
    {
        enableMovement = true;
    }
    /*
    public void nod(string dir)
    {
        switch (dir)
        {
            case "W":
                nodW = 1;
                break;
            case "A":
                nodD = -1;
                break;
            case "S":
                nodW = -1;
                break;
            case "D":
                nodD = 1;
                break;
        }
    }
    */
}
