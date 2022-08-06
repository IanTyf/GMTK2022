using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMesh : MonoBehaviour
{
    public Mesh[] patrolStates;
    public Mesh[] attentionStates; // 0 is front, 1 is left, 2 is right
    public Mesh[] chaseStates;

    public float attentionAngleOffset;

    private MeshFilter mf;
    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePatrolState()
    {
        mf.mesh = patrolStates[Random.Range(0, patrolStates.Length)];
    }

    public int updateAttentionState(Vector3 curLookDir, Vector3 playerDir)
    {
        float turnAngle = Vector3.SignedAngle(curLookDir, playerDir, Vector3.up);
        if (Mathf.Abs(turnAngle) < 25f)
        {
            // turn front
            mf.mesh = attentionStates[0];
            return 0;
        }
        else if (turnAngle < 0)
        {
            // turn right
            mf.mesh = attentionStates[1];
            return 1;
        }
        else
        {
            // turn left
            mf.mesh = attentionStates[2];
            return 2;
        }
        //mf.mesh = 
    }

    public void updateChaseNormalState()
    {
        mf.mesh = chaseStates[0];
    }

    public void updateChaseCloseState()
    {
        mf.mesh = chaseStates[1];
    }
    
    public void updateChaseDeadState()
    {
        mf.mesh = chaseStates[2];
    }
}
