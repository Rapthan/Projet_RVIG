using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    private List<Task> _tasks;

    [SerializeField] private Transform player;
    
    [SerializeField] private GameObject arrowTowardTask;
    private Dictionary<Task, Transform> _arrows;
    
    private void Awake()
    {
        Instance = this;
        _tasks = new List<Task>();
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
        _tasks.Add(task);
        GameObject arrow = Instantiate(arrowTowardTask, player);
        _arrows.Add(task, arrow.transform);
    }
}
