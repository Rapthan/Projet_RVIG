using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
            arrow.LookAt(new Vector3(arrowTask.Key.transform.position.x, arrow.position.y, arrowTask.Key.transform.position.z));
        }
    }

    public void AddTask(Task task)
    {
        GameObject arrow = Instantiate(arrowTowardTask, Vector3.zero, quaternion.identity, player);
        arrow.transform.localPosition = 0.2f * Vector3.up;
        _arrows.Add(task, arrow.transform);
        task.onComplete.AddListener(() => RemoveTask(task));
    }

    private void RemoveTask(Task task)
    {
        Destroy(_arrows[task].gameObject);
        _arrows.Remove(task);
    }

    public Dictionary<Task, Vector3> TasksPositions()
    {
        Dictionary<Task, Vector3> positions = new Dictionary<Task, Vector3>();
        foreach (var arrowTask in _arrows)
        {
            Task task = arrowTask.Key;
            positions.Add(task, task.transform.position);
        }
        return positions;
    }
}
