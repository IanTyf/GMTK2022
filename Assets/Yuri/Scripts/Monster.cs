using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    [SerializeField]
    private float monsterSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (player.GetComponent<playerControllerYuri_test>().mode == playerControllerYuri_test.Mode.Idle)
        {
            //idle
        }
        else
        {
            Debug.Log("Casting");
            moveToPlayer();
            CastRay();
        }
        
        
    }

    void CastRay()
    {
        RaycastHit hit;
        //walking
        if (Physics.Raycast(transform.position, player.transform.position-transform.position,out hit))
        {
             Debug.Log(hit.transform.name);
            if (hit.transform.tag == "unCover")
            {
                Debug.DrawRay(transform.position, player.transform.position-transform.position,Color.red);
                Debug.Log("moving");
                
            }
            else
            {
                Debug.DrawRay(transform.position, player.transform.position-transform.position,Color.green);
                Debug.Log("stop");
            }
        }
        else
        {
            Debug.Log("nothing");
        }
    }
    void moveToPlayer()
    {
        transform.Translate((player.transform.position-transform.position)*Time.deltaTime*monsterSpeed);
    }
}
