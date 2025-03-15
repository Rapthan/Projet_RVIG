using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableGravityOnGrab : MonoBehaviour
{
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Écoute les événements de prise et de lâcher avec les bons types d'arguments
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        rb.useGravity = false;  // Désactive la gravité quand l'objet est attrapé
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        rb.useGravity = false;  // Réactive la gravité quand l'objet est lâché
    }
}
