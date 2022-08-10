using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnemy : MonoBehaviour
{
    public int pos;

    public GlitchEffect glitchEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Teleport()
    {
        //if (Random.Range(0f, 1f) < 0.3f)
        {
            glitchEffect.glitchForSeconds(0.8f, 0.08f);
            disableAll();
            pos = pos < transform.childCount - 1 ? pos + 1 : 0;
            transform.GetChild(pos).gameObject.SetActive(true);
        }
    }

    public void Disappear()
    {
        glitchEffect.glitchForSeconds(0.8f, 0.08f);
        disableAll();
    }

    private void disableAll()
    {
        for (int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
