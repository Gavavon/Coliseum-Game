using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

	public int maxHealth = 400;
	public int minHealth = 200;
    public int health;

	public float atkSpdSec = 1;

	public int GSR;

	public static EnemyStats instance;
	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		health = UnityEngine.Random.Range(200, 400);
		health = (int)(Math.Round(health / 5.0) * 5);
		GSR = UnityEngine.Random.Range(-75, 75);
	}
}
