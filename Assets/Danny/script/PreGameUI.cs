using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreGameUI : MonoBehaviour
{
    private Animator Animator;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        Animator = this.gameObject.GetComponent<Animator>();
        //AudioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Animator.SetFloat("WarningTime", (Animator.GetFloat("WarningTime") + Time.deltaTime));
    }

    public void SuggestionIn() //on last animation
    {
        Animator.SetBool("SgIn", true);
        AudioSource.Play();
    }
    public void SuggestionOut()//on button
    {
        Animator.SetBool("SgOut", true);
        AudioSource.Stop();
    }

    public void InsIn() 
    {
        Animator.SetBool("Ins", true);
    }

    public void StartSceneLoad() 
    {
        SceneManager.LoadScene("Scene_Start");
    }
}
