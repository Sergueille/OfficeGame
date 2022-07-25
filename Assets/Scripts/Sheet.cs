using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet : MonoBehaviour
{
    public bool IsOverlay
    {
        set
        {
            string newLayer = value ? "Overlay" : "Default";
            SetLayerRecusive(gameObject, LayerMask.NameToLayer(newLayer));
        }
    }

    private void SetLayerRecusive(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            SetLayerRecusive(child.gameObject, layer);
        }
    }
}
