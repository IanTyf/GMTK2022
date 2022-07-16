using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollSound : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource audioSource;

    private float cd = 0.5f;
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
        audioSource.PlayOneShot(clip);
        timer = 0f;
        transform.parent.GetComponent<rollSoundManager>().addVolume();
    }
}
