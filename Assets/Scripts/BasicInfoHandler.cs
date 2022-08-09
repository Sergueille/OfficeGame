using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasicInfoHandler : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI fullName;
    [SerializeField] protected TextMeshProUGUI firstName;
    [SerializeField] protected TextMeshProUGUI lastName;
    [SerializeField] protected TextMeshProUGUI birth;
    [SerializeField] protected TextMeshProUGUI customerID;

    private Human _human;
    public Human Human
    {
        get => _human;
        set 
        { 
            _human = value;
            Refresh();
        }
    }

    public virtual void Refresh()
    {
        if (fullName != null)
            fullName.text = Human.FullName;
        if (firstName != null)
            firstName.text = Human.firstName;
        if (lastName != null)
            lastName.text = Human.name;

        if (birth != null)
        {
            int randomFormat = Random.Range(0, (int)Human.DateFormat.lastFullFormatValue + 1);
            birth.text = Human.GetFormattedBirth((Human.DateFormat)randomFormat);
        }

        if (customerID != null)
            customerID.text = Human.customerID;
    }
}
