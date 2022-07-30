using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollSoundManager : MonoBehaviour
{
    public float rollVolumeLevel;
    public float tooLoudThreshold;

    // Start is called before the first frame update
    void Start()
    {
        rollVolumeLevel = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.parent.GetComponent<roll>().playerMode == roll.Mode.Dead) return;

        if (rollVolumeLevel > 0)    rollVolumeLevel -= Time.deltaTime * 1.2f;

        if (rollVolumeLevel > tooLoudThreshold)
        {
            Debug.Log("TOO LOUD");
            transform.parent.GetComponent<roll>().playerMode = roll.Mode.MakeSound;
        }
        else
        {
            //transform.parent.GetComponent<roll>().playerMode = roll.Mode.Idle;
        }
    }

    public void addVolume()
    {
        rollVolumeLevel += 0.7f;
    }
}
