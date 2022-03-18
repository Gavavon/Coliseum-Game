using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    
    public enum TimeCycle 
    { 
        DayStart,
        MidDay,
        NightStart,
        MidNight
    }
    public static TimeCycle timeCycle { get; set; }
    public TimeCycle currentTime;
    public enum MealTime
    {
        Breakfeast,
        Dinner
    }
    public static MealTime mealtime;

    public static TimeManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        timeCycle = TimeCycle.DayStart;
        currentTime = timeCycle;
    }


}
