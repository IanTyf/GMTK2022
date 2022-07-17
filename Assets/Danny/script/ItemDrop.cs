using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    private AudioSource audio;
    public AudioClip[] audioClips;

    public bool dropped;

    // Start is called before the first frame update
    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
        dropped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void dropSound() 
    {
        if (dropped) return;
        dropped = true;
        int clipIndex;
        clipIndex = Mathf.Max(0, UnityEngine.Random.Range(0, audioClips.Length - 1));
        audio.clip = audioClips[clipIndex];
        audio.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Floor")) 
        {
            dropSound();
        }
    }
}
