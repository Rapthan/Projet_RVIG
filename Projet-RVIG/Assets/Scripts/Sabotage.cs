using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Sabotage : MonoBehaviour
{
    private SwitchCamera _switchCamera;
    
    private void Start()
    {
        _switchCamera = SwitchCamera.Instance;
    }

    protected void Activation()
    {
        _switchCamera.Sabotaged();
    }
    
    protected void Disactivation()
    {
        _switchCamera.Unsabotaged();
    }
}
