using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiseumManager : MonoBehaviour
{
    public static ColiseumManager instance;
    private void Awake()
    {
        instance = this;
    }
    public enum coliseums
    { 
        coliseum1,
        coliseum2,
        coliseum3,
        coliseum4,
        coliseum5,
        coliseum6
    }
    public static coliseums activeColiseum;
	private void Start()
	{
        activeColiseum = coliseums.coliseum3;

    }
    public void resetColiseums()
    {
        switch (activeColiseum)
        {
            case coliseums.coliseum3:

                break;
        }
    }
}
