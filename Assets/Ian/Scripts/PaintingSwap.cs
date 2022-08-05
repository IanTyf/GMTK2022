using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSwap : MonoBehaviour
{

    public Texture2D[] allTextures;
    private MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateTexture(int ind)
    {
        mr.material.mainTexture = allTextures[ind];
    }
}
