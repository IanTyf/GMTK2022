using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropSound : MonoBehaviour
{
    public string currentSurface;

    private AudioSource audioSource;


    public AudioClip woodDropFromHigh;
    public AudioClip woodDropFromLow;

    public AudioClip deskRollSound;
    public AudioClip carpetRollSound;

    public GameObject rollSoundStuff;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        string oldName = currentSurface;

        // update current surface
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f))
        {
            if (hit.collider.tag == "standable")
                currentSurface = hit.collider.gameObject.name;
        }

        if (oldName.Equals(currentSurface)) return;

        switch (currentSurface)
        {
            case "Desk":
                updateSound(deskRollSound);
                break;
            case "Floor":
                updateSound(deskRollSound);
                break;
            case "Counter":
                updateSound(carpetRollSound);
                break;
            case "Carpet":
                updateSound(carpetRollSound);
                break;
            case "Bed":
                updateSound(carpetRollSound);
                break;
        }

        if (oldName.Equals("Desk"))
        {
            if (currentSurface.Equals("Floor"))
            {
                audioSource.PlayOneShot(woodDropFromHigh);
                // biss

            }
        }
        else if (oldName.Equals("Counter"))
        {
            if (currentSurface.Equals("Floor"))
            {
                audioSource.PlayOneShot(woodDropFromLow);
                // attention

            }
        }
        else if (oldName.Equals("Bed"))
        {
            if (currentSurface.Equals("Floor"))
            {
                audioSource.PlayOneShot(woodDropFromLow);
                // attention

            }
        }
    }

    private void updateSound(AudioClip clip)
    {
        for (int i=0; i<rollSoundStuff.transform.childCount; i++)
        {
            rollSoundStuff.transform.GetChild(i).GetComponent<AudioSource>().clip = clip;
        }
    }
}
