using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [SerializeField] private Transform player;
    
    [SerializeField] private GameObject arrowTowardTask;
    private Dictionary<Task, Transform> _arrows;
    
    private void Awake()
    {
        Instance = this;
        _arrows = new Dictionary<Task, Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var arrowTask in _arrows)
        {
            Transform arrow = arrowTask.Value;
            arrow.LookAt(arrowTask.Key.transform);
        }
    }

    public void AddTask(Task task)
    {
        GameObject arrow = Instantiate(arrowTowardTask, player);
        _arrows.Add(task, arrow.transform);
    }

    public List<Vector3> TasksPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (var arrowTask in _arrows)
        {
            positions.Add(arrowTask.Key.transform.position);
        }
        return positions;
    }
}
