using UnityEngine;
using System.Linq;

public class ValidateObjectPlacement : Task
{
    [SerializeField] private GameObject[] greenObjects;
    [SerializeField] private GameObject[] yellowObjects;

    [SerializeField] private Collider lot1Collider;  // Zone pour les objets jaunes
    [SerializeField] private Collider lot2Collider;  // Zone pour les objets verts

    [SerializeField] private GameObject validationObject;

    void Start()
    {
        base.Start();
        
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(_ => CheckObjectsPlacement());
        }
    }

    public void CheckObjectsPlacement()
    {
        bool allCorrect = true;

        // Vérifier que tous les objets verts sont dans la bonne zone (lot2)
        foreach (GameObject obj in greenObjects)
        {
            if (!IsInsideZone(obj, lot1Collider))
            {
                allCorrect = false;
            }
        }

        // Vérifier que tous les objets jaunes sont dans la bonne zone (lot1)
        foreach (GameObject obj in yellowObjects)
        {
            if (!IsInsideZone(obj, lot2Collider))
            {
                allCorrect = false;
            }
        }

        ChangeValidationObjectColor(allCorrect);
    }

    void ChangeValidationObjectColor(bool isValid)
    {
        if (validationObject != null)
        {
            Renderer renderer = validationObject.GetComponent<Renderer>();
            /*if (renderer != null)
            {
                renderer.material.color = isValid ? Color.green : Color.red;
            }*/

            if (isValid)
            {
                if (renderer != null)
                {
                    renderer.material.color = Color.green;
                }
                TaskComplete();
            }
            else
            {
                if (renderer != null)
                {
                    renderer.material.color = Color.red;
                }
            }
        }
    }

    bool IsInsideZone(GameObject obj, Collider zoneCollider)
    {
        if (zoneCollider == null) return false;
        return zoneCollider.bounds.Contains(obj.transform.position);
    }
}
