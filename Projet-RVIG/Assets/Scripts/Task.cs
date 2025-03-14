using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Task : MonoBehaviour
{
    [SerializeField] private float batteryRefill;
    private bool _alreadyActivated;
    public string taskName;

    private void Start()
    {
        //à appeler si l'implémentation héritée utilise un start
        TaskManager.Instance.AddTask(this);
    }

    protected void TaskComplete()
    {
        if (!_alreadyActivated)
        {
            BatteryManager.Instance.AddBattery(batteryRefill);
            _alreadyActivated = true;
        }
    }
}
