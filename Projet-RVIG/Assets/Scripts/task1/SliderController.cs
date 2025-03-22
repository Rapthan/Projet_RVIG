using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSliderController : MonoBehaviour
{
    public Transform sphere; // La sphère à déplacer
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable plusButton;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable minusButton;

    public Transform minX; // Position minimale sur X
    public Transform maxX;  // Position maximale sur X

    private float step = 100f; // Distance de déplacement

    void Start()
    {
        // Écoute les événements d'interaction
        plusButton.selectEntered.AddListener(MoveRight);
        minusButton.selectEntered.AddListener(MoveLeft);
    }

    void MoveRight(SelectEnterEventArgs args)
    {
        if (sphere.position.x < maxX.position.x)
        {
            Vector3 newPosition = sphere.position;
            newPosition.x += step;
            sphere.position = newPosition;
        }
    }

    void MoveLeft(SelectEnterEventArgs args)
    {
        if (sphere.position.x > minX.position.x)
        {
            Vector3 newPosition = sphere.position;
            newPosition.x -= step;
            sphere.position = newPosition;
        }
    }

    private void OnDestroy()
    {
        // Nettoyage des événements pour éviter les fuites de mémoire
        plusButton.selectEntered.RemoveListener(MoveRight);
        minusButton.selectEntered.RemoveListener(MoveLeft);
    }
}
