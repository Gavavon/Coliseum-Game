using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerAnimation : MonoBehaviour
{
    public GameObject daggerOriginal;
    public Transform daggerReturnPos;

    public static DaggerAnimation instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        daggerOriginal.transform.position = daggerReturnPos.position;
    }

    public void ReturnDagger() 
    {
        daggerOriginal.transform.position = daggerReturnPos.position;
    }
}
