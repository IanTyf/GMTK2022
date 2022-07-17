using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneGameManager : MonoBehaviour
{
    public GameObject[] brokenDice;
    public float force;
    public Animator anim;

    private bool end;

    // Start is called before the first frame update
    void Start()
    {
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            foreach (GameObject die in brokenDice)
            {
                die.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * force * Random.Range(0.5f, 1.1f),die.transform.position - Vector3.up*0.1f + Vector3.forward * 0.1f *Random.Range(-1f, 1f) + Vector3.right * 0.1f * Random.Range(-1f, 1f), ForceMode.Impulse);
            }
            if (!end)
            {
                end = true;
                Invoke("endGame", 4f);
            }
        }
    }

    private void endGame()
    {
        anim.SetTrigger("fadeToBlackSlow");

        Invoke("loadMainMenu", 4f);
    }

    private void loadMainMenu()
    {
        SceneManager.LoadScene("Scene_Start");
    }
}
