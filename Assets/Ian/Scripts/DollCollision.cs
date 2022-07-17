using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollCollision : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerTrigger")
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();

                // doll eye change color

            }

        }
    }
}
