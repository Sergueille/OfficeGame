using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sheet : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI title;

    public RectTransform UIContainer;

    public bool IsOverlay
    {
        set
        {
            string newLayer = value ? "Overlay" : "Default";
            SetLayerRecusive(gameObject, LayerMask.NameToLayer(newLayer));
        }
    }

    public bool CanFocus
    {
        set
        {
            raycaster.enabled = value;
        }
        get => raycaster.enabled;
    }

    public string Title
    {
        set
        {
            title.text = value;
        }
    }

    private void Awake()
    {
        CanFocus = false;
    }

    private void Start()
    {
        canvas.worldCamera = PlayerController.instance.mainCam;
    }

    private void SetLayerRecusive(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            SetLayerRecusive(child.gameObject, layer);
        }
    }

    public void EndReading()
    {
        CanFocus = false;
        PlayerController.instance.isReadingSheet = false;
        PlayerController.instance.AttachTo(PlayerController.instance.previousAttach);
    }
}
