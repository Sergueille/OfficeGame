using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class MonoInput : MonoBehaviour
{
    private TMP_InputField inputField;
    private const string tagString = "<mspace=1rem>";

    private void Start()
    {
        inputField = gameObject.GetComponent<TMP_InputField>();
        inputField.text = tagString;
    }
}
