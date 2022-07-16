using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

public class playerControllerYuri_test : MonoBehaviour
{
    //private variables
    [SerializeField]
    private float speed = 15.0f;
    [SerializeField]
    private float turnSpeed = 25.0f;
    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    public enum Mode
    {
        Walking,Idle
    }
    public Mode mode = Mode.Idle;
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (horizontalInput == 0 && forwardInput == 0)
        {
            mode = Mode.Idle;
        }
        else
        {
            mode = Mode.Walking;
        }
        //Debug.Log(horizontalInput+" "+forwardInput);
        Debug.Log("mode:"+mode);
        
        // Move the vehicle forward
        transform.Translate(Vector3.forward*Time.deltaTime*speed *forwardInput);
        //turn the vehicle
        transform.Rotate(Vector3.up,Time.deltaTime*turnSpeed*horizontalInput);
        
        
    }
}
