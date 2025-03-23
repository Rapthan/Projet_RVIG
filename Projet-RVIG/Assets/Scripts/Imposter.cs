using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Imposter : MonoBehaviour
{
    private NPCMovement npcMovement;
    [SerializeField] private float killCd = 20;
    [SerializeField] private int oneInNumberToKillProbs = 3;
    private bool _canKill;
    private bool _isInCd;
    private bool _waitForFirstDispatch;
    private List<Collider> _crewmatesAlreadyTriedToKill;
    public Renderer renderer;//à transmettre en serialisant
    
    private NPCManager npcManager;
    [SerializeField] private MeshRenderer corpsePrefab;

    private void Awake()
    {
        npcMovement = GetComponent<NPCMovement>();
        _crewmatesAlreadyTriedToKill = new List<Collider>();
    }

    private void Start()
    {
        npcManager = NPCManager.Instance;
        npcManager.AddImposter(this);
        
        npcMovement.taskCompleted.AddListener(() =>
        {
            _crewmatesAlreadyTriedToKill.Clear();
            _canKill = true;
        });
        NPCManager.Instance.dispatched.AddListener(() => _canKill = false);
        VoteMenu.Instance.AddImposter(this);
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(renderer.isVisible);
        if (_canKill && !_isInCd && !_crewmatesAlreadyTriedToKill.Contains(other) && other.TryGetComponent(out NPCMovement npc)) //on pourra rajouter plus timer, comme un cd sur le kill ou un delai avant de tuer si vu sur caméra
        {
            _canKill = false; //ne peut pas tuer 2 fois en 1 point
            if (!_waitForFirstDispatch) {
                _waitForFirstDispatch = true;
                return;
            }

            if (Random.Range(1, oneInNumberToKillProbs) == 1)
            {
                npc.Die();
                StartCoroutine(KillCd());
            }
            else
            {
                _crewmatesAlreadyTriedToKill.Add(other);
            }
        }
    }

    public void Die()
    {
        VoteMenu.Instance.RemoveImposter(this);
        if (_canKill) npcManager.TaskCancelled();
        npcManager.RemoveImposter(this);
        Instantiate(corpsePrefab, transform.position - 0.5f * Vector3.up, corpsePrefab.transform.rotation).material.color = renderer.material.color;
        Destroy(gameObject);
    }

    private IEnumerator KillCd()
    {
        _isInCd = true;
        yield return new WaitForSeconds(killCd);
        _isInCd = false;
    }
}
