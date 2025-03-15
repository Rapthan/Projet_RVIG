using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ValidateObjectPlacement : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToCheck; // Les 6 objets à vérifier
    [SerializeField] private GameObject[] lot1Cubes; // 4 cubes qui forment le premier lot (vert)
    [SerializeField] private GameObject[] lot2Cubes; // 4 cubes qui forment le deuxième lot (jaune)
    [SerializeField] private Button checkButton; // Bouton pour lancer la vérification

    private Material greenMaterial;
    private Material yellowMaterial;

    void Start()
    {
        // Récupérer automatiquement les matériaux des lots à partir des cubes
        if (lot1Cubes.Length > 0) 
            greenMaterial = lot1Cubes[0].GetComponent<Renderer>().sharedMaterial;
        
        if (lot2Cubes.Length > 0) 
            yellowMaterial = lot2Cubes[0].GetComponent<Renderer>().sharedMaterial;

        // Vérifier si le bouton est assigné et lier l'événement
        if (checkButton == null)
        {
            checkButton = FindObjectOfType<Button>(); // Trouve un bouton dans la scène
        }

        if (checkButton != null)
        {
            checkButton.onClick.AddListener(CheckObjectsPlacement);
        }
        else
        {
            Debug.LogError("Aucun bouton trouvé dans la scène !");
        }
    }

    void CheckObjectsPlacement()
    {
        bool allCorrect = true;

        foreach (GameObject obj in objectsToCheck)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();
            if (objRenderer == null) continue;

            Material objMaterial = objRenderer.sharedMaterial;

            // Vérifier si l'objet appartient au bon lot
            if (objMaterial == greenMaterial)
            {
                if (!IsInsideLot(obj, lot1Cubes))
                {
                    Debug.Log($"{obj.name} (VERT) est mal placé !");
                    allCorrect = false;
                }
            }
            else if (objMaterial == yellowMaterial)
            {
                if (!IsInsideLot(obj, lot2Cubes))
                {
                    Debug.Log($"{obj.name} (JAUNE) est mal placé !");
                    allCorrect = false;
                }
            }
        }

        if (allCorrect)
        {
            Debug.Log("Tous les objets sont bien placés !");
        }
        else
        {
            Debug.Log("Certains objets sont mal placés !");
        }
    }

    bool IsInsideLot(GameObject obj, GameObject[] lotCubes)
    {
        // Déterminer les bornes min et max du lot
        Vector3 minBounds = lotCubes.Select(c => c.transform.position).Aggregate(Vector3.Min);
        Vector3 maxBounds = lotCubes.Select(c => c.transform.position).Aggregate(Vector3.Max);

        // Vérifier si l'objet est dans les limites du lot
        Vector3 objPos = obj.transform.position;
        return (objPos.x >= minBounds.x && objPos.x <= maxBounds.x) &&
               (objPos.y >= minBounds.y && objPos.y <= maxBounds.y) &&
               (objPos.z >= minBounds.z && objPos.z <= maxBounds.z);
    }
}
