using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public roll playerRoll;
    public GlitchEffect glitchEffect;

    public GameObject enemy;
    public GameObject emptyFootStep;

    public EffectsController effectController;

    public GameObject overlay;
    public AudioSource heartBeat;

    private bool glitch;
    private float glitchVal;
    private GameObject efs;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (glitch)
        {
            glitchVal += Time.deltaTime * 0.15f;
            glitchEffect.glitchStrength = glitchVal;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dieInstantly();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            DieSlowly();
        }
    }

    // biss
    public void dieInstantly()
    {
        // disable WASD
        playerRoll.canMove = false;

        // disable enemy and effect controller
        effectController.enabled = false;

        enemy.SetActive(false);
        

        // start increase glitch and footstep gets closer
        glitch = true;
        glitchVal = 0f; 
        efs = Instantiate(emptyFootStep, enemy.transform.position, Quaternion.identity);
        efs.GetComponent<EmptyFootStep>().playerTransform = playerRoll.gameObject.transform;
        Destroy(efs, 5.5f);
        // after two seconds, die.
        Invoke("blackScreenAndReload", 7f);
    }


    // slowly die
    public void DieSlowly()
    {
        // disable WASD
        playerRoll.canMove = false;

        effectController.enabled = false;

        enemy.SetActive(false);

        blackScreenAndReload();
    }


    public void blackScreenAndReload()
    {
        //glitchEffect.glitchStrength = 0f;
        heartBeat.volume = 0f;
        overlay.GetComponent<Animator>().SetTrigger("fadeToBlack");

        Invoke("ReloadScene", 4f);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
