using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedLightSwitch : MonoBehaviour
{
    public GameObject player;

    public Material[] mats;
    public Material defaultMat;

    public int currentInd;

    // Start is called before the first frame update
    void Start()
    {
        currentInd = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLight()
    {
        float minDist = 10000f;
        int ind = -1;
        for (int i=0; i<transform.childCount; i++)
        {
            float dist = Vector3.Distance(player.transform.position, transform.GetChild(i).position);
            if (dist < minDist && dist < 5f)
            {
                minDist = dist;
                ind = i;
            }
        }

        if (ind != -1)
        {
            // add 2 if possible
            ind = ind + 1 >= transform.childCount ? ind : ind + 1;
            ind = ind + 1 >= transform.childCount ? ind : ind + 1;

            if (currentInd != -1)
            {
                transform.GetChild(currentInd).gameObject.GetComponent<MeshRenderer>().material = defaultMat;
                transform.GetChild(currentInd).gameObject.GetComponent<Light>().enabled = false;
            }


            transform.GetChild(ind).gameObject.GetComponent<Light>().enabled = true;
            transform.GetChild(ind).gameObject.GetComponent<MeshRenderer>().material = mats[Random.Range(0, mats.Length)];

            currentInd = ind;

        }
    }
}
