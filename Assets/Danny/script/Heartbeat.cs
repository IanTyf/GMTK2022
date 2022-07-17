using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    [Range(0.2f,2f)]
    public float HeartbeatGap=1f;
    private IEnumerator coroutine;
    private AudioSource audioSource;
    public bool ifHeartbeat = false;

    void Start()
    {

        audioSource = this.gameObject.GetComponent<AudioSource>();
     
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            StopCoroutine(coroutine);
        }

        if (Input.GetKeyDown(KeyCode.P)) 
        {
            playHeartbeat();
        }

        
    }

    // every 2 seconds perform the print()
    private IEnumerator PlayOnce(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            audioSource.Play();
        }
    }

    public void playHeartbeat() 
    {
        coroutine = PlayOnce(HeartbeatGap);
        StartCoroutine(coroutine);
    }
}
