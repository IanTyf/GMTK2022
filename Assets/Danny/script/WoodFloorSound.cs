using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFloorSound : MonoBehaviour
{
    public Vector2[] clipCuts;
    private AudioSource audioSource;
    public AudioSource stepAudio;
    //public AudioClip woodClip;
    
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            Debug.Log(index);
            PlayWoodSound();
        }
        

        if (audioSource.time >= clipCuts[index].y)
        {
            audioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.U)) 
        {
            PlayFootstep();
        }
        
    }

    public void PlayWoodSound() 
    {
        stepAudio.Stop();
        index = Mathf.Max(0, UnityEngine.Random.Range(0, clipCuts.Length - 1));
        //audioSource.clip = woodClip;
        audioSource.time = clipCuts[index].x;
        audioSource.Play();
    }

    public void PlayFootstep() 
    {
        audioSource.Stop();
        stepAudio.Play();
    }
}
