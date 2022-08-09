using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FormHandler : MonoBehaviour
{
    [SerializeField] protected TMP_InputField fullName;
    [SerializeField] protected TMP_InputField firstName;
    [SerializeField] protected TMP_InputField lastName;
    [SerializeField] protected TMP_InputField birth;
    [SerializeField] protected Human.DateFormat birthDateFormat;
    [SerializeField] protected TMP_InputField customerID;
    [SerializeField] protected TMP_InputField signIdDate;
    [SerializeField] protected Human.DateFormat signInDateFormat;
    [SerializeField] protected FormHandler spouseHandler;
    [SerializeField] protected FormHandler childTemplate;
    [SerializeField] protected Transform noChildrenDisplay;

    public void CheckWhith(Human human)
    {
        
    }
}
