using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionMatrix : MonoBehaviour
{

    public float surrenderChance = 20;
    public float healthSurrenderPercent = 10;

    public static DecisionMatrix instance;
    private void Awake()
    {
        instance = this;
    }

    public void combatDecision() 
    {
        int randNum = Random.Range(1, 100);
        int[] temp;
        switch (EnemyBehaviour.instance.lastCombatBehaviour) 
        { 
            case EnemyBehaviour.combatBehave.attack:
                temp = new int[] { 40, 25, 25, 10 };
                break;
            case EnemyBehaviour.combatBehave.randomBlock:
                temp = new int[] { 80, 8, 8, 2 };
                break;
            case EnemyBehaviour.combatBehave.readyBlock:
                temp = new int[] { 30, 40, 5, 25 };
                break;
            case EnemyBehaviour.combatBehave.none:
                temp = new int[] { 60, 15, 15, 10 };
                break;
            default:
                temp = new int[] { 0, 0, 0, 0 };
                Debug.Log("ERROR DecisionMatrix.cs Line 37");
                break;
        }
        if (randNum <= temp[0])
        {
            EnemyBehaviour.instance.currentCombatBehaviour = EnemyBehaviour.combatBehave.attack;
            EnemyBehaviour.instance.lastCombatBehaviour = EnemyBehaviour.combatBehave.attack;
        }
        else
        {
            if (randNum <= temp[1])
            {
                EnemyBehaviour.instance.currentCombatBehaviour = EnemyBehaviour.combatBehave.randomBlock;
                EnemyBehaviour.instance.lastCombatBehaviour = EnemyBehaviour.combatBehave.randomBlock;
            }
            else
            {
                if (randNum <= temp[2])
                {
                    EnemyBehaviour.instance.currentCombatBehaviour = EnemyBehaviour.combatBehave.readyBlock;
                    EnemyBehaviour.instance.lastCombatBehaviour = EnemyBehaviour.combatBehave.readyBlock;
                }
                else
                {
                    if (randNum <= temp[3])
                    {
                        EnemyBehaviour.instance.currentCombatBehaviour = EnemyBehaviour.combatBehave.none;
                        EnemyBehaviour.instance.lastCombatBehaviour = EnemyBehaviour.combatBehave.none;
                    }
                    else
                    {
                        Debug.Log("ERROR DecisionMatrix.cs Line 64");
                    }
                }
            }
        }
    }

    public float getHealthSurrenderPercent() 
    {
        float temp = healthSurrenderPercent / 100;
        return EnemyStats.instance.health * temp;
    }

    public void surrenderDecision() 
    {
        int temp = Random.Range(1, 100);
        if (temp < surrenderChance) 
        {
            EnemyActions.instance.surrender();
        }
    }
}
