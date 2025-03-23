using UnityEngine;


public class CheckRotation : Task
{
    public GameObject cube;
    public GameObject contour;
    private Renderer cubeRenderer;

    void Start()
    {
        base.Start();
        
        if (cube == null || contour == null)
        {
            Debug.LogError("Cube ou Contour non assigné !");
            return;
        }

        // Récupérer le Renderer du cube
        cubeRenderer = cube.GetComponent<Renderer>();
        if (cubeRenderer == null)
        {
            Debug.LogError("Le cube n'a pas de Renderer !");
            return;
        }

        // Essayer de récupérer le XR Interactable et ajouter l'événement
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(_ => CheckRotationMatch());
        }
        else
        {
            Debug.LogError("Aucun XR Interactable trouvé !");
        }
    }

    public void CheckRotationMatch()
    {
        if (cube == null || contour == null || cubeRenderer == null)
        {
            return;
        }

        float cubeRotationZ = cube.transform.eulerAngles.z;
        float contourRotationZ = contour.transform.eulerAngles.z;
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(cubeRotationZ, contourRotationZ));

        print(angleDifference);
        if (angleDifference < 5f || Mathf.Abs(angleDifference - 90) < 5f || Mathf.Abs(angleDifference - 180) < 5f)
        {
            if (cubeRenderer.material.color != Color.green)
            {
                Debug.Log("✅ Aligné !");
                cubeRenderer.material.color = Color.green;
                TaskComplete();
            }
        }
        else
        {
            if (cubeRenderer.material.color != Color.red)
            {
                Debug.Log("❌ Pas aligné !");
                cubeRenderer.material.color = Color.red;
            }
        }
    }
}
