using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperUp : MonoBehaviour
{
    private Animator anim;

    public enum Mode
    {
        up, down
    }

    public Mode direction;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (direction == Mode.up)
            {
                Up();
            }
            else if (direction == Mode.down)
            {
                Down();
            }
        }
    }

    private void Up()
    {
        anim.SetTrigger("Up");
    }

    private void Down()
    {
        anim.SetTrigger("Down");
    }
}
