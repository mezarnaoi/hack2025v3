﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    private Camera cam;
    private float width;
    private float height;

    public float widthStretch = 150f;
    public float heightStretch;

    public bool realTime;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        width = Screen.width + widthStretch;
        height = Screen.height + heightStretch;
        cam.aspect = (width / height);
    }

    private void FixedUpdate()
    {
        if (realTime)
        {
            width = Screen.width + widthStretch;
            height = Screen.height + heightStretch;
            cam.aspect = (width / height);
        }
    }
}
