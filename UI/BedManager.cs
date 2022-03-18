using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BedManager : MonoBehaviour
{
	public GameObject StartNightOptions;
	public GameObject MidNightOptions;
	public static BedManager instance;
	private void Awake()
	{
		instance = this;
	}
	public void updateBedUI() 
	{
		switch (TimeManager.instance.currentTime) 
		{
			case TimeManager.TimeCycle.NightStart:
				MidNightOptions.SetActive(false);
				StartNightOptions.SetActive(true);
				break;
			case TimeManager.TimeCycle.MidNight:
				StartNightOptions.SetActive(false);
				MidNightOptions.SetActive(true);
				break;
		}
	}
}
