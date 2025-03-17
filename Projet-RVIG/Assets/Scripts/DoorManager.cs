using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
   [SerializeField] private Animator _animator;
    
    public void ActivateDoor()
    {
        _animator.SetBool("character_nearby", !_animator.GetBool("character_nearby"));
    }
}
