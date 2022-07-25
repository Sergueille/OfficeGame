using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttachement : InteractObject
{
    public string actionNameOverride = "Voir";
    public Transform cameraTransform;

    private void Start()
    {
        leftActionName = actionNameOverride;
        leftAction = Attach;
    }

    private void Update()
    {
        if (PlayerController.instance.attachPoint == null)
            clickCollider.enabled = true;
        else
            clickCollider.enabled = PlayerController.instance.attachPoint.position != cameraTransform.position;
    }

    public void Attach()
    {
        PlayerController.instance.AttachTo(cameraTransform);
    }
}
