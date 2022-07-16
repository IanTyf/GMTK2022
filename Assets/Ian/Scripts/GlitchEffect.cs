using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    public Material mat;

    public float glitchStrength;


    public float frequency;
    private float frequencyTimer;

    private float increase;
    private int direction;

    private int posneg;

    // Start is called before the first frame update
    void Start()
    {
        glitchStrength = 0.0f;
        increase = 0;
        direction = 0;
        posneg = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //increase = 1.01f;
            direction = 1;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //increase = 0.99f;
            direction = -1;
        }

        glitchStrength += Time.deltaTime * direction * 0.5f;
        if (glitchStrength > 1) glitchStrength = 1;
        if (glitchStrength < 0) glitchStrength = 0;

        float s = 1 / Mathf.Pow((1 + Mathf.Exp(-30 * (glitchStrength - 0.5f))), 0.3f);

        frequencyTimer += Time.deltaTime;
        if (frequencyTimer > frequency)
        {
            frequencyTimer = 0f;
            frequency *= 0.9f;
            if (frequency < Time.deltaTime) frequency = Time.deltaTime;
            
        }
            mat.SetFloat("_Strength", 0.1f * s * Random.Range(-1.5f, 1.5f));
            posneg *= -1;
    }
}
