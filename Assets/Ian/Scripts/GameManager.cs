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

    public GameObject[] brokenDice;
    public GameObject normalDice;

    public GameObject[] spawnPoints;
    public GameObject[] brokenSpawnPoints;

    public AudioSource ambient;

    public ExitRoom er;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        for (int i=0; i<spawnPoints.Length; i++)
        {
            if (i<StaticManager.deathCount)
            {
                Instantiate(brokenDice[i], brokenSpawnPoints[i].transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(normalDice, spawnPoints[i].transform.position, Quaternion.identity);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ambient = GameObject.Find("AmbientSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (glitch)
        {
            glitchVal += Time.deltaTime * 0.15f;
            glitchEffect.glitchStrength = glitchVal;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dieInstantly();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            DieSlowly();
        }
        */
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
        if (StaticManager.deathCount < 5)
        {
            StaticManager.deathCount++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // all dead, no more lives

            StaticManager.deathCount = 0;
            // go to fourth scene
            SceneManager.LoadScene("Scene_Lost");
        }
    }

    public void blackInFive()
    {
        Invoke("blackScreenAndGoToMainMenu", 5f);
    }

    public void blackScreenAndGoToMainMenu()
    {
        er.increaseAmbient = false;
        ambient.volume = 0f;
        glitchEffect.glitchStrength = 0f;
        heartBeat.volume = 0f;
        overlay.GetComponent<Animator>().SetTrigger("black");
        //overlay.GetComponent<Image>().color = Color.black;

        Invoke("goToMainScene", 5f);
    }

    private void goToMainScene()
    {
        SceneManager.LoadScene("Scene_Start");
    }
}
