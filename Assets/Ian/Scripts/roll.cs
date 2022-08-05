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

    // Start is called before the first frame update
    void Start()
    {
        camMovement = camTransform.gameObject.GetComponent<cameraMovement>();
        rb = GetComponent<Rigidbody>();
        //rollTimer = 0f;
        playerMode = Mode.Idle;

        canMove = true;
        camTransform.Rotate(new Vector3(0f, transform.rotation.eulerAngles.y, 0f), Space.World);
        camMovement.InitRot();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMode == Mode.Dead) return;

        // sync camera position
        camTransform.position = transform.position;

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

        if (rb.velocity.magnitude < newFrameOffset && prevSpeed > rb.velocity.magnitude && canNewFrame)
        {
            // new frame
            Debug.Log("new frame");
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
}
