using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roll : MonoBehaviour
{
    public enum Mode
    {
        Idle, MakeSound, Dead
    }


    public Transform camTransform;
    private cameraMovement camMovement;
    private Rigidbody rb;
    public float force;

    public float rollCD;
    private float rollTimer;

    public Mode playerMode;

    public bool moving;

    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        camMovement = camTransform.gameObject.GetComponent<cameraMovement>();
        rb = GetComponent<Rigidbody>();
        rollTimer = 0f;
        playerMode = Mode.Idle;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMode == Mode.Dead) return;

        // sync camera position
        camTransform.position = transform.position;

        if (!canMove) return;
 
        // can roll
        if (rollTimer == 0f)
        {
            rollTimer = rollCD;
            moving = true;

            Vector3 camNormalizedForward = new Vector3(camTransform.forward.x, 0f, camTransform.forward.z);
            camNormalizedForward.Normalize();

            if (Input.GetKey(KeyCode.W))
            {
                Vector3 forcePos = camTransform.position - camNormalizedForward * 0.5f + Vector3.up * 0.3f;
                rb.AddForceAtPosition(camNormalizedForward * force, forcePos, ForceMode.Impulse);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Vector3 forcePos = camTransform.position + camTransform.right * 0.5f + Vector3.up * 0.3f;
                rb.AddForceAtPosition(camTransform.right * -force, forcePos, ForceMode.Impulse);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Vector3 forcePos = camTransform.position - camNormalizedForward * 0.5f + Vector3.up * 0.3f;
                rb.AddForceAtPosition(camNormalizedForward * -force, forcePos, ForceMode.Impulse);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Vector3 forcePos = camTransform.position - camTransform.right * 0.5f + Vector3.up * 0.3f;
                rb.AddForceAtPosition(camTransform.right * force, forcePos, ForceMode.Impulse);
            }
            else
            {
                moving = false;
                rollTimer = 0f;
            }
        }
        else
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer < 0f) rollTimer = 0f;
        }
        
    }
}
