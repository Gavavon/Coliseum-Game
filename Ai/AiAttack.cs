using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAttack : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    [HideInInspector]
    public Transform playerLastSeen;

    [Header("Attacking")]
    public SphereCollider sphereCol;
    [HideInInspector]
    public bool playerInSight = false;
    [HideInInspector]
    public bool spotted = false;
    [HideInInspector]
    public bool aimed = false;
    public float bulletDamage = 10;
    public float fieldOfViewAngle = 110f;
    public float reactionTime = 1;
    public float timeBetweenShots = 1;

    [Header("Nav Mesh Agent")]
    NavMeshAgent agent;

    public static AiAttack instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //check if the player is spotted 
        if (playerInSight) 
        {
            switch (EnemyStateHandler.instance.currentEnemyATKState) 
            {
                case EnemyStateHandler.EnemyATKState.Preparing:
                    ReadyDagger();
                    break;
                case EnemyStateHandler.EnemyATKState.Aiming:
                    AimDaggers();
                    break;
                case EnemyStateHandler.EnemyATKState.Throwing:
                    ThrowDaggers();
                    break;
            }
        }

        Debug.Log(EnemyStateHandler.instance.currentEnemyATKState);
    }
    public void ReadyDagger() 
    {
        //run the grab dagger animation
        //set aimed to be true
        EnemyStateHandler.instance.currentEnemyATKState = EnemyStateHandler.EnemyATKState.Aiming;
    }
    IEnumerator AimDaggers() 
    {
        EnemyStateHandler.instance.currentEnemyATKState = EnemyStateHandler.EnemyATKState.Throwing;
        yield return new WaitForSeconds(0.5f);
        /*
        switch (EnemyStateHandler.instance.currentEnemyTypeState)
        {
            case EnemyStateHandler.EnemyTypeState.Sentry:
                //run aim dagger animation

                yield return new WaitForSeconds(0.5f);
                break;
            case EnemyStateHandler.EnemyTypeState.Hunter:
                switch (EnemyStateHandler.instance.hunterSubState)
                {
                    case EnemyStateHandler.HunterSubState.UnSpotted:
                        //run aim dagger animation

                        yield return new WaitForSeconds(3);
                        break;
                    case EnemyStateHandler.HunterSubState.Spotted:
                        //run aim dagger animation

                        yield return new WaitForSeconds(1);
                        break;
                }
                break;
        }
        */
    }
    public void ThrowDaggers()
    {
        switch (EnemyStateHandler.instance.currentEnemyTypeState)
        {
            case EnemyStateHandler.EnemyTypeState.Sentry:
                // throw 3 daggers at a certain rate
                Throw(1);
                break;
            case EnemyStateHandler.EnemyTypeState.Hunter:
                // throw 1 dagger at a certain rate
                Throw(1);
                break;
        }
        DaggerAnimation.instance.ReturnDagger();
        EnemyStateHandler.instance.currentEnemyATKState = EnemyStateHandler.EnemyATKState.NotATK;
    }

    IEnumerator Throw(int daggerAmont)
    {
        //throw a clone of the object forward
        //have the clone stop when it intersects with anything
        //when it intersects with something wait x amount of time then scale its size down to zero and delete it
        yield return new WaitForSeconds(0f);
        DaggerController.instance.CreateDaggerClone();
        DaggerController.instance.AddDaggerForce();

        //return back to aim
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCol.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        transform.LookAt(player.transform);
                        EnemyStateHandler.instance.currentEnemyATKState = EnemyStateHandler.EnemyATKState.Preparing;
                    }
                    else
                    {
                        playerInSight = false;
                    }
                }
            }
        }
    }
}
