using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MenuControl : MonoBehaviour
{

    public Texture2D[] chessTextures;
    public Material chessBoradMat;
    
    public Texture2D[] rulebookTextures;
    public Material rulebookMat;
    public Animator ruleBookAnimator;
    private int n = 0;


    public GameObject vcam1;
    // Start is called before the first frame update
    void Start()
    {
        menuNormal();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.H)) 
        {
            playClick();
        }
    }

    public void changeTex(int targetTexIndex) 
    {
        chessBoradMat.SetTexture("_Texture2D", chessTextures[targetTexIndex]);
    }

    //chessborad texture control
    #region
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
    #endregion


    public void RuleHover() 
    {
        ruleBookAnimator.SetTrigger("ruleHover");
    }

    public void RuleHoverReset() 
    {
        ruleBookAnimator.ResetTrigger("ruleHover");
    }


    private void OnDestroy()
    {
        menuNormal();
    }

    public void GameBegin() 
    {
        vcam1.SetActive(false);
    }

    public void RulebookChange() 
    {
        if (n == 0)
        {
            n = 1;
        }
        else 
        {
            n = 0;
        }
        rulebookMat.SetTexture("_Texture2D", rulebookTextures[n]);
    }
}
