using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    [SerializeField]
    private float monsterSpeed;

    [SerializeField]
    public float KillDist =3;
    

    public enum Mode
    {
        Partrol,InAttention,Chase,InReseting
    }

    public Mode mode_Monster;
    #region Reset
    public float resetTime = 3;
    [SerializeField]
    public float reset_Timer = 0;

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

    public LayerMask coverDetect;
    void Start()
    {
        reset_Timer = resetTime;
        attention_Timer = attentionTime;

        setNewPoint();
        curIndex = (int)Random.Range(0, navPoints.Count);
        curPoint = navPoints[curIndex];
        
        mode_Monster =  Mode.Partrol;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CastRayDetect())
        {
            
        }
        //Update monster_mode
        //check make sound
        if (player.GetComponent<roll>().playerMode == roll.Mode.MakeSound&&mode_Monster==Mode.Partrol)
        {
            mode_Monster = Mode.InAttention;
        }
        

        #region check inAttention

        if (mode_Monster == Mode.InAttention)
        {
            
        }
        #endregion

        #region Chase

        if (mode_Monster == Mode.Chase)
        {
            
            
        }


        #endregion
        
        #region inReseting

        if (mode_Monster == Mode.InReseting)
        {
            
        }


        #endregion

        #region Patoral

        if (mode_Monster == Mode.Partrol)
        {
            
        }
        #endregion
        switch (mode_Monster)
        {
            case Mode.InAttention:
                Debug.Log("InAttention");
                //check if kill player
                if (Dist_player_Monster() < KillDist)
                {
                    //kill player
                    player.GetComponent<roll>().playerMode = roll.Mode.Dead;
                }
                else
                {
                    //start attention timer
                    if (attention_Timer > 0)
                    {
                        attention_Timer -= Time.deltaTime;
                        //check chase or not
                        if (player.GetComponent<roll>().moving
                            || CastRayDetect())
                        {
                            //update to chase mode
                            mode_Monster = Mode.InAttention;
                        }
                        else
                        {
                            //mode_Monster = Mode.InAttention;
                        }
                    }
                    else
                    {
                        if (player.GetComponent<roll>().moving
                            || CastRayDetect())
                        {
                            //update to chase mode
                            mode_Monster = Mode.Chase;
                        }
                        else
                        {
                            mode_Monster = Mode.Partrol;
                        }
                        
                    }
                
                }
                break;
            case Mode.Chase:
                Debug.Log("chasing");
                if (player.GetComponent<roll>().moving
                    || CastRayDetect())
                {
                    //chase
                    if (Dist_player_Monster() < KillDist)
                    {
                        //kill player
                        player.GetComponent<roll>().playerMode = roll.Mode.Dead;
                    }
                    else
                    {
                        //在追 还没死
                        mode_Monster = Mode.Chase;
                        moveToPlayer();
                        reset_Timer = resetTime;
                    }

                }
                else
                {
                    mode_Monster = Mode.InReseting;
                }
                break;
            case Mode.InReseting:
                Debug.Log("inReseting");
                if (reset_Timer > 0 )
                {
                    //counting the timer checking the player
                    reset_Timer -= Time.deltaTime;
                }
                else
                {
                    if (player.GetComponent<roll>().moving
                        || CastRayDetect())
                    {
                        mode_Monster = Mode.Chase;
                    }
                    else
                    {
                        mode_Monster = Mode.Partrol;

                    }
                }
                break;
            case Mode.Partrol:
                Debug.Log("Partol");
                //reset to patral
                Patral();
                reset_Timer = resetTime;
                attention_Timer = attentionTime;
                break;
        }
        
        
        /*#region old

        

        
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
        #endregion*/
    }

    public bool CastRayDetect()
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
    void moveToPlayer()
    {
        transform.Translate((player.transform.position-transform.position)*Time.deltaTime*monsterSpeed);
    }

    public float Dist_player_Monster()
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
