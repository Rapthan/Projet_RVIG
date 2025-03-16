using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class handAnimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty inputTriggerActions;
    [SerializeField] private InputActionProperty inputGripAction;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = inputTriggerActions.action.ReadValue<float>();
        float gripValue = inputGripAction.action.ReadValue<float>();
        
        _anim.SetFloat("Trigger",triggerValue);
        _anim.SetFloat("Grip",gripValue);
    }
}
