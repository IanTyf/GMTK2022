using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startgame() 
    {
        //Debug.Log("start");
        anim.SetTrigger("fadeOut");

        Invoke("loadGameScene", 3f);
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene("Scene_Main");
    }

    public void quitgame() 
    {
        //Debug.Log("quit");
        Application.Quit();
    }
}
