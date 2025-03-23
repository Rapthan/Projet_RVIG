using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLeversTask : Task
{
    [SerializeField] List<Lever> levers = new List<Lever>();
    private int _leverCount = 0;

    private void Start()
    {
        base.Start();
        foreach (Lever lever in levers)
        {
            lever.leverActivated.AddListener(LeverActivated);
        }
    }

    private void LeverActivated()
    {
        //print(levers.Count);
        _leverCount++;
        //print(_leverCount);
        if (_leverCount == levers.Count)
        {
            //print(onComplete);
            TaskComplete();
        }
    }
}
