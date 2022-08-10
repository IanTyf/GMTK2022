using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterMesh : MonoBehaviour
{
    private Transform patrolStates;
    private Transform attentionStates; // 0 is front, 1 is left, 2 is right
    private Transform chaseStates;

    public float shortTurnOffset;
    public float largeTurnOffset;

    // Start is called before the first frame update
    void Start()
    {
        patrolStates = transform.GetChild(0);
        attentionStates = transform.GetChild(1);
        chaseStates = transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePatrolState()
    {
        disableAll();
        patrolStates.GetChild(Random.Range(0, patrolStates.childCount)).gameObject.SetActive(true);
    }

    public void updateAttentionState(Vector3 curLookDir, Vector3 playerDir)
    {
        disableAll();
        float turnAngle = Vector3.SignedAngle(curLookDir, playerDir, Vector3.up);
        if (Mathf.Abs(turnAngle) < 25f)
        {
            // turn front
            attentionStates.GetChild(0).gameObject.SetActive(true);
        }
        else if (Mathf.Abs(turnAngle) < 90f)
        {
            if (turnAngle > 0) 
            {
                transform.Rotate(new Vector3(0f, shortTurnOffset, 0f));
                attentionStates.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                transform.Rotate(new Vector3(0f, -shortTurnOffset, 0f));
                attentionStates.GetChild(4).gameObject.SetActive(true);
            }
        }
        else
        {
            if (turnAngle > 0)
            {
                transform.Rotate(new Vector3(0f, largeTurnOffset, 0f));
                attentionStates.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                transform.Rotate(new Vector3(0f, -largeTurnOffset, 0f));
                attentionStates.GetChild(3).gameObject.SetActive(true);
            }
        }
    }

    public void updateChaseNormalState()
    {
        disableAll();
        chaseStates.GetChild(Random.Range(0, 2)).gameObject.SetActive(true);
    }

    public void updateChaseCloseState()
    {
        disableAll();
        chaseStates.GetChild(2).gameObject.SetActive(true);
    }
    
    public void updateChaseDeadState()
    {
        disableAll();
        chaseStates.GetChild(3).gameObject.SetActive(true);
    }

    public void updateIdleState()
    {
        disableAll();
        patrolStates.GetChild(0).gameObject.SetActive(true);
    }

    private void disableAll()
    {
        //transform.rotation = Quaternion.identity;
        transform.localRotation = quaternion.identity;

        for (int i=0; i<patrolStates.childCount; i++)
        {
            patrolStates.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < attentionStates.childCount; i++)
        {
            attentionStates.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < chaseStates.childCount; i++)
        {
            chaseStates.GetChild(i).gameObject.SetActive(false);
        }
    }
}
