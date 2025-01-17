using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster_Turnbase : MonoBehaviour
{
    //patrol 
    
    #region reference
    public GameObject player;
    public GameManager gm;
    public LayerMask coverDetect;
    public MonsterMesh monsterMesh;
    #endregion
    
    #region Monster Property
    [SerializeField]
    private float monsterSpeed;
    [SerializeField]
    private float monsterChaseSpeed;
    [SerializeField]
    public float KillDist =1f;
    [SerializeField]
    public float CloseDist = 5f;

    public float KillOffset;

    public Vector3 idlePos;
    public Vector3 idleRot;
    #endregion

    #region Level_Desk

    public bool onDesk;
    public bool onCounter;
    public bool onFloor;

    public bool monsterOnFloor;

    #endregion
    #region Behaviors
    public enum Mode
    {
        Idle,Partrol,Attention,Chase
    }
    
    public Mode mode_Monster;


    #endregion


    #region Patrol
    public List<GameObject> navPoints = new List<GameObject>();
    [SerializeField]
    private GameObject curPoint;
    [SerializeField]
    private int curIndex;
    
    private float testingTime = 2;
    [SerializeField]
    private float testingTimer = 0;

    public GameObject idlePosTrigger;
    #endregion


    #region Patrolhit

    private Vector3 curPos;
    [SerializeField] private int stepTry = 1000;
    [SerializeField] private bool HitObstacle =false;
    private Vector3 curPlayerPosXZ;
    private float Height;
    private float offsetY;
    [SerializeField] 
    private bool SetOneFrame;

    public GameObject DetectRay,DetectRayL,DetectRayR;
    #endregion
    void Start()
    {
        idlePos = transform.position;
        idleRot = transform.rotation.eulerAngles;
        //mode_Monster = Mode.Idle;
        testingTimer = testingTime;

        if (mode_Monster == Mode.Partrol)
        {
            setNewPoint();
            curIndex = Random.Range(0, navPoints.Count);
            curPoint = navPoints[curIndex];
        }
        
        curPos = transform.position;
        Debug.Log("start pos: "+ curPos);

        monsterOnFloor = false;

        //onDesk = true;
    }
    
    void Update()
    {
        updatePlayerPosXZ();
        //Debug.DrawRay();

        //Debug.Log("angle:"+Vector3.Angle(player.transform.position - transform.position, transform.forward));
        
        
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(DetectRay.transform.position,Vector3.down*100,Color.magenta);
        Debug.DrawRay(DetectRayL.transform.position,Vector3.down*100,Color.magenta);
        Debug.DrawRay(DetectRayR.transform.position,Vector3.down*100,Color.magenta);
        //RaycastHit HitBarrier;
        if (mode_Monster == Mode.Chase && SetOneFrame)
        {
            Debug.Log("Modified Move");
            RayDetect();
        }
    }

    public void RayDetect()
    {
        RaycastHit RayDetectHit;
        //try forward;
        for (int i = 0; i < stepTry; i++)
        {
            #region try forward
            //try forward
            transform.position = curPos;
            Debug.Log("Try forward");
            transform.Translate((curPlayerPosXZ-curPos).normalized*(i+1)*.1f*monsterChaseSpeed,Space.World);
            if (Physics.Raycast(DetectRay.transform.position, Vector3.down, out RayDetectHit, Mathf.Infinity))
            {
                //D
                Debug.Log("Hit Name:"+ RayDetectHit.transform.name);
                if (((RayDetectHit.transform.gameObject.name == "Desk" && !monsterOnFloor)||(RayDetectHit.transform.gameObject.name == "Floor" && monsterOnFloor)) 
                    &&!SideDetectHit())
                {
                    Debug.Log("modified move forward successfully");
                    SetOneFrame = false;
                    break;
                }
                else
                {
                    #region TryLeft
                    //try left
                    transform.position = curPos;
                    Debug.Log("Try left");
                    transform.Translate(-transform.right*(i+1)*.1f*monsterChaseSpeed,Space.World);
                    if (Physics.Raycast(DetectRay.transform.position, Vector3.down, out RayDetectHit, Mathf.Infinity))
                    {
                        if (((RayDetectHit.transform.gameObject.name == "Desk" && !monsterOnFloor) || (RayDetectHit.transform.gameObject.name == "Floor" && monsterOnFloor))
                            && !SideDetectHit())
                        {
                            Debug.Log("modified move forward successfully");
                            SetOneFrame = false;
                            break;
                        }
                        else
                        {
                            #region Try Right
                            //try right
                            transform.position = curPos;
                            Debug.Log("Try right");
                            transform.Translate(transform.right*(i+1)*.1f*monsterChaseSpeed,Space.World);
                            if (Physics.Raycast(DetectRay.transform.position, Vector3.down, out RayDetectHit, Mathf.Infinity))
                            {
                                if (((RayDetectHit.transform.gameObject.name == "Desk" && !monsterOnFloor) || (RayDetectHit.transform.gameObject.name == "Floor" && monsterOnFloor))
                                    && !SideDetectHit())
                                {
                                    Debug.Log("modified move forward successfully");
                                    SetOneFrame = false;
                                    break;
                                }
                                else
                                {
                                    //failed
                                    
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                }
            }
            #endregion
        }
    }

    public bool SideDetectHit()
    {
        //return true when hit item instead of ground
        RaycastHit RayDetectHit_L,RayDetectHit_R;
        if (Physics.Raycast(DetectRayL.transform.position, Vector3.down, out RayDetectHit_L, Mathf.Infinity))
        {
            if (RayDetectHit_L.transform.tag == "ground")
            {
                if (Physics.Raycast(DetectRayR.transform.position, Vector3.down, out RayDetectHit_R, Mathf.Infinity))
                {
                    if (RayDetectHit_R.transform.tag == "ground")
                    {
                        //just hit ground
                        return false;
                    }
                    else
                    {
                        //hit item
                        return true;

                    }
                }
                else
                {
                    //nothing hitted
                    return false;
                }
            }
            else
            {
                //hit item
                return true;
            }
        }
        else
        {
            //nothing hitted
            return false;
        }
    }
    public void MonsterRound(bool prePlayerExpo, bool curPlayerExpo)
    {
        /*//check if player dead
        if (Dist_player_Monster() < KillDist)
        {
            //kill player
            player.GetComponent<roll>().playerMode = roll.Mode.Dead;
        }
        else
        
        {
            
        }*/
        //判断行动
        //怪物根据玩家的位置行动
        //上一回合被发现
        switch (mode_Monster)
        {
            case Mode.Idle:
                if (curPlayerExpo)
                {
                    mode_Monster = Mode.Attention;
                }
                if (onFloor)
                {
                    setNewPoint();
                    transform.position = navPoints[0].transform.position;
                    curPos = transform.position;
                    mode_Monster = Mode.Partrol;
                    updatePlayerPosXZ();
                }
                break;
            case Mode.Partrol:
                if (curPlayerExpo)
                {
                    mode_Monster = Mode.Attention;
                }
                break;
            case Mode.Attention:
                if (curPlayerExpo)
                {
                    //player on desk
                    if (onDesk)
                    {
                        mode_Monster = Mode.Chase;
                    }
                    
                    //player on counter
                    if (onCounter)
                    {
                        mode_Monster = Mode.Idle;
                    }

                    //player on floor
                    if (onFloor)
                    {
                        mode_Monster = Mode.Chase;
                    }
                }
                else
                {
                    if (onDesk)
                    {
                        transform.position = idlePos;
                        transform.rotation = Quaternion.identity;
                        transform.Rotate(idleRot,Space.Self);
                        mode_Monster = Mode.Idle;
                    }
                    
                    if (onCounter)
                    {
                        mode_Monster = Mode.Idle;
                    }
                    
                    //player on floor
                    if (onFloor)
                    {
                        if (!monsterOnFloor)
                        {
                            setNewPoint();
                            transform.position = navPoints[0].transform.position;
                            curPos = transform.position;
                            updatePlayerPosXZ();
                        }
                        mode_Monster = Mode.Partrol;
                    }

                }
                break;
            case Mode.Chase:
                if (curPlayerExpo)
                {
                    //player on desk
                    if (onDesk)
                    {
                        mode_Monster = Mode.Chase;
                    }
                    
                    if (onCounter)
                    {
                        mode_Monster = Mode.Idle;
                    }

                    //player on floor
                    if (onFloor)
                    {
                        mode_Monster = Mode.Chase;
                    }
                }
                else
                {
                    if (onDesk)
                    {
                        transform.position = idlePos;
                        mode_Monster = Mode.Idle;
                        transform.rotation = Quaternion.identity;
                        transform.Rotate(idleRot,Space.Self);
                    }
                    
                    if (onCounter)
                    {
                        mode_Monster = Mode.Idle;
                    }
                    
                    //player on floor
                    if (onFloor)
                    {
                        if (!monsterOnFloor)
                        {
                            setNewPoint();
                            transform.position = navPoints[0].transform.position;
                            curPos = transform.position;
                            updatePlayerPosXZ();
                        }
                        mode_Monster = Mode.Partrol;
                    }
                }
                break;
        }
        
        //execute monster behavior
        switch (mode_Monster)
        {
            case Mode.Idle:
                monsterMesh.updateIdleState();
                break;
            case Mode.Attention:
                monsterMesh.updateAttentionState(transform.forward, curPlayerPosXZ - transform.position);
                LookAtPlayer();
                break;
            case Mode.Partrol:
                monsterMesh.updatePatrolState();
                PatrolByStep();
                break;
            case Mode.Chase:
                LookAtPlayer();
                ChasePlayer();
                LookAtPlayer();

                monsterMesh.updateChaseNormalState();
                break;
        }
        if (Dist_player_Monster() < CloseDist && mode_Monster == Mode.Chase)
        {
            monsterMesh.updateChaseCloseState();
        }

        if (Dist_player_Monster() < KillDist  && mode_Monster == Mode.Chase)
        {
            //kill player
            // TODO: manually set monster position
            //transform.Translate((curPlayerPosXZ - curPos).normalized * (Dist_player_Monster() - KillOffset), Space.World);
            transform.position = new Vector3(curPlayerPosXZ.x, transform.position.y, curPlayerPosXZ.z) + new Vector3(transform.position.x - curPlayerPosXZ.x, 0f, transform.position.z - curPlayerPosXZ.z).normalized * KillOffset;
            //+ new Vector3(curPos.x - curPlayerPosXZ.x, 0f, curPos.z - curPlayerPosXZ.z).normalized * KillOffset;
            //LookAtPlayer();
            transform.position = transform.position + transform.right * 0.2f;

            monsterMesh.updateChaseDeadState();
            player.GetComponent<roll>().playerMode = roll.Mode.Dead;
            gm.NewDeath();
            SetOneFrame = false;
        }
    }

    public void LookAtPlayer()
    {
        transform.LookAt(curPlayerPosXZ);
    }
    public void ChasePlayer()
    {
        
        
        //normal move
        transform.Translate((curPlayerPosXZ-curPos).normalized*monsterChaseSpeed*1f,Space.World);
        Debug.Log("CurPos: "+curPos);

        curPos = transform.position;
        
        RaycastHit ChasePlayerHit;
        //check hit
        if (Physics.Raycast(DetectRay.transform.position, Vector3.down, out ChasePlayerHit, Mathf.Infinity))
        {
            if (ChasePlayerHit.transform.tag == "ground")
            {
                //check ground
                if (SideDetectHit())
                {
                    //hit something else
                    SetOneFrame = true;
                }
                else
                {
                    
                }
            }
            else
            {
                //hit something else
                SetOneFrame = true;
            }
        }
        else
        {
            //正常走
            SetOneFrame = false;
        }
    }


    #region OnTriggerChecek
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            HitObstacle = true;
        }
        //Debug.Log("OnTriggerEnter"+other.gameObject.name);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
           HitObstacle = true;
        }
        //Debug.Log("OnTriggerStay"+other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        HitObstacle = false;
        //Debug.Log("OnTriggerExit"+other.gameObject.name);
    }
    #endregion
    

    public float Dist_player_Monster()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    public float Dist_player_Monster_XZ()
    {
        return Vector3.Distance(transform.position, curPlayerPosXZ);
    }

    public void PatrolByStep()
    {
        if (!monsterOnFloor)
        {
            LookAtPlayer();
            monsterOnFloor = true;
            return;
        }


        if (Vector3.Distance(transform.position, curPoint.transform.position) > monsterSpeed)
        {
            transform.LookAt(curPoint.transform.position);
            transform.Translate((curPoint.transform.position-transform.position).normalized*1*monsterSpeed,Space.World);
            //Debug.Log(curPoint.transform.position);
        }
        else
        {
            transform.position = curPoint.transform.position;
            setNewPoint();
            transform.LookAt(curPoint.transform.position);
            //Debug.Log("Switch point");
        }
        //transform.position = navPoints[(int) Random.Range(0,navPoints.Count)].transform.position;
        curPos = transform.position;

    }
    
    void setNewPoint()
    {
        /*int newIndex = (int)Random.Range(0, navPoints.Count);
        while (newIndex == curIndex)
        {
            newIndex = (int)Random.Range(0, navPoints.Count);
        }*/
        
        if (curIndex < navPoints.Count - 1)
        {
            curIndex++;
        }
        else
        {
            curIndex = 0;
        }
        curPoint = navPoints[curIndex];
    }
    public bool DetectPlayer()
    {
        RaycastHit hit;
        //walking
        if (Physics.Raycast(DetectRay.transform.position, player.transform.position- DetectRay.transform.position,out hit,Mathf.Infinity))
        {
            //Debug.Log(hit.transform.name);
            //Debug.Log(Vector3.Distance(transform.position,player.transform.position));
            if (hit.transform.tag == "Player")
            {
                Debug.DrawRay(DetectRay.transform.position, player.transform.position- DetectRay.transform.position,Color.red);
                //Debug.Log("moving");
                return true;
            }
            else
            {
                Debug.DrawRay(DetectRay.transform.position, player.transform.position- DetectRay.transform.position,Color.green);
                //Debug.Log("stop");
                return false;
            }
        }
        else
        {
            Debug.Log("nothing");
            return false;
        }
    }

    public bool PatrolDetectPlayer()
    {
        RaycastHit coneHit;
        if (Physics.Raycast(DetectRay.transform.position, player.transform.position- DetectRay.transform.position, out coneHit, 15f))
        {
            //hit 到 夹角<15 >-15
            Debug.Log("angle:"+Vector3.Angle(player.transform.position - transform.position, transform.forward));
            if (Vector3.Angle(player.transform.position - transform.position, transform.forward) < 30)
            {
                if (coneHit.transform.tag == "Player")
                {
                    Debug.DrawRay(transform.position, player.transform.position-transform.position,Color.blue);
                    Debug.Log("moving");
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            //没hit到
            {
                Debug.DrawRay(transform.position, player.transform.position-transform.position,Color.black);
                //Debug.Log("moving");
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private void updatePlayerPosXZ()
    {
        curPlayerPosXZ.x = player.transform.position.x;
        curPlayerPosXZ.z = player.transform.position.z;
        curPlayerPosXZ.y = transform.position.y;

        Debug.DrawRay(transform.position, transform.forward * 15, Color.yellow);
    }
}
