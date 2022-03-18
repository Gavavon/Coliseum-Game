using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public enum fightStatus 
    { 
        preFight,
        currentlyFighting,
        postFight,
        pause
    }
    public fightStatus currentFightStatus;

    public GameObject[] fightersOBJ;
    public EnemyBehaviour[] fighters;
    public EnemyBehaviour currentFighter;

    public static FightManager instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentFightStatus = fightStatus.pause;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFightStatus == fightStatus.preFight) 
        {
            int temp = Random.Range(0, fighters.Length - 1);
            currentFighter = fighters[temp];
            fightersOBJ[temp].SetActive(true);
            currentFighter.chosenFighter = true;
        }
    }
}
