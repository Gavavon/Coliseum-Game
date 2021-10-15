using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public bool FREEZE = true;

    public Animator[] animeList;

    public static Freeze instance;
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
