using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HingeJoint))]
public class Lever : MonoBehaviour
{
   private Rigidbody _rigidbody;
   private HingeJoint _hinge;
   public UnityEvent leverActivated;
   private bool _alreadyActivated = false;

   private void Awake()
   {
      _hinge = GetComponent<HingeJoint>();
      _rigidbody = GetComponent<Rigidbody>();
      leverActivated = new UnityEvent();
   }

   private void Update()
   {
      if (_hinge.angle >=60 && !_alreadyActivated)
      {
         _alreadyActivated = true;
         //print(leverActivated);
         _rigidbody.freezeRotation = true;
         leverActivated.Invoke();
         Destroy(this);
      }
   }
}
