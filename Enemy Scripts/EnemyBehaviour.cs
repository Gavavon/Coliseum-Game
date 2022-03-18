using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public bool chosenFighter = false;
    public bool judgementReady = false;
    public enum enemyBehave 
    { 
        startFight,
        inCombat,
        endFight,
        none
    }
    public enemyBehave currentEnemyBehaviour;

    public enum combatBehave 
    { 
        attack,
        readyBlock,
        randomBlock,
        none
    }
    public combatBehave currentCombatBehaviour;
    public combatBehave lastCombatBehaviour;
    public enum combatmovement 
    { 
        rotateRight,
        rotateLeft,
        standStill,
        approach,
        none
    }
    public combatmovement currentCombatMovement;

    public static EnemyBehaviour instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyBehaviour = enemyBehave.none;
        currentCombatBehaviour = combatBehave.none;
        lastCombatBehaviour = combatBehave.none;
        currentCombatMovement = combatmovement.none;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (chosenFighter) 
        {
            EnemyActions.instance.chosenFighter();
        }
    }
}
