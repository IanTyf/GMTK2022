using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFootStep : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translation = (playerTransform.position - transform.position).normalized;
        transform.Translate(translation * Time.deltaTime * speed);
    }
}
