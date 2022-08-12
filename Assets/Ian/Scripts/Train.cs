using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    private int currentInd;

    // Start is called before the first frame update
    void Start()
    {
        currentInd = 0;
        disableAllTrains();
        transform.GetChild(currentInd).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateTrain()
    {
        disableAllTrains();
        currentInd++;
        if (currentInd >= transform.childCount) currentInd = 0;
        transform.GetChild(currentInd).gameObject.SetActive(true);
    }

    private void disableAllTrains()
    {
        for (int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
