using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    [SerializeField]
    private float monsterSpeed;

    private float KillDist =3;

    private Vector3 initPos;

    #region Reset
    private float resetTime = 3;
    [SerializeField]
    private float reset_Timer = 0;

    public bool ableToSearchPlayer = false;

    #endregion

    #region Patrol
    public List<GameObject> navPoints = new List<GameObject>();
    [SerializeField]
    private GameObject curPoint;

    private int curIndex;
    private float attentionTime = 2;
    [SerializeField]
    private float attention_Timer = 0;

    public bool Attentioned =false;
    public bool inAttention =false;
    #endregion

    void Start()
    {
        initPos = this.transform.position;
        reset_Timer = resetTime;
        attention_Timer = attentionTime;

        setNewPoint();
        curIndex = (int)Random.Range(0, navPoints.Count);
        curPoint = navPoints[curIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check if player died
        if (!(player.GetComponent<roll>().playerMode == roll.Mode.Dead))
        {
            // player alive
            if (Dist_player_Monster() < KillDist)
            {
                //kill player
                player.GetComponent<roll>().playerMode = roll.Mode.Dead;
            }
            else
            {
                //player move
                if (player.GetComponent<roll>().playerMode == roll.Mode.MakeSound
                    || CastRayDetect())
                {
                    Attentioned = true;
                    inAttention = true;

                }
                //player stop
                else
                {
                    Attentioned = false;
                }

                //start attention timer
                if (attention_Timer > 0&& inAttention)
                {
                    attention_Timer -= Time.deltaTime;
                }
                else
                {
                    inAttention = false;
                }
                
                //attention time passed
                if (!inAttention)
                {
                    //check player state
                    if (Attentioned)
                    {
                        //player walking || uncover
                        //attention player

                        moveToPlayer();
                        reset_Timer = resetTime;
                        ableToSearchPlayer = true;

                    }
                    else
                    {
                        //monster waiting to reset
                        if (reset_Timer > 0 && ableToSearchPlayer)
                        {
                            //counting the timer checking the player
                            reset_Timer -= Time.deltaTime;
                        }
                        else
                        {
                            //reset to patral
                            Patral();
                            reset_Timer = resetTime;
                            attention_Timer = attentionTime;
                            ableToSearchPlayer = false;
                        }
                    }
                }

            }
        }
        else
        {
            // player Die
        }
    }

    public bool CastRayDetect()
    {
        RaycastHit hit;
        //walking
        if (Physics.Raycast(transform.position, player.transform.position-transform.position,out hit))
        {
             //Debug.Log(hit.transform.name);
             //Debug.Log(Vector3.Distance(transform.position,player.transform.position));
            if (hit.transform.tag == "unCover")
            {
                Debug.DrawRay(transform.position, player.transform.position-transform.position,Color.red);
                Debug.Log("moving");
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, player.transform.position-transform.position,Color.green);
                Debug.Log("stop");
                return false;
            }
        }
        else
        {
            Debug.Log("nothing");
            return false;
        }
    }
    void moveToPlayer()
    {
        transform.Translate((player.transform.position-transform.position)*Time.deltaTime*monsterSpeed);
    }

    float Dist_player_Monster()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    void Patral()
    {
        if (Vector3.Distance(transform.position, curPoint.transform.position) <= 0.2)
        {
            setNewPoint();
        }
        else
        {
            transform.Translate((curPoint.transform.position-transform.position)*Time.deltaTime*monsterSpeed);
        }

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
