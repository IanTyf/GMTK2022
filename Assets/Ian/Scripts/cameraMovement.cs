using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float xSens;
    public float ySens;

    private float xRot;
    private float yRot;



    // Start is called before the first frame update
    void Start()
    {
        xRot = 0f;
        yRot = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxisRaw("Mouse X") * xSens;
        float y = Input.GetAxisRaw("Mouse Y") * ySens;

        xRot -= y;
        yRot += x;

        if (xRot < -75f) xRot = -75f;
        if (xRot > 75f) xRot = 75f;

        transform.rotation = Quaternion.Euler(xRot, yRot, 0f);
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
