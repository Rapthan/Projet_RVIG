using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
   private bool _state;
   
   private void OnTriggerEnter(Collider other)
   {
      _state = true;
   }

   private void OnTriggerExit(Collider other)
   {
      _state = false;
   }

   public bool GetState()
   {
      return _state;
   }
}
