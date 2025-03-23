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
        GameObject arrow = Instantiate(arrowTowardTask, 0.2f * Vector3.up, quaternion.identity, player);
        _arrows.Add(task, arrow.transform);
        task.onComplete.AddListener(() => RemoveTask(task));
    }

    private void RemoveTask(Task task)
    {
        Destroy(_arrows[task].gameObject);
        _arrows.Remove(task);
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
