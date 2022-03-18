using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
	public static DeathManager instance;
	private void Awake()
	{
		instance = this;
	}
	private void Update()
	{
		//if player died reset coliseum
		ColiseumManager.instance.resetColiseums();
	}
	
}
