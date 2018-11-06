using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [HideInInspector] public bool primed;
    [HideInInspector] public bool aim;

    public Transform hipPos;
    public Transform aimPos;
}
