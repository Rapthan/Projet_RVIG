using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NPCMovement : MonoBehaviour
{
    public UnityEvent taskCompleted;
    public NavMeshAgent agent;
    private NPCManager npcManager;

    public bool hasReachedDestination;
    private bool _waitForFirstFrame;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        taskCompleted = new UnityEvent();
        hasReachedDestination = true;
    }

    private void Start()
    {
        npcManager = NPCManager.Instance;
        npcManager.AddNPCMovement(this);
    }
    
    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !hasReachedDestination)
        {
            hasReachedDestination = true;
            taskCompleted.Invoke();
        }
    }

    public void Die()
    {
        if (hasReachedDestination) npcManager.TaskCancelled();
        npcManager.RemoveNPCMovement(this);
        Destroy(gameObject); //à remplacer par le spawn d'un cadavre
    }
}
