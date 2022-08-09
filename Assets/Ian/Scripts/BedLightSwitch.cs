using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedLightSwitch : MonoBehaviour
{
    public GameObject player;

    public Material[] mats;
    public Material defaultMat;

    public GameObject pointLight1;
    public GameObject pointLight2;
    public GameObject pointLight3;

    public int currentInd;

    // Start is called before the first frame update
    void Start()
    {
        currentInd = 1;
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
            if (dist < minDist && dist < 1f)
            {
                minDist = dist;
                ind = i;
            }
        }

        if (ind != -1)
        {
            // add 7 if possible
            ind = ind + 1 >= transform.childCount ? ind : ind + 1;
            ind = ind + 1 >= transform.childCount ? ind : ind + 1;
            ind = ind + 1 >= transform.childCount ? ind : ind + 1;
            ind = ind + 1 >= transform.childCount ? ind : ind + 1;
            ind = ind + 1 >= transform.childCount ? ind : ind + 1;
            ind = ind + 1 >= transform.childCount ? ind : ind + 1;
            ind = ind + 1 >= transform.childCount ? ind : ind + 1;

            if (currentInd != -1)
            {
                transform.GetChild(currentInd).gameObject.GetComponent<MeshRenderer>().material = defaultMat;
                if (currentInd > 0) transform.GetChild(currentInd - 1).gameObject.GetComponent<MeshRenderer>().material = defaultMat;
                if (currentInd < transform.childCount - 1) transform.GetChild(currentInd + 1).gameObject.GetComponent<MeshRenderer>().material = defaultMat;
                //transform.GetChild(currentInd).gameObject.GetComponent<Light>().enabled = false;
            }


            //transform.GetChild(ind).gameObject.GetComponent<Light>().enabled = true;
            int randNum1 = Random.Range(0, mats.Length);
            transform.GetChild(ind).gameObject.GetComponent<MeshRenderer>().material = mats[randNum1];
            pointLight1.transform.SetParent(transform.GetChild(ind), false);
            pointLight1.GetComponent<Light>().color = mats[randNum1].color;

            int randNum2 = Random.Range(0, mats.Length);
            if (ind > 0)
            {
                transform.GetChild(ind - 1).gameObject.GetComponent<MeshRenderer>().material = mats[randNum2];
                pointLight2.transform.SetParent(transform.GetChild(ind - 1), false);
                pointLight2.GetComponent<Light>().enabled = true;
                pointLight2.GetComponent<Light>().color = mats[randNum2].color;
            }
            else
            {
                pointLight2.GetComponent<Light>().enabled = false;
            }

            int randNum3 = Random.Range(0, mats.Length);
            if (ind < transform.childCount - 1)
            {
                transform.GetChild(ind + 1).gameObject.GetComponent<MeshRenderer>().material = mats[randNum3];
                pointLight3.transform.SetParent(transform.GetChild(ind + 1), false);
                pointLight3.GetComponent<Light>().enabled = true;
                pointLight3.GetComponent<Light>().color = mats[randNum3].color;
            }
            else
            {
                pointLight3.GetComponent<Light>().enabled = false;
            }

            currentInd = ind;

        }
    }
}
