using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody playerBody;
    public Transform playerParent;
    public Transform heldItem;

    public Image knob;
    public GameObject leftAction;
    public TextMeshProUGUI leftActionText;
    public GameObject rightAction;
    public TextMeshProUGUI rightActionText;

    public Color knobNormalColor;
    public Color knobHoverColor;

    public float sensitivity = 0.2f;
    public float speed = 2;
    public float speedSmooth = 0.3f;
    public float attachementDuration = 0.5f;

    [NonSerialized] public bool isAttached = false;
    [NonSerialized] public Transform attachPoint = null;
    [NonSerialized] public bool isReadingSheet = false;
    [NonSerialized] public Transform previousAttach = null;

    private Vector2 smoothSpeed;
    private Vector2 smoothSpeedSpeed;
    private bool exitingAttach = false;

    [NonSerialized] public Camera mainCam;

    [NonSerialized] public List<Sheet> sheets;

    private void Awake()
    {
        instance = this;
        mainCam = gameObject.GetComponent<Camera>();
        sheets = new List<Sheet>();
    }

    private void Update()
    {
        // Mouse
        if (!isAttached)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector2 delta = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            delta *= sensitivity;// * Time.deltaTime;

            float rotateY = playerBody.transform.rotation.eulerAngles.y + delta.x;
            playerBody.MoveRotation(Quaternion.Euler(0, rotateY, 0));

            float rotateX = gameObject.transform.localRotation.eulerAngles.x - delta.y;
            if (rotateX > 180) rotateX -= 360;
            if (rotateX > 90) rotateX = 90;
            if (rotateX < -90) rotateX = -90;

            gameObject.transform.localRotation = Quaternion.Euler(rotateX, 0, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        // Keyboard
        Vector2 targetMovement = Vector2.zero;
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
            targetMovement.y += speed;
        if (Input.GetKey(KeyCode.S))
            targetMovement.y -= speed;
        if (Input.GetKey(KeyCode.D))
            targetMovement.x += speed;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
            targetMovement.x -= speed;

        if (!isAttached)
        {
            smoothSpeed = Vector2.SmoothDamp(smoothSpeed, targetMovement, ref smoothSpeedSpeed, speedSmooth);

            Vector3 newPos = playerBody.position 
                + (smoothSpeed.y * Time.deltaTime * playerBody.transform.forward) 
                + (smoothSpeed.x * Time.deltaTime * playerBody.transform.right);
            playerBody.MovePosition(newPos);
        }
        else if (!isReadingSheet)
        {
            // Trying to move -> exit attachement
            if (targetMovement != Vector2.zero && !exitingAttach)
            {
                exitingAttach = true;
                Cursor.lockState = CursorLockMode.Locked;

                Vector3 newRotation = Quaternion.LookRotation(attachPoint.position - transform.parent.position).eulerAngles;

                LeanTween.moveLocal(gameObject, Vector3.zero, attachementDuration).setEaseInOutQuad();
                LeanTween.rotate(playerBody.gameObject, new Vector3(0, newRotation.y, 0), attachementDuration).setEaseInOutQuad();
                LeanTween.rotateLocal(gameObject, new Vector3(newRotation.x, 0, 0), attachementDuration).setEaseInOutQuad()
                .setOnComplete(() =>
                {
                    isAttached = false;
                    attachPoint = null;
                    exitingAttach = false;
                });
            }
        }


        // Interaction
        if (!isReadingSheet)
        {
            LayerMask mask = LayerMask.GetMask("Interact");
            InteractObject obj = null;

            Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hit, mask))
            {
                obj = hit.collider.gameObject.GetComponent<InteractObject>();
            }

            if (obj != null && hit.distance < obj.interactMaxDist)
            {
                if (obj.leftAction != null && obj.leftActionName != "")
                {
                    leftAction.SetActive(true);
                    leftActionText.text = obj.leftActionName;

                    if (Input.GetMouseButtonUp(0))
                        obj.leftAction();
                }
                else
                {
                    leftAction.SetActive(false);
                }

                if (obj.rightAction != null && obj.rightActionName != "")
                {
                    rightAction.SetActive(true);
                    rightActionText.text = obj.rightActionName;

                    if (Input.GetMouseButtonUp(1))
                        obj.rightAction();
                }
                else
                {
                    rightAction.SetActive(false);
                }

                knob.color = knobHoverColor;
                LayoutRebuilder.ForceRebuildLayoutImmediate(leftAction.transform.parent.GetComponent<RectTransform>());
            }
            else
            {
                knob.color = knobNormalColor;
                leftAction.SetActive(false);
                rightAction.SetActive(false);
            }

            knob.enabled = !isAttached;
        }
        else
        {
            knob.enabled = false;
            leftAction.SetActive(false);
            rightAction.SetActive(false);
        }
    }

    public void AttachTo(Transform newTranform)
    {
        attachPoint = transform;
        isAttached = true;

        LeanTween.move(gameObject, newTranform.position, attachementDuration).setEaseInOutQuad();
        LeanTween.rotate(gameObject, newTranform.rotation.eulerAngles, attachementDuration).setEaseInOutQuad();
    }
}
