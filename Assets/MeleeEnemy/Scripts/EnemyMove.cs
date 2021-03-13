using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform destination;
    NavMeshAgent navMeshAgent;
    
    void SetDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
            

    }


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        SetDestination();
    }
}
