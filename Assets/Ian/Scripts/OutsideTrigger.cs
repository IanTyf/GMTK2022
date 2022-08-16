using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideTrigger : MonoBehaviour
{
    public EffectsController ec;
    public GameObject endPoint;
    public Animator goodEndingAnim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerDist = ec.gameObject.transform.position.z - transform.position.z;
        float totalDist = endPoint.transform.position.z - transform.position.z;
        if (playerDist / totalDist > 0.7f)
        {
            goodEndingAnim.SetTrigger("goodEnding");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ec.reachOutside();
        }
    }
}
