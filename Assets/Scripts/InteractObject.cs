using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    [SerializeField]
    protected Collider clickCollider;
    public float interactMaxDist = 1;

    [NonSerialized] public string leftActionName = "";
    [NonSerialized] public Action leftAction;
    [NonSerialized] public string rightActionName = "";
    [NonSerialized] public Action rightAction;
}
