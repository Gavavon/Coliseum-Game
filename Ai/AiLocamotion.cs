using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocamotion : MonoBehaviour
{
    [Header("Nav Mesh Agent")]
    NavMeshAgent agent;
    private int nextDestination = 0;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    [HideInInspector]
    public float timer = 0.0f;

    [Header("Animator")]
    Animator animator;

    [Header("Player")]
    private Transform playerTransfrom;
    public Transform[] destinations;
    //----------------------------------------------------------------
    #region Start & Update
    void Start()
    {
        playerTransfrom = AiAttack.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.autoBraking = false;
    }

    void Update()
    {
        switch (EnemyStateHandler.instance.currentEnemyTypeState)
        {
            case EnemyStateHandler.EnemyTypeState.Sentry:
                agent.destination = agent.transform.position;

                break;
            case EnemyStateHandler.EnemyTypeState.Hunter:
                Patrol();

                break;
        }

        FreezeChecking();

    }
    #endregion
    //----------------------------------------------------------------
    #region Movement
    public void Patrol()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            try
            {
                if (false/*AiAttack.instance.spotted*/)
                {
                    //Hunting(true);
                }
                else
                {
                    Walk();
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    {
                        GotoNextDest();
                    }
                }
            }
            catch 
            {
                Debug.LogError("Player In Sight is not set");
            }
            timer = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
    #endregion
    //----------------------------------------------------------------
    #region walking between points
    public void GotoNextDest()
    {
        if (destinations.Length == 0)
        {
            Debug.LogError("Destinaitons not set");
            return;
        }
        nextDestination = (nextDestination + 1) % destinations.Length;
    }
    public void Walk() 
    {
        float sqDistance = (destinations[nextDestination].position - agent.destination).sqrMagnitude;
        if (sqDistance > maxDistance * maxDistance)
        {
            agent.destination = destinations[nextDestination].position;
        }
    }
    #endregion
    //----------------------------------------------------------------
    #region Chasing the player and patrolling temp areas
    public void Hunting(bool spotted)
    {
        //if the player is spotted chase the player's last spotted location then walk around in a sphere area randomly looking around
        //create a 3d sphere on player's last known location then walk around inside it

        GameObject tempRoamingSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tempRoamingSphere.transform.position = new Vector3(0, 0, 0);

        if (spotted) 
        {
            float sqDistance = (playerTransfrom.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                agent.destination = playerTransfrom.position;
            }
        }
        
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
    #endregion
    //----------------------------------------------------------------
    public void FreezeChecking()
    {
        if (Freeze.instance.FREEZE)
        {
            agent.destination = agent.transform.position;
        }
    }
}
