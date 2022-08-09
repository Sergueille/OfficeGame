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

    [Tooltip("The canvas of the sheet will be disabled if the camera is further than this distance")]
    public float maxVisibleDist;

    public bool showCanvas = true;

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

    private void Update()
    {
        Vector3 delta = transform.position - PlayerController.instance.mainCam.transform.position;
        float sqrDist = (delta.x * delta.x) + (delta.y * delta.y);

        if (!showCanvas || sqrDist > maxVisibleDist * maxVisibleDist)
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            canvas.gameObject.SetActive(true);
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

    public void EndReading()
    {
        CanFocus = false;
        PlayerController.instance.isReadingSheet = false;
        PlayerController.instance.AttachTo(PlayerController.instance.previousAttach);
    }

    public GameObject AddUI(GameObject prefab)
    {
        return Instantiate(prefab, UIContainer);
    }

    public GameObject AddUI(List<GameObject> randomList)
    {
        int randomID = Random.Range(0, randomList.Count);
        GameObject prefab = randomList[randomID];
        return AddUI(prefab);
    }

    public GameObject AddUI(List<GameObject> randomList, Human human)
    {
        GameObject go = AddUI(randomList);
        go.GetComponent<BasicInfoHandler>().Human = human;
        return go;
    }
}
