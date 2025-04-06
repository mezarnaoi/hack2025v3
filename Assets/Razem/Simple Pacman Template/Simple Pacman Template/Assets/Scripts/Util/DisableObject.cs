using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public void Start()
    {
        Destroy(this.gameObject, 1f);
    }
}
