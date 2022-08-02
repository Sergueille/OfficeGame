using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float otherGenderProbability = 0.01f;
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

    public List<Slot> humanInfoSlots;
    public List<TextMeshPro> humanInfoLabel;

    public List<Slot> formsSlots;

    public GameObject documentPrefab;
    public List<GameObject> basicInfoPrefabs;
    public Slot testSlot;

    public List<Human> humans;

    private void Awake()
    {
        instance = this;
        humans = new List<Human>();

        birthYearRange = new(minBirthYear, maxBirthYear - minBirthYear);
        childrensYearRange = new(childrensMinBirthYear, childrensMaxBirthYear - childrensMinBirthYear);
    }

    private void Start()
    {
        int slotID = 0;
        char slotStartLetter = 'A';

        for (int i = 0; i < humansCount; i++)
        {
            Human newHuman = new();
            humans.Add(newHuman);
        }

        humans.Sort((h1, h2) => h1.name.CompareTo(h2.name));

        for (int i = 0; i < humansCount; i++)
        {
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

            Sheet sheet = CreateSheet();
            sheet.Title = "Données du client";
            sheet.AddBasicInfo(humans[i]);
            humanInfoSlots[slotID].PutSheetImmediate(sheet);
        }
            
        humanInfoLabel[slotID].text = $"{slotStartLetter} - Z";


        // Instantiate forms
        slotID = 0;
        for (int i = 0; i < formCount; i++)
        {
            string formName = $"{(char)Random.Range('A', 'Z' + 1)}-{Random.Range(10, 100)}";

            if (formsSlots[slotID].sheets.Count > maxSheetsPerSlot)
                slotID++;

            Sheet sheet = CreateSheet();
            sheet.Title = $"Formulaire {formName}";
            formsSlots[slotID].PutSheetImmediate(sheet);
        }
    }

    public Sheet CreateSheet()
    {
        return Instantiate(documentPrefab).GetComponent<Sheet>();
    }
}
