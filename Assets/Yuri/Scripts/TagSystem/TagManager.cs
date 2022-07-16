using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TagManager : MonoBehaviour
{
    public static TagManager Instance { get; private set; }
    private void Awake() {
        if (Instance == null) {
            Instance = (TagManager) this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    public List<string> tagsList = new List<string>();
    [ShowInInspector]
    public Dictionary<string, List<GameObject>> tagsDictionary = new Dictionary<string, List<GameObject>>();
    
    private void InitTags() {
        int length = tagsList.Count;
        tagsDictionary = new Dictionary<string, List<GameObject>>(length);
        for (int i = 0; i < length; i++) {
            tagsDictionary.Add(tagsList[i], new List<GameObject>());
        }
    }
    public List<GameObject> FindObjsWithTag(string tag) {
        if (tagsDictionary.ContainsKey(tag)) {
            return tagsDictionary[tag];
        }
        Debug.Log("不存在标签为" + tag + "的物体");
        return null;
    }

}
