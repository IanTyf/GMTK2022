using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollSound : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource audioSource;

    private float cd = 0.8f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 1)
            timer += Time.deltaTime;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") return;
        if (timer < cd) return;
        //rollSoundManager rsm = transform.parent.GetComponent<rollSoundManager>();
        //float volume = rsm.rollVolumeLevel / 2 + 0.3f;
        //if (rsm.rollVolumeLevel > rsm.tooLoudThreshold) volume = 1;
        audioSource.PlayOneShot(clip, audioSource.volume);
        //audioSource.volume = volume;
        //audioSource.clip = clip;
        //audioSource.Play();
        Debug.Log(other.name);
        timer = 0f;
        //rsm.addVolume();
    }
}
