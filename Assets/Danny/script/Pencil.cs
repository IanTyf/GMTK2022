using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    public Vector3 RollTorque;
    private Rigidbody selfRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        selfRigidBody = this.gameObject.GetComponent<Rigidbody>();
    }


    public void PencilTouched() 
    {
        selfRigidBody.AddRelativeTorque(RollTorque, ForceMode.Impulse);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("player")) 
        {
            PencilTouched();
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerTrigger")
        {
            PencilTouched();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.K))
        {
            PencilTouched();
        }
        */
    }
}
