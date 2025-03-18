using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;
    private List<NPCMovement> npcMovements;
    [SerializeField] private List<Vector3> taskPositions;
    [SerializeField] private float timeBeforeDispatch = 2f;

    private int tasksCompletedNumber;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else if (Instance != this) Destroy(this);
        
        npcMovements = new List<NPCMovement>();
        tasksCompletedNumber = 0;
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        //taskPositions = TaskManager.Instance.TasksPositions();
        StartCoroutine(Dispatch());
    }

    public void AddNPCMovement(NPCMovement npcMovement)
    {
        npcMovements.Add(npcMovement);
        npcMovement.taskCompleted.AddListener(TaskCompleted);
    }

    public void RemoveNPCMovement(NPCMovement npcMovement)
    {
        npcMovements.Remove(npcMovement);
    }

    private NPCMovement SelectNewNPC(List<NPCMovement> nonSelectedNPCs)
    {
        NPCMovement newNPC = nonSelectedNPCs[Random.Range(0, nonSelectedNPCs.Count)];
        nonSelectedNPCs.Remove(newNPC);
        return newNPC;
    }

    private IEnumerator Dispatch()
    {
        yield return new WaitForSeconds(timeBeforeDispatch);
            
        List<NPCMovement> nonSelectedNPCs = new List<NPCMovement>(npcMovements);
        List<Vector3> destinations = new List<Vector3>();
        
        while (nonSelectedNPCs.Count > 0)
        {
            Vector3 destination;
            do destination = taskPositions[Random.Range(0, taskPositions.Count)];
            while (destinations.Contains(destination));
            destinations.Add(destination);
            
            int numberShouldBeAtTask = (nonSelectedNPCs.Count < 6 ? nonSelectedNPCs.Count : 3);
            int numberPresentAtTask;
            
            for (numberPresentAtTask = 0;
                 numberPresentAtTask < numberShouldBeAtTask;
                 numberPresentAtTask++)
            {
                NPCMovement NPC = SelectNewNPC(nonSelectedNPCs);
                NPC.agent.SetDestination(destination);
                NPC.hasReachedDestination = false;
            }
        }
    }

    public void TaskCompleted()
    {
        tasksCompletedNumber++;
        if (tasksCompletedNumber >= npcMovements.Count)
        {
            StartCoroutine(Dispatch());
            tasksCompletedNumber = 0;
        }
    }

    public void TaskCancelled()
    {
        if (tasksCompletedNumber > 0) tasksCompletedNumber--;
    }
}
