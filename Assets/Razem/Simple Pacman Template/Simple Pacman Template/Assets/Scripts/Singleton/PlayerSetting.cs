using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    public static PlayerSetting instance;
    public Player player { get; set; }
    public Item item { get; set; }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
}
