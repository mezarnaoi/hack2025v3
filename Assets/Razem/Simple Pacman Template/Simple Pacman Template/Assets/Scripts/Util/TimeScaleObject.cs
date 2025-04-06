using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleObject : MonoBehaviour
{
    private void OnDisable()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f;
    }
}
