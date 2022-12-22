using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = player.position;
    }
}
