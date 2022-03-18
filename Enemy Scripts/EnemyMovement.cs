using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform startPos;
    public Transform walkInPos;
    private bool startPosReached = false;
    private bool walkInReached = false;

    public Transform judgementPos;

    public GameObject[] targets;
    public GameObject primaryTarget;
    public bool targetSelected = false;
    //public healthBars targets

    private NavMeshAgent agent;

    public float combatTimerStartAmt = 60;
    public float combatTimer;

    public static EnemyMovement instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        combatTimer = combatTimerStartAmt;
    }

	private void Update()
	{
        switch (EnemyBehaviour.instance.currentEnemyBehaviour)
        {
            case EnemyBehaviour.enemyBehave.startFight:
                stepThroughStartFight();
                break;
            case EnemyBehaviour.enemyBehave.inCombat:
                if (!targetSelected) 
                { 
                    selectTarget();
                }
                inCombat();
                break;
            case EnemyBehaviour.enemyBehave.endFight:
                prepareForJudgement();
                break;
        }

        combatTimer -= Time.deltaTime;

        if (combatTimer <= 0.0f)
        {
            enemyMovementUpdate();
        }
	}

    public void stepThroughStartFight() 
    {
        if (agent.remainingDistance != 0)
        {
            if (!startPosReached)
            {
                changeDest(startPos);
            }
            if (!walkInReached && startPosReached)
            {
                changeDest(walkInPos);
            }
            if (startPosReached && walkInReached)
            {
                //run bow animation
                EnemyBehaviour.instance.currentEnemyBehaviour = EnemyBehaviour.enemyBehave.inCombat;
            }
        }
        else 
        {
            if (!startPosReached)
            {
                startPosReached = true;
            }
            if (!walkInReached && startPosReached)
            {
                walkInReached = true;
            }
        }
    }

    public void prepareForJudgement() 
    {
        changeDest(judgementPos);
        if (agent.remainingDistance == 0)
        {
            EnemyActions.instance.judgement();
        }
    }

    public void inCombat() 
    {
        //movement
        switch (EnemyBehaviour.instance.currentCombatMovement) 
        {
            case EnemyBehaviour.combatmovement.none:
                EnemyBehaviour.instance.currentCombatMovement = EnemyBehaviour.combatmovement.approach;
                break;
            case EnemyBehaviour.combatmovement.approach:
                //move into range of primary target
                break;
            case EnemyBehaviour.combatmovement.standStill:
                //remain approach target
                break;
            case EnemyBehaviour.combatmovement.rotateRight:
                //rotate around target
                break;
            case EnemyBehaviour.combatmovement.rotateLeft:
                //rotate around target
                break;
        }
        if (Vector3.Distance(gameObject.transform.position, primaryTarget.transform.position) > 10) 
        {
            EnemyBehaviour.instance.currentCombatMovement = EnemyBehaviour.combatmovement.approach;
        }


        //actions
        switch (EnemyBehaviour.instance.currentCombatBehaviour)
        {
            case EnemyBehaviour.combatBehave.none:
                StartCoroutine(readyNextAttack());
                break;
            case EnemyBehaviour.combatBehave.attack:
                EnemyActions.instance.attack();
                break;
            case EnemyBehaviour.combatBehave.randomBlock:
                EnemyActions.instance.block();
                break;
            case EnemyBehaviour.combatBehave.readyBlock:
                EnemyActions.instance.readyBlock();
                break;
        }
    }
    IEnumerator readyNextAttack()
    {
        yield return new WaitForSeconds(EnemyStats.instance.atkSpdSec);
        DecisionMatrix.instance.combatDecision();
    }

    public void selectTarget() 
    {
        //if(primaryTarget.health > 0) { 
        GameObject closest = targets[0];
        if (targets.Length < 1) 
        {
            for (int i = 1; i < targets.Length; i++)
            {
                if (Vector3.Distance(gameObject.transform.position, targets[i].transform.position) < Vector3.Distance(gameObject.transform.position, closest.transform.position)) 
                { 
                    closest = targets[i];
                }
            }
        }
        primaryTarget = closest;
        //}
    }

    public void enemyMovementUpdate() 
    {

        resetTimer();
    }

    public void resetTimer(float amount)
    {
        combatTimer = amount;
    }
    public void resetTimer() 
    {
        combatTimer = combatTimerStartAmt;
    }
    public void changeDest(Transform dest) 
    {
        agent.destination = dest.position;
    }
}
