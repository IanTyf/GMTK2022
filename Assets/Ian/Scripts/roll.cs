using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roll : MonoBehaviour
{
    #region Reference

    public TurnbaseController turnbaseController;


    #endregion
    public enum Mode
    {
        Idle, MakeSound, Dead
    }

    public int number;
    public LayerMask coverLayer;

    public Transform camTransform;
    private cameraMovement camMovement;
    private Rigidbody rb;
    public float force;
    public float upForce;

    //public float rollCD;
    //private float rollTimer;

    public Mode playerMode;

    public bool moving;

    public bool canMove;

    public bool canNewFrame;
    public float newFrameOffset;

    private float prevSpeed;

    private bool updateRoll;


    public bool movementTestOnly;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camMovement = camTransform.gameObject.GetComponent<cameraMovement>();
        //rollTimer = 0f;
        playerMode = Mode.Idle;

        canMove = true;
        canNewFrame = true;
        camTransform.position = transform.position;
        camTransform.Rotate(new Vector3(0f, transform.rotation.eulerAngles.y, 0f), Space.World);
        //camMovement.InitRot();

        if (movementTestOnly) enableUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        camTransform.position = transform.position;
        if (!updateRoll) return;
        if (playerMode == Mode.Dead) return;

        // sync camera position

        if (canMove)
        {
            
            // can roll
            //if (rollTimer == 0f)
            //{
                //rollTimer = rollCD;
                moving = true;
                canMove = false;

                Vector3 camNormalizedForward = new Vector3(camTransform.forward.x, 0f, camTransform.forward.z);
                camNormalizedForward.Normalize();

                if (Input.GetKey(KeyCode.W))
                {
                    Vector3 forcePos = camTransform.position - camNormalizedForward * 0.5f + Vector3.up * 0.3f;
                    rb.AddForceAtPosition(camNormalizedForward * force, forcePos, ForceMode.Impulse);

                    Vector3 upForcePos = camTransform.position - camNormalizedForward * 0.3f - Vector3.up * 0.5f;
                    rb.AddForceAtPosition(Vector3.up * upForce, upForcePos, ForceMode.Impulse);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Vector3 forcePos = camTransform.position + camTransform.right * 0.5f + Vector3.up * 0.3f;
                    rb.AddForceAtPosition(camTransform.right * -force, forcePos, ForceMode.Impulse);

                    Vector3 upForcePos = camTransform.position + camTransform.right * 0.3f - Vector3.up * 0.5f;
                    rb.AddForceAtPosition(Vector3.up * upForce, upForcePos, ForceMode.Impulse);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Vector3 forcePos = camTransform.position - camNormalizedForward * 0.5f + Vector3.up * 0.3f;
                    rb.AddForceAtPosition(camNormalizedForward * -force, forcePos, ForceMode.Impulse);

                    Vector3 upForcePos = camTransform.position + camNormalizedForward * 0.3f - Vector3.up * 0.5f;
                    rb.AddForceAtPosition(Vector3.up * upForce, upForcePos, ForceMode.Impulse);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    Vector3 forcePos = camTransform.position - camTransform.right * 0.5f + Vector3.up * 0.3f;
                    rb.AddForceAtPosition(camTransform.right * force, forcePos, ForceMode.Impulse);

                    Vector3 upForcePos = camTransform.position - camTransform.right * 0.3f - Vector3.up * 0.5f;
                    rb.AddForceAtPosition(Vector3.up * upForce, upForcePos, ForceMode.Impulse);
                }
                else
                {
                    moving = false;
                    //rollTimer = 0f;
                    canMove = true;
                }
            //}
            //else
            //{
                //rollTimer -= Time.deltaTime;
                //if (rollTimer < 0f) rollTimer = 0f;
            //}

        }

        if (rb.velocity.magnitude < newFrameOffset && prevSpeed > rb.velocity.magnitude && canNewFrame && rb.velocity.magnitude > 0f)
        {
            // new frame
            Debug.Log("new frame");
            updateNumber();
            turnbaseController.newTurn();
            canNewFrame = false;
        }

        if (rb.velocity.magnitude == 0f && prevSpeed > 0)
        {
            canMove = true;
            canNewFrame = true;
        }


        prevSpeed = rb.velocity.magnitude;
        
    }

    public void enableUpdate()
    {
        updateRoll = true;
        camMovement.InitRot();
        camMovement.EnableMovement();
        camTransform.gameObject.GetComponent<Camera>().enabled = true;
        camTransform.gameObject.GetComponent<AudioListener>().enabled = true;
    }


    private void updateNumber()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1f, coverLayer))
        {
            switch (hit.transform.gameObject.name)
            {
                case "Right":
                    number = 4;
                    break;
                case "Left":
                    number = 3;
                    break;
                case "Forward":
                    number = 5;
                    break;
                case "Back":
                    number = 2;
                    break;
                case "Up":
                    number = 6;
                    break;
                case "Down":
                    number = 1;
                    break;
            }
        }
    }
}
