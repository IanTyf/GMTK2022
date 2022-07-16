using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {
    //包含所有的 tag 返回true
    public static bool HasTag(this GameObject gameObject, params string[] tag) {
        if (gameObject.TryGetComponent<Tags>(out Tags t)) {
            for (int i = 0; i < tag.Length; i++) {
                if (!t.tags.Contains(tag[i])) {
                    Debug.Log(gameObject.name+"不存在"+tag+"标签");
                    return false;
                }
            }
        }
        return true;
    }
    //给物体增加 tag
    public static void AddTag(this GameObject gameObject, params string[] tags) {
        if (gameObject.TryGetComponent<Tags>(out Tags t)) {
            foreach (var tag in tags) {
                if (!TagManager.Instance.tagsList.Contains(tag)) {
                    Debug.Log("增加一个tag：" + tag);
                    TagManager.Instance.tagsDictionary.Add(tag, new List<GameObject>());
                    Debug.Log(tag + "增加一个物体" + gameObject.name);
                    TagManager.Instance.tagsDictionary[tag].Add(gameObject);
                }
                else {
                    Debug.Log(tag + "增加一个物体" + gameObject.name);
                    TagManager.Instance.tagsDictionary[tag].Add(gameObject);
                }
            }
        }
    }
    //给物体删除tag
    public static void RemoveTag(this GameObject gameObject,params string[] tags) {
        for (int i = 0; i < tags.Length; i++) {
            if (gameObject.HasTag(tags[i])) {
                gameObject.GetComponent<Tags>().tags.Remove(tags[i]);
                Debug.Log(gameObject.name+"移除"+tags+"标签");
                TagManager.Instance.tagsDictionary[tags[i]].Remove(gameObject);
            }
            else {
                Debug.LogWarning(gameObject.name+"不存在"+tags[i]+"标签");
            }
        }
    }
}

