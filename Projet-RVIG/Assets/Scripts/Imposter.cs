using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imposter : MonoBehaviour
{
    private NPCMovement npcMovement;

    private void Awake()
    {
        npcMovement = GetComponent<NPCMovement>();
    }

    private void Start()
    {
        npcMovement.taskCompleted.AddListener(III);
    }

    private void III()
    {
        
    }
}
