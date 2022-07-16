using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour{
    public List<string> tags=new List<string>();
    void Start() {
        gameObject.AddTag(tags.ToArray());
        //gameObject.HasTag("Player");
    }
    void Update(){
    }

    public void AddTag(string tag)
    {
        gameObject.AddTag(tag);
    }
    public void RemoveTag(string tag)
    {
        gameObject.RemoveTag(tag);
    }
    public bool HasTag(string tag)
    {
        return gameObject.HasTag(tag);
    }
}

