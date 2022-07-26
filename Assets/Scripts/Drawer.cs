using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public Vector3 openDeltaPos;
    public float transitionDuration = 0.3f;
    public bool open;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = gameObject.transform.localPosition;

        if (open) 
            gameObject.transform.position = startPosition + openDeltaPos;
    }

    public void Open()
    {
        if (open) return;
        open = true;
        LeanTween.moveLocal(gameObject, startPosition + openDeltaPos, transitionDuration).setEaseInOutQuad();
    }

    public void Close()
    {
        if (!open) return;
        open = false;
        LeanTween.moveLocal(gameObject, startPosition, transitionDuration).setEaseInOutQuad();
    }

    public void Toggle()
    {
        if (open) Close();
        else Open();
    }
}
