using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuControl : MonoBehaviour
{

    public Texture2D[] chessTextures;
    public Material chessBoradMat;
    
    public Texture2D[] rulebookTextures;
    public Material rulebookMat;
    public Animator rulebookAnimator;
    private int n = 0;

    public menuCamZoom menuCam;
  


    public GameObject vcam1;
    // Start is called before the first frame update
    void Start()
    {
        menuNormal();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            RulebookChange();
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
        menuCam.Zoom();
    }

    public void quitHover()
    {
        changeTex(3);
    }

    public void quitClick()
    {
        changeTex(4);
        Application.Quit();
    }
    public void creditHover()
    {
        changeTex(5);
    }

    public void creditClick()
    {
        changeTex(6);
    }
    #endregion


    public void RuleHover() 
    {
        if (rulebookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.05f)
        {
            rulebookAnimator.SetFloat("multiplier", 1f);
        }
        else 
        {
            rulebookAnimator.SetFloat("multiplier", 1f);
            Debug.Log("play new");
            rulebookAnimator.Play("float", 0, 0f);
        }
    }

    public void RuleHoverReset() 
    {
        if (rulebookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime<0.95f)
        {
            rulebookAnimator.SetFloat("multiplier", -1f);
        }
        else
        {
            rulebookAnimator.SetFloat("multiplier", -1f);
            Debug.Log("play new");
            rulebookAnimator.Play("float", 0, 1f);
        }
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
