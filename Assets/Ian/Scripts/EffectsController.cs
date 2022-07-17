using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public WoodFloorSound enemySounds;
    public GameObject heartBeat;
    public Material glitchMat;
    public GameObject fogControl;

    
    public Monster monster;

    private bool footStepPlayed;

    // Start is called before the first frame update
    void Start()
    {
        footStepPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (monster.mode_Monster == Monster.Mode.Partrol)
        {
            if (Random.Range(0f, 1f) < Time.deltaTime)
            {
                //enemySounds.PlayWoodSound();
            }
        }
        else if (monster.mode_Monster == Monster.Mode.Chase)
        {
            if (!footStepPlayed)
            {
                //enemySounds.PlayFootstep();
                footStepPlayed = true;
            }
        }
        else
        {
            footStepPlayed = false;
            // stop foot step audio
            enemySounds.StopAll();
        }
    }
}
