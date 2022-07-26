using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float otherGenderProbability = 0.01f;
    public RangeInt birthYearRange;
    public int humansCount;

    public GameObject documentPrefab;
    public Slot testSlot;

    public List<Human> humans;

    private void Awake()
    {
        instance = this;
        humans = new List<Human>();
    }

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            Sheet sheet = CreateSheet();
            sheet.Title = "Document " + i.ToString();

            testSlot.PutSheetImmediate(sheet);
        }

        for (int i = 0; i < humansCount; i++)
        {
            Human newHuman = new();
            humans.Add(newHuman);
        }
    }

    public Sheet CreateSheet()
    {
        return Instantiate(documentPrefab).GetComponent<Sheet>();
    }
}
