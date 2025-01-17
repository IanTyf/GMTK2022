using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollCollision : MonoBehaviour
{
    public AudioSource audioSource;
    public Material normalMAT;
    public Material emissionMAT;
    public GameObject eyeball;
    private float timeCount = 0f;
    private bool playing;
    private Animator animator;

    public GameObject rollSoundStuff;


    // Start is called before the first frame update
    void Start()
    {
        playing = false;
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing) 
        {
            timeCount += Time.deltaTime;
        }
        if (timeCount >= 16.0f) 
        {
            EyeBallNormal();
        }

        
        
    }

    public void EyeballLight() 
    {
        eyeball.GetComponent<MeshRenderer>().material = emissionMAT;
        animator.SetBool("isPlaying", true);
    }

    public void EyeBallNormal() 
    {
        eyeball.GetComponent<MeshRenderer>().material = normalMAT;
        animator.SetBool("isPlaying", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                EyeballLight();
                //rollSoundStuff.GetComponent<rollSoundManager>().rollVolumeLevel = rollSoundStuff.GetComponent<rollSoundManager>().tooLoudThreshold + 0.1f;
                Debug.Log("playing sound");

                // doll eye change color
                playing = true;
            }

        }
    }

    
}
