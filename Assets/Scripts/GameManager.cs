using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int minBirthYear;
    [SerializeField] private int maxBirthYear;
    [SerializeField] private int childrensMinBirthYear;
    [SerializeField] private int childrensMaxBirthYear;
    [System.NonSerialized] public RangeInt birthYearRange;
    [System.NonSerialized] public RangeInt childrensYearRange;
    public int humansCount;
    public int customerIDLength = 10;
    public int maxSheetsPerSlot;
    public int formCount;
    public int maxChildCount = 6;
    public int nbRequest = 3;
    [SerializeField] private int requestDocMinCount = 3;
    [SerializeField] private int requestDocMaxCount = 4;
    [System.NonSerialized] public RangeInt requestDocCount;

    public List<Slot> humanInfoSlots;
    public List<TextMeshPro> humanInfoLabel;

    public List<Slot> formsSlots;

    public GameObject documentPrefab;
    public List<GameObject> basicInfoPrefabs;
    public List<GameObject> familyInfoPrefabs;
    public List<GameObject> basicFormPrefabs;
    public List<GameObject> familyFormPrefabs;
    public Slot testSlot;

    public List<Human> humans;
    public List<Request> requests;

    private void Awake()
    {
        instance = this;
        humans = new List<Human>();

        birthYearRange = new(minBirthYear, maxBirthYear - minBirthYear);
        childrensYearRange = new(childrensMinBirthYear, childrensMaxBirthYear - childrensMinBirthYear);
        requestDocCount = new(requestDocMinCount, requestDocMaxCount - requestDocMinCount);
    }

    private void Start()
    {
        int slotID = 0;
        char slotStartLetter = 'A';

        // Create human database
        for (int i = 0; i < humansCount; i++)
        {
            Human newHuman = new();
            humans.Add(newHuman);
        }

        // Sort by name
        humans.Sort((h1, h2) => h1.name.CompareTo(h2.name));
        
        // For each human
        for (int i = 0; i < humansCount; i++)
        {
            // If slot is full, take next and update shelf labels
            if (humanInfoSlots[slotID].sheets.Count > maxSheetsPerSlot)
            {
                char endLetter = humans[i - 1].name[0];

                if (slotStartLetter == endLetter)
                    humanInfoLabel[slotID].text = slotStartLetter.ToString();
                else
                    humanInfoLabel[slotID].text = $"{slotStartLetter} - {endLetter}";

                slotStartLetter = humans[i].name[0];
                slotID++;
            }

            // Create a sheet
            Sheet sheet = CreateSheet();
            sheet.Title = "Données du client";
            sheet.AddUI(basicInfoPrefabs, humans[i]);
            sheet.AddUI(familyInfoPrefabs, humans[i]);
            humanInfoSlots[slotID].PutSheetImmediate(sheet);
        }
            
        humanInfoLabel[slotID].text = $"{slotStartLetter} - Z";

        string[] formsID = new string[formCount];

        // Instantiate forms
        slotID = 0;
        for (int i = 0; i < formCount; i++)
        {
            formsID[i] = $"{(char)Random.Range('A', 'Z' + 1)}-{Random.Range(10, 100)}";

            if (formsSlots[slotID].sheets.Count > maxSheetsPerSlot)
                slotID++;

            Sheet sheet = CreateSheet();
            sheet.Title = $"Formulaire {formsID[i]}";
            sheet.AddUI(basicFormPrefabs);
            formsSlots[slotID].PutSheetImmediate(sheet);
        }

        for (int i = 0; i < nbRequest; i++)
        {
            Request req = new();
            List<int> indexes = new();

            int docCount = Random.Range(requestDocCount.start, requestDocCount.end + 1);
            for (int j = 0; j < docCount; j++)
            {
                int randomID = -1;

                do
                {
                    randomID = Random.Range(0, formCount);

                } while (indexes.Contains(randomID)); // Prevent asking the same form twice

                req.formIDs.Add(formsID[randomID]);
                indexes.Add(randomID);
            }

            requests.Add(req);
        }

        ScreenManager.instance.UpdateRequests();
    }

    public Sheet CreateSheet()
    {
        return Instantiate(documentPrefab).GetComponent<Sheet>();
    }
}

public class Request
{
    public Human targetHuman;
    public List<string> formIDs;
}
