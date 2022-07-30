using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Turnbase : MonoBehaviour
{
    //patrol 
    
    #region reference
    public GameObject player;
    public GameManager gm;
    public LayerMask coverDetect;
    #endregion
    
    #region Monster Property
    [SerializeField]
    private float monsterSpeed;
    [SerializeField]
    public float KillDist =3;
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
    #endregion
    

    void Start()
    {
        mode_Monster = Mode.Idle;
        testingTimer = testingTime;
        
        setNewPoint();
        curIndex = Random.Range(0, navPoints.Count);
        curPoint = navPoints[curIndex];
    }
    
    void Update()
    {
        /*testingTimer -= Time.deltaTime;
        if (testingTimer < 0)
        {
            PatrolByStep();
            testingTimer = testingTime;
        }*/
    }

    public void MonsterRound(bool prePlayerExpo, bool curPlayerExpo)
    {
        //check if player dead
        if (Dist_player_Monster() < KillDist)
        {
            //kill player
            player.GetComponent<roll>().playerMode = roll.Mode.Dead;
        }
        else
        //判断行动
        {
            //怪物根据上一回合玩家的位置行动
            //上一回合被发现
            switch (mode_Monster)
            {
                case Mode.Partrol:
                    if (prePlayerExpo)
                    {
                        mode_Monster = Mode.Attention;
                    }
                    break;
                case Mode.Attention:
                    if (prePlayerExpo)
                    {
                        mode_Monster = Mode.Chase;
                    }
                    else
                    {
                        mode_Monster = Mode.Partrol;
                    }
                    break;
                case Mode.Chase:
                    if (prePlayerExpo)
                    {
                        mode_Monster = Mode.Chase;
                    }
                    else
                    {
                        mode_Monster = Mode.Partrol;
                    }
                    break;
            }
        }
        
        //execute monster behavior
        switch (mode_Monster)
        {
            case Mode.Idle:
                break;
            case Mode.Attention:
                LookAtPlayer();
                break;
            case Mode.Partrol:
                PatrolByStep();
                break;
            case Mode.Chase:
                ChasePlayer();
                break;
        }
    }

    public void LookAtPlayer()
    {
        transform.LookAt(player.transform.position);
    }
    public void ChasePlayer()
    {
        transform.Translate((player.transform.position-transform.position).normalized*monsterSpeed*1);
    }
    public float Dist_player_Monster()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    public void PatrolByStep()
    {
        if (Vector3.Distance(transform.position, curPoint.transform.position) > monsterSpeed)
        {
            transform.LookAt(curPoint.transform.position);
            transform.Translate((curPoint.transform.position-transform.position).normalized*1);
        }
        else
        {
            transform.LookAt(curPoint.transform.position);
            transform.position = curPoint.transform.position;
            setNewPoint();
        }
        //transform.position = navPoints[(int) Random.Range(0,navPoints.Count)].transform.position;
    }
    
    void setNewPoint()
    {
        int newIndex = (int)Random.Range(0, navPoints.Count);
        while (newIndex == curIndex)
        {
            newIndex = (int)Random.Range(0, navPoints.Count);
        }
        
        curIndex = newIndex;
        curPoint = navPoints[curIndex];
    }
    public bool DetectPlayer()
    {
        RaycastHit hit;
        //walking
        if (Physics.Raycast(transform.position, player.transform.position-transform.position,out hit,Mathf.Infinity,coverDetect))
        {
            //Debug.Log(hit.transform.name);
            //Debug.Log(Vector3.Distance(transform.position,player.transform.position));
            if (hit.transform.tag == "unCover")
            {
                Debug.DrawRay(transform.position, player.transform.position-transform.position,Color.red);
                //Debug.Log("moving");
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, player.transform.position-transform.position,Color.green);
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
}
