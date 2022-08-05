using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuControl : MonoBehaviour
{

    public Texture[] menuTextures;
    public Material chessBoradMat;


    public Animator ruleBookAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeTex(int targetTexIndex) 
    {
        chessBoradMat.SetTexture("mainTexture", menuTextures[targetTexIndex]);
    }

    public void menuNormal() 
    {
        changeTex(0);
    }

    public void playHover() 
    {
        changeTex(1);
    }

    public void playClick() 
    {
        changeTex(2);
    }

    public void quitHover()
    {
        changeTex(3);
    }

    public void quitClick()
    {
        changeTex(4);
    }
    public void creditHover()
    {
        changeTex(6);
    }

    public void creditClick()
    {
        changeTex(7);
    }

}
