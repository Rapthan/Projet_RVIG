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
            //print(gameObject + "; " + agent.destination + "; " + agent.remainingDistance);//bug : dès le début les npc ont finis leur tache avant d'être rerépartis, et donc il peut avoir des meurtres
            hasReachedDestination = true;
            taskCompleted.Invoke();
        }
    }

    public void Die()
    {
        print("I died rip : " + gameObject);
        if (hasReachedDestination) npcManager.TaskCancelled();
        npcManager.RemoveNPCMovement(this);
        Destroy(gameObject); //à remplacer par le spawn d'un cadavre
    }
}
