using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public bool freeze;
    public static Freeze instance;
    private void Awake()
    {
        instance = this;
    }

    public bool getFreeze() 
    {
        return freeze;
    }
    public void setFreeze(bool x) 
    {
        freeze = x;
    }

}
