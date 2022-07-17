using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCollision : MonoBehaviour
{
    public GameObject rollSoundStuff;
    public Computer computer;
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
            computer.TurnOnScreen();
            rollSoundStuff.GetComponent<rollSoundManager>().rollVolumeLevel = rollSoundStuff.GetComponent<rollSoundManager>().tooLoudThreshold + 0.1f;
        }
    }
}
