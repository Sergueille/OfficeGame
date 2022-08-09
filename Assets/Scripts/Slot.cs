using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : InteractObject
{
    [SerializeField] Transform cameraAttachement;
    [SerializeField] Transform sheetAttachement;
    [SerializeField] Transform sheetIntermediateAttachement;

    public List<Sheet> sheets;
    public float movementDuration = 0.3f;
    public float maxRandomOrientation = 10;
    public float sheetSpace = 0.003f;

    private void Update()
    {
        leftAction = null;
        rightAction = null;

        if (sheets.Count == 0)
        {
            if (PlayerController.instance.sheets.Count > 0)
            {
                leftActionName = "Poser la feuille";
                leftAction = PutSheet;
            }
        }
        else
        {
            if (PlayerController.instance.sheets.Count > 0)
            {
                leftActionName = "Poser la feuille";
                leftAction = PutSheet;
            }
            else
            {
                leftActionName = "Lire la feuille";
                leftAction = ReadSheet;
            }

            rightActionName = "Prendre la feuille";
            rightAction = () => { TakeSheet(); };
        }
    }

    public void PutSheet()
    {
        Sheet sheet = PlayerController.instance.sheets[^1];


        sheets.Add(sheet);
        PlayerController.instance.sheets.RemoveAt(PlayerController.instance.sheets.Count - 1);

        float randomOri = Random.Range(-maxRandomOrientation, maxRandomOrientation);
        float finalY = sheetSpace * (sheets.Count - 1);

        sheet.transform.parent = sheetIntermediateAttachement;
        LeanTween.moveLocal(sheet.gameObject, Vector3.zero, movementDuration / 2).setEaseInOutQuad();
        LeanTween.rotateLocal(sheet.gameObject, Vector3.zero, movementDuration / 2).setEaseInOutQuad().setOnComplete(() =>
        {
            sheet.IsOverlay = false;
            sheet.transform.parent = sheetAttachement;
            LeanTween.moveLocal(sheet.gameObject, new Vector3(0, finalY, 0), movementDuration / 2).setEaseInOutQuad();
            LeanTween.rotateLocal(sheet.gameObject, new Vector3(0, randomOri, 0), movementDuration / 2).setEaseInOutQuad().setOnComplete(() =>
            {
                if (sheets.Count > 1)
                    sheets[^2].showCanvas = false;
            });
        });
    }

    public void PutSheetImmediate(Sheet sheet)
    {
        if (sheets.Count > 0)
            sheets[^1].showCanvas = false;

        sheets.Add(sheet);
        sheet.IsOverlay = false;
        sheet.transform.parent = sheetAttachement;

        float randomOri = Random.Range(-maxRandomOrientation, maxRandomOrientation);
        float finalY = sheetSpace * (sheets.Count - 1);
        sheet.gameObject.transform.localPosition = new Vector3(0, finalY, 0);
        sheet.gameObject.transform.localRotation = Quaternion.Euler(0, randomOri, 0);
    }

    public void TakeSheet()
    {
        if (sheets.Count > 0)
        {
            Sheet sheet = sheets[^1];

            PlayerController.instance.sheets.Add(sheet);
            sheets.RemoveAt(sheets.Count - 1);

            sheet.transform.parent = sheetIntermediateAttachement;
            LeanTween.moveLocal(sheet.gameObject, Vector3.zero, movementDuration / 2).setEaseInOutQuad();
            LeanTween.rotateLocal(sheet.gameObject, Vector3.zero, movementDuration / 2).setEaseInOutQuad().setOnComplete(() =>
            {
                sheet.IsOverlay = true;
                sheet.transform.parent = PlayerController.instance.heldItem;
                LeanTween.moveLocal(sheet.gameObject, Vector3.zero, movementDuration / 2).setEaseInOutQuad();
                LeanTween.rotateLocal(sheet.gameObject, Vector3.zero, movementDuration / 2).setEaseInOutQuad();
            });

            if (sheets.Count > 0)
                sheets[^1].showCanvas = true;
        }
    }

    public void ReadSheet()
    {
        sheets[^1].CanFocus = true;
        PlayerController.instance.previousAttach = PlayerController.instance.attachPoint;
        PlayerController.instance.isReadingSheet = true;
        PlayerController.instance.AttachTo(cameraAttachement);
    }
}
