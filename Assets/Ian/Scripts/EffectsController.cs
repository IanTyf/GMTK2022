using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public WoodFloorSound enemySounds;
    public AudioSource heartBeat;
    public GlitchEffect glitchEffect;
    public FogControl fogControl;

    
    public Monster_Turnbase monster;

    public GameManager gm;

    public GameObject mesh;

    public GameObject outsideEndPoint;

    private bool isOutside;
    private float outsideZDist;
    private Color startCol;

    private bool footStepPlayed;

    private float chaseStartDist;
    private float startResettingStrength;
    private float fogResettingDensity;

    // Start is called before the first frame update
    void Start()
    {
        footStepPlayed = false;
        chaseStartDist = -1f;
        fogResettingDensity = 0.1f;
        startResettingStrength = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.GameStarted) return;


        if (isOutside)
        {
            float frac = (outsideEndPoint.transform.position.z - transform.position.z) / outsideZDist;
            float newR = (1 - startCol.r) * (1 - frac) + startCol.r;
            float newG = (1 - startCol.g) * (1 - frac) + startCol.g;
            float newB = (1 - startCol.b) * (1 - frac) + startCol.b;
            mesh.GetComponent<MeshRenderer>().material.color = new Color(newR, newG, newB, frac);
        }
        else
        {
            // enemy sounds
            #region enemy sounds
            if (monster.mode_Monster == Monster_Turnbase.Mode.Partrol)
            {
                if (Random.Range(0f, 1f) < Time.deltaTime * 0.1f)
                {
                    enemySounds.PlayWoodSound();
                }
            }
            /*
            else if (monster.mode_Monster == Monster_Turnbase.Mode.Chase)
            {
                if (!footStepPlayed)
                {
                    enemySounds.PlayFootstep();
                    footStepPlayed = true;
                }
            }
            */
            else
            {
                footStepPlayed = false;
                // stop foot step audio
                enemySounds.StopAll();
            }
            #endregion


            // heartbeat
            #region heartbeat
            if (monster.mode_Monster == Monster_Turnbase.Mode.Attention && chaseStartDist == -1f)
            {
                chaseStartDist = monster.Dist_player_Monster();
            }
            else if (monster.mode_Monster == Monster_Turnbase.Mode.Partrol || monster.mode_Monster == Monster_Turnbase.Mode.Idle)
            {
                chaseStartDist = -1f;
            }

            // chaseStartDist = -1 -> volume 0.05 && pitch 0.7

            // chaseStartDist = max -> volume 0.05 && pitch 0.7
            // chaseStartDist <= monster.killDist -> volume 0.7 && pitch 1.4
            if (chaseStartDist == -1)
            {
                heartBeat.volume -= Time.deltaTime * 0.15f;
                heartBeat.pitch -= Time.deltaTime * 0.15f;

                heartBeat.volume = (heartBeat.volume < 0.05f) ? 0.05f : heartBeat.volume;
                heartBeat.pitch = (heartBeat.pitch < 0.7f) ? 0.7f : heartBeat.pitch;
            }
            else
            {
                float dist = monster.Dist_player_Monster();
                if (dist < monster.KillDist) dist = monster.KillDist;
                float frac = (dist - monster.KillDist) / (chaseStartDist - monster.KillDist);
                heartBeat.volume = (0.7f + frac * -0.65f) * 0.9f + 0.05f;
                heartBeat.pitch = (1.4f + frac * -0.7f) * 0.9f + 0.08f;
            }
            #endregion


            // fog
            /*
            #region fog
            if (chaseStartDist == -1)
            {
                fogControl.FogDensity = 0.1f;
            }
            else
            {
                if (monster.mode_Monster != Monster.Mode.InReseting)
                {
                    float dist = monster.Dist_player_Monster();
                    if (dist < monster.KillDist) dist = monster.KillDist;
                    float frac = (dist - monster.KillDist) / (chaseStartDist - monster.KillDist);
                    fogControl.FogDensity = 1.0f + frac * -0.9f;

                    fogResettingDensity = 0.1f;
                }
                else
                {
                    if (fogResettingDensity == 0.1f) fogResettingDensity = fogControl.FogDensity;

                    float frac = monster.reset_Timer / monster.resetTime;
                    fogControl.FogDensity = fogResettingDensity * frac * 0.9f + 0.1f;
                }
            }
            #endregion
        */

            // glitch effect

            #region glitch effect

            if (monster.mode_Monster == Monster_Turnbase.Mode.Attention && chaseStartDist == -1f)
            {
                chaseStartDist = monster.Dist_player_Monster();
            }
            else if (monster.mode_Monster == Monster_Turnbase.Mode.Partrol || monster.mode_Monster == Monster_Turnbase.Mode.Idle)
            {
                chaseStartDist = -1f;
            }


            if (chaseStartDist == -1)
            {
                glitchEffect.glitchStrength = 0f;
                Color col = mesh.GetComponent<MeshRenderer>().material.color;
                mesh.GetComponent<MeshRenderer>().material.color = new Color(col.r, col.g, col.b, 1f);
            }
            else
            {
                //if (monster.mode_Monster != Monster_Turnbase.Mode.InReseting)
                //{
                float dist = monster.Dist_player_Monster();
                if (dist < monster.KillDist) dist = monster.KillDist;
                float frac = (dist - monster.KillDist) / (chaseStartDist - monster.KillDist);
                glitchEffect.glitchStrength = (1.0f + frac * -1f) * 0.6f + 0.2f;

                Color col = mesh.GetComponent<MeshRenderer>().material.color;
                mesh.GetComponent<MeshRenderer>().material.color = new Color(col.r, col.g, col.b, 1.0f - (1.0f + frac * -1f) * 0.6f);

                startResettingStrength = 0f;
                //}
                /*
                else
                {
                    if (startResettingStrength == 0f) startResettingStrength = glitchEffect.glitchStrength;

                    float frac = monster.reset_Timer / monster.resetTime;
                    glitchEffect.glitchStrength = startResettingStrength * frac;
                }
                */
            }
            #endregion

        }
    }

    public void reachOutside()
    {
        if (isOutside) return;

        isOutside = true;
        outsideZDist = outsideEndPoint.transform.position.z - transform.position.z;
        startCol = mesh.GetComponent<MeshRenderer>().material.color;
        mesh.transform.parent.gameObject.GetComponent<roll>().speedUp(outsideZDist, outsideEndPoint.transform.position.z);
        glitchEffect.glitchStrength = 0f;
        heartBeat.volume = 0.05f;
        heartBeat.pitch = 0.7f;
    }
}
