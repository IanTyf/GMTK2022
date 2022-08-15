using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalenderSwap : MonoBehaviour
{
    public Texture2D[] textures;

    // Start is called before the first frame update
    void Start()
    {
        int ind = StaticManager.deathCount;
        if (ind >= textures.Length) ind = textures.Length - 1;
        GetComponent<MeshRenderer>().material.mainTexture = textures[ind];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
