using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posTrigger : MonoBehaviour
{
    public GameObject idlePos;
    public GameObject Monster;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Detected");
            Monster.transform.position = idlePos.transform.position;
            Monster.GetComponent<Monster_Turnbase>().idlePos = idlePos.transform.position;
            //Monster.GetComponent<Monster_Turnbase>().LookAtPlayer();
            Monster.GetComponent<Monster_Turnbase>().idleRot = new Vector3(0,-115.68f,0);
            Monster.transform.rotation = Quaternion.identity;
            Monster.transform.Rotate(new Vector3(0,-115.68f,0),Space.Self);
            Destroy(this.gameObject);
        }
    }
}
