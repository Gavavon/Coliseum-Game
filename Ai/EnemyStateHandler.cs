using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler : MonoBehaviour
{
    public enum EnemyTypeState
    {
        Sentry,
        Hunter
    }
    public EnemyTypeState enemyTypeState;
    public EnemyTypeState currentEnemyTypeState { get; set; }

    public enum EnemyATKState
    {
        NotATK,
        Preparing,
        Aiming,
        Throwing
    }
    public EnemyATKState enemyATKState;
    public EnemyATKState currentEnemyATKState { get; set; }

    public enum HunterSubState
    {
        UnSpotted,
        Spotted
    }
    public HunterSubState hunterSubState;
    public HunterSubState currentHunterSubState { get; set; }
    void Start()
    {
        currentEnemyTypeState = enemyTypeState;
        currentEnemyATKState = enemyATKState;
        currentHunterSubState = hunterSubState;
    }

    public static EnemyStateHandler instance;
    private void Awake()
    {
        instance = this;
    }
}
