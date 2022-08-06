using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoom : MonoBehaviour
{
    private bool exited;

    private AudioSource ambientSound;
    public bool increaseAmbient;
    public GameManager gm;

    public Material transMat;
    public MeshRenderer playerMesh;
    // Start is called before the first frame update
    void Start()
    {
        exited = false;
        increaseAmbient = false;

        GameObject AS = GameObject.Find("AmbientSound");
        if (AS) ambientSound = AS.GetComponent<AudioSource>();
        //ambientSound = GameObject.Find("AmbientSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (increaseAmbient)
        {
            ambientSound.volume += Time.deltaTime * 0.04f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerTrigger" && !exited)
        {
            exited = true;
            increaseAmbient = true;
            //playerMesh.material = transMat;
            gm.blackInFive();
        }
    }
}
