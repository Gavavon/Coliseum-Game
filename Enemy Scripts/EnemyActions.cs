using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public static EnemyActions instance;
    private void Awake()
    {
        instance = this;
    }

    public void judgement() 
    {
        //play kneeling animation
        Debug.Log("Waitning for judgement");
        EnemyBehaviour.instance.judgementReady = true;
    }

    public void chosenFighter() 
    {
        EnemyMovement.instance.stepThroughStartFight();
    }

    public void readyBlock() 
    {
        Debug.Log("readyBlock");
        StartCoroutine(readyBlockIEnum());
        //if the player attacks during this time its automatically blocked
    }
    IEnumerator readyBlockIEnum()
    {
        yield return new WaitForSeconds(4);
        EnemyBehaviour.instance.currentCombatBehaviour = EnemyBehaviour.combatBehave.none;
    }

    public void block()
    {
        //play attack animation
        Debug.Log("block");
        EnemyBehaviour.instance.currentCombatBehaviour = EnemyBehaviour.combatBehave.none;
    }

    public void attack() 
    {
        //play attack animation
        Debug.Log("attack");
        EnemyBehaviour.instance.currentCombatBehaviour = EnemyBehaviour.combatBehave.none;
    }

    public void gotHit(int damage) 
    {
        //tranition to knocked animation
        EnemyStats.instance.health -= damage;
        if (EnemyStats.instance.health <= 0)
        {
            Death();
            return;
        }
        if (EnemyStats.instance.health <= DecisionMatrix.instance.getHealthSurrenderPercent()) 
        {
            DecisionMatrix.instance.surrenderDecision();
            return;
        }
        
    }

    public void heal() 
    {
        //run drink potion animation
        if (EnemyStats.instance.health + 100 < EnemyStats.instance.maxHealth)
        {
            EnemyStats.instance.health += 100;
        }
        else 
        {
            int temp = EnemyStats.instance.maxHealth - EnemyStats.instance.health;
            EnemyStats.instance.health += temp;
        }
    }

    public void surrender() 
    {
        //run surrender animation
        Debug.Log("surrender");
    }

    public void Death() 
    {
        //die
        Debug.Log("died");
        EnemyBehaviour.instance.currentEnemyBehaviour = EnemyBehaviour.enemyBehave.none;
        Destroy(gameObject);
    }
}
