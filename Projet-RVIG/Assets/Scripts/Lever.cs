using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      Debug.Log("ça marche !");
   }
}
