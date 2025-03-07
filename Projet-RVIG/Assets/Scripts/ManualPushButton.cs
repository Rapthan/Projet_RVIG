using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ManualPushButton : MonoBehaviour
{
    public UnityEvent trigger;
    
    private XRBaseInteractable _interactable;
    private bool _isHovering;
    
    private Vector3 _buttonPosition;
    private Transform _pokeAttachTransform;
    private bool _hasActivated;
    [SerializeField] private float distanceToActivate = 1;
    
    private void Awake()
    {
        _buttonPosition = transform.position;
        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.hoverEntered.AddListener(OnHover);
        _interactable.hoverExited.AddListener(OnExitHover);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHovering)
        {
            if (!_hasActivated && Vector3.Distance(_buttonPosition, _pokeAttachTransform.position) <= distanceToActivate)
            {
                trigger.Invoke();
                _hasActivated = true;
            }
            else if (_hasActivated && Vector3.Distance(_buttonPosition, _pokeAttachTransform.position) > distanceToActivate)
            {
                _hasActivated = false;
            }
        }
    }

    public void OnHover(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)eventArgs.interactorObject;
            _isHovering = true;
            
            _pokeAttachTransform = interactor.attachTransform;
        }
    }

    public void OnExitHover(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactorObject is XRPokeInteractor)
        {
            _isHovering = false;
            if (_hasActivated) _hasActivated = false;
        }
    }
}
