using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Task : MonoBehaviour
{
    [SerializeField] private float batteryRefill;
    [SerializeField] private AudioSource audioSource;
    public UnityEvent onComplete;
    private bool _alreadyActivated;
    public string taskName;

    public void Start()
    {
        //à appeler si l'implémentation héritée utilise un start
        onComplete = new UnityEvent();
        TaskManager.Instance.AddTask(this);
    }

    protected void TaskComplete()
    {
        if (!_alreadyActivated)
        {
            onComplete.Invoke();
            audioSource.Play();
            BatteryManager.Instance.AddBattery(batteryRefill);
            _alreadyActivated = true;
        }
    }
}
