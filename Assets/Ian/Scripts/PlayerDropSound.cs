using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropSound : MonoBehaviour
{
    public LayerMask surfaceCheck;
    public string currentSurface;

    private AudioSource audioSource;


    public AudioClip woodDropFromHigh;
    public AudioClip woodDropFromLow;

    public AudioClip deskRollSound;
    public AudioClip carpetRollSound;

    public GameObject rollSoundStuff;
    public GameManager gm;

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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f, surfaceCheck))
        {
            Debug.Log("hit " + hit.collider.name);
            if (hit.collider.tag == "standable")
                currentSurface = hit.collider.gameObject.name;
        }

        if (oldName.Equals(currentSurface)) return;

        switch (currentSurface)
        {
            case "Desk":
                updateVolumeAndPitch(0.10f, 0.8f);
                updateSound(deskRollSound);
                break;
            case "Floor":
                updateVolumeAndPitch(0.10f, 0.8f);
                updateSound(deskRollSound);
                break;
            case "Counter":
                updateVolumeAndPitch(0.8f, 1.24f);
                updateSound(carpetRollSound);
                break;
            case "Carpet":
                updateVolumeAndPitch(0.8f, 1.24f);
                updateSound(carpetRollSound);
                break;
            case "Bed":
                updateVolumeAndPitch(0.8f, 1.24f);
                updateSound(carpetRollSound);
                break;
        }

        if (oldName.Equals("Desk"))
        {
            if (currentSurface.Equals("Floor"))
            {
                audioSource.PlayOneShot(woodDropFromHigh);
                // biss
                gm.dieInstantly();
            }
        }
        else if (oldName.Equals("Counter"))
        {
            if (currentSurface.Equals("Floor"))
            {
                audioSource.PlayOneShot(woodDropFromLow);
                // attention
                rollSoundStuff.GetComponent<rollSoundManager>().rollVolumeLevel = rollSoundStuff.GetComponent<rollSoundManager>().tooLoudThreshold + 0.1f;
            }
        }
        else if (oldName.Equals("Bed"))
        {
            if (currentSurface.Equals("Floor"))
            {
                audioSource.PlayOneShot(woodDropFromLow);
                // attention
                rollSoundStuff.GetComponent<rollSoundManager>().rollVolumeLevel = rollSoundStuff.GetComponent<rollSoundManager>().tooLoudThreshold + 0.1f;
            }
        }
    }

    private void updateSound(AudioClip clip)
    {
        for (int i=0; i<rollSoundStuff.transform.childCount; i++)
        {
            Debug.Log("updated walk sound");
            rollSoundStuff.transform.GetChild(i).GetComponent<rollSound>().clip = clip;
        }
    }

    private void updateVolumeAndPitch(float volume, float pitch)
    {
        for (int i = 0; i < rollSoundStuff.transform.childCount; i++)
        {
            rollSoundStuff.transform.GetChild(i).GetComponent<AudioSource>().volume = volume;
            rollSoundStuff.transform.GetChild(i).GetComponent<AudioSource>().pitch = pitch;
        }
    }
}
