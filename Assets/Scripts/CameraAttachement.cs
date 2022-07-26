using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttachement : InteractObject
{
    public string actionNameOverride = "Voir";
    public Transform cameraTransform;

    public Drawer linkedDrawer;

    [NonSerialized] public bool attached;

    private void Start()
    {
        leftActionName = actionNameOverride;
        leftAction = Attach;
    }

    private void Update()
    {
        if (PlayerController.instance.attachPoint == null)
        {
            clickCollider.enabled = true;
            attached = false;
        }
        else
        {
            attached = PlayerController.instance.attachPoint.position == cameraTransform.position;
            clickCollider.enabled = !attached;
        }

        if (!attached && !PlayerController.instance.isReadingSheet && linkedDrawer != null)
            linkedDrawer.Close();

        if (attached && linkedDrawer != null)
            linkedDrawer.Open();
    }

    public void Attach()
    {
        PlayerController.instance.AttachTo(cameraTransform);
    }
}
