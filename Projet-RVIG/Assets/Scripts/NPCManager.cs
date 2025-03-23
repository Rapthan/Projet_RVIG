using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    [SerializeField] private NPCMovement npcPrefab;
    [SerializeField] private Imposter imposterPrefab;
    [SerializeField] [Range(3,10)] private int numberOfNPC;
    [SerializeField] [Range(1,3)] private int numberOfImposter; //nombre d'imposteur parmis les NPC
    [SerializeField] private float radiusSpawn;
    private List<NPCMovement> npcMovements;
    private List<Imposter> _imposters;
    
    [SerializeField] private List<Vector3> taskPositions;
    [SerializeField] private float timeBeforeDispatch = 2f;
    [SerializeField] private List<Color> colors;

    private int tasksCompletedNumber;
    public UnityEvent dispatched;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else if (Instance != this) Destroy(this);
        
        npcMovements = new List<NPCMovement>();
        _imposters = new List<Imposter>();
        tasksCompletedNumber = 0;
        dispatched = new UnityEvent();
    }

    private IEnumerator Start()
    {
        SpawnNPC();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        //taskPositions = TaskManager.Instance.TasksPositions();
        StartCoroutine(Dispatch());
    }

    public void SetNumberOfNPC(int n)
    {
        numberOfNPC = n;
    }

    public void SetNumberOfImposter(int n)
    {
        numberOfImposter = n;
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

    public void AddImposter(Imposter imposter)
    {
        _imposters.Add(imposter);
    }

    public void RemoveImposter(Imposter imposter)
    {
        _imposters.Remove(imposter);
    }

    private NPCMovement SelectNewNPC(List<NPCMovement> nonSelectedNPCs)
    {
        NPCMovement newNPC = nonSelectedNPCs[Random.Range(0, nonSelectedNPCs.Count)];
        nonSelectedNPCs.Remove(newNPC);
        return newNPC;
    }

    private void SpawnNPC()
    {
        List<int> positionIndexes = new List<int>();
        for (int i = 0; i < numberOfNPC; i++)
        {
            positionIndexes.Add(i);
        }

        for (int i = 0; i < numberOfNPC - numberOfImposter; i++)
        {
            int positionIndex = positionIndexes[Random.Range(0, positionIndexes.Count)];
            positionIndexes.Remove(positionIndex);

            Vector3 pos = transform.position + new Vector3(radiusSpawn * Mathf.Cos(2 * positionIndex * Mathf.PI / numberOfNPC), 1.25f,
                radiusSpawn * Mathf.Sin(2 * positionIndex * Mathf.PI / numberOfNPC));
            
            Instantiate(npcPrefab,
                pos,
                Quaternion.identity).renderer.material.color = colors[positionIndex];
        }
        
        for (int i = 0; i < numberOfImposter; i++)
        {
            int positionIndex = positionIndexes[Random.Range(0, positionIndexes.Count)];
            positionIndexes.Remove(positionIndex);

            Vector3 pos = transform.position + new Vector3(radiusSpawn * Mathf.Cos(2 * positionIndex * Mathf.PI / numberOfNPC), 1.25f,
                radiusSpawn * Mathf.Sin(2 * positionIndex * Mathf.PI / numberOfNPC));

            Instantiate(imposterPrefab, pos, Quaternion.identity).renderer.material.color = colors[positionIndex];
        }
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
        
        dispatched.Invoke();
    }

    public void TaskCompleted()
    {
        tasksCompletedNumber++;
        //print(tasksCompletedNumber);
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
