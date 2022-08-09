using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    [SerializeField] private AccessProxy reqTemplate;

    const string indentTag = "<indent=1rem>";

    private void Awake()
    {
        instance = this;
    }

    public void UpdateRequests()
    {
        foreach (Request req in GameManager.instance.requests)
        {

        }
    }
}
