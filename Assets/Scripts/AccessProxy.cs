using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessProxy : MonoBehaviour
{
    [System.NonSerialized] public Dictionary<string, GameObject> dict;
    [HideInInspector] public List<string> names;     
    [HideInInspector] public List<GameObject> objects;

    private void Awake()
    {
        dict = new();
        for(int i = 0; i < names.Count; i++)
        {
            dict.Add(names[i], objects[i]);
        }
    }

    public GameObject Get(string name)
    {
        return dict[name];
    }
}
