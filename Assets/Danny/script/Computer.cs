using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [Range(5f,255f)]
    public float LightIntensity = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TrunOnScreen() 
    {
        this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(LightIntensity, LightIntensity, LightIntensity, 0));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M)) 
        {
            TrunOnScreen();
        }
        
    }

    private void OnDestroy()
    {
        this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));

    }
}
