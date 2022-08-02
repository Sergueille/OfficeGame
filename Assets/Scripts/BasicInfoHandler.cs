using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasicInfoHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fullName;
    [SerializeField] private TextMeshProUGUI firstName;
    [SerializeField] private TextMeshProUGUI lastName;
    [SerializeField] private TextMeshProUGUI birth;
    [SerializeField] private TextMeshProUGUI customerID;

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

    private void Refresh()
    {
        if (fullName != null)
            fullName.text = Human.FullName;
        if (firstName != null)
            firstName.text = Human.firstName;
        if (lastName != null)
            lastName.text = Human.name;

        if (birth != null)
        {
            int randomFormat = Random.Range(0, (int)Human.DateFormat.lastValue);
            birth.text = Human.GetFormattedBirth((Human.DateFormat)randomFormat);
        }

        if (customerID != null)
            customerID.text = Human.customerID;
    }
}
