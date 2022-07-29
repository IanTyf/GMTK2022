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
        Idle,Partrol,Chase
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
        testingTimer -= Time.deltaTime;
        if (testingTimer < 0)
        {
            PatrolByStep();
            testingTimer = testingTime;
        }
    }

    public void ChasePlayer()
    {
        transform.Translate((player.transform.position-transform.position).normalized*monsterSpeed*10);
    }
    public float Dist_player_Monster()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    public void PatrolByStep()
    {
        if (Vector3.Distance(transform.position, curPoint.transform.position) > monsterSpeed)
        {
            transform.Translate((curPoint.transform.position-transform.position).normalized*1);
        }
        else
        {
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
}
