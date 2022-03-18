using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public CanvasGroup bedUI;

    public static BedScript instance;
    private void Awake()
    {
        instance = this;
    }
    public void showBedUI() 
    {
        bedUI.alpha = 1;
    }
    public void closeBedUI()
    {
        bedUI.alpha = 0;
    }
    public void sleepToMorning() 
    {
        //full heal
    }
    public void sleepToMidnight()
    {
        //no heal but night fight
    }
    public void sleepToMorningHalf()
    {
        //quater heal and only after night fights
    }
}
