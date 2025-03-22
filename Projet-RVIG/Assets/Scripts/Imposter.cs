using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imposter : MonoBehaviour
{
    private NPCMovement npcMovement;
    [SerializeField] private Renderer renderer;
    public float timeToKill; //initialisé par le npcManager au spawn
    private bool _canKill;
    private bool _waitForFirstDispatch;

    private void Awake()
    {
        npcMovement = GetComponent<NPCMovement>();
    }

    private void Start()
    {
        npcMovement.taskCompleted.AddListener(() => _canKill = true);
        NPCManager.Instance.dispatched.AddListener(() => _canKill = false);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(renderer.isVisible);
        if (_canKill && !renderer.isVisible && other.TryGetComponent(out NPCMovement npc)) //on pourra rajouter plus timer, comme un cd sur le kill ou un delai avant de tuer si vu sur caméra
        {
            _canKill = false; //ne peut pas tuer 2 fois en 1 point
            if (!_waitForFirstDispatch) {
                _waitForFirstDispatch = true;
                return;
            }
            npc.Die();
        }
    }
}
