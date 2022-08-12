using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum CheckPoint
{
    Desk, Counter, Bed, Floor
}

public class GameManager : MonoBehaviour
{
    public roll playerRoll;
    public GlitchEffect glitchEffect;

    public GameObject enemy;
    public GameObject emptyFootStep;

    public EffectsController effectController;

    public GameObject overlay;
    public GameObject endText;
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

    public GameObject checkPoints;

    public bool GameStarted;

    public string[] endTextContent;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // player dice spawn
        switch (StaticManager.curCheckPoint)
        {
            case CheckPoint.Desk:
                playerRoll.gameObject.transform.position = checkPoints.transform.GetChild(0).position;
                playerRoll.gameObject.transform.rotation = checkPoints.transform.GetChild(0).rotation;
                break;
            case CheckPoint.Counter:
                playerRoll.gameObject.transform.position = checkPoints.transform.GetChild(1).position;
                playerRoll.gameObject.transform.rotation = checkPoints.transform.GetChild(1).rotation;
                break;
            case CheckPoint.Bed:
                playerRoll.gameObject.transform.position = checkPoints.transform.GetChild(2).position;
                playerRoll.gameObject.transform.rotation = checkPoints.transform.GetChild(2).rotation;
                break;
            case CheckPoint.Floor:
                playerRoll.gameObject.transform.position = checkPoints.transform.GetChild(3).position;
                playerRoll.gameObject.transform.rotation = checkPoints.transform.GetChild(3).rotation;
                break;
        }


        // broken dice spawn
        /*
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
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject AS = GameObject.Find("AmbientSound");
        if (AS) ambient = AS.GetComponent<AudioSource>();
        //ambient = GameObject.Find("AmbientSound").GetComponent<AudioSource>();
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


    public void NewDeath()
    {
        Invoke("blackScreenAndReload", 3f);
    }


    public void blackScreenAndReload()
    {
        //glitchEffect.glitchStrength = 0f;
        heartBeat.volume = 0f;
        effectController.enabled = false;
        overlay.GetComponent<Animator>().SetTrigger("fadeToBlack");
        Invoke("showRandomEndText", 1.7f);

        Invoke("ReloadScene", 7.1f);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        /*
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
        */
    }

    private void showRandomEndText()
    {
        endText.GetComponent<TMP_Text>().text = endTextContent[Random.Range(0, endTextContent.Length)];
        endText.GetComponent<Animator>().SetTrigger("showText");
    }

    public void blackInFive()
    {
        Invoke("blackScreenAndGoToMainMenu", 5f);
    }

    public void blackScreenAndGoToMainMenu()
    {
        er.increaseAmbient = false;
        if (ambient) ambient.volume = 0f;
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
