using UnityEngine;
using UnityEngine.UI;

public class CheckRotation : MonoBehaviour
{
    public GameObject cube;    // Référence vers le cube
    public GameObject contour; // Référence vers le contour
    private Renderer cubeRenderer; // Pour changer la couleur du cube
    private Button button; // Référence au bouton

    void Start()
    {
        if (cube == null || contour == null)
        {
            Debug.LogError("Cube ou Contour non assigné !");
            return;
        }

        // Récupérer le composant Button (si ce script est attaché à un bouton)
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(CheckRotationMatch);
        }
        else
        {
            Debug.LogError("Aucun bouton trouvé sur cet objet !");
        }

        // Récupérer le Renderer du cube
        cubeRenderer = cube.GetComponent<Renderer>();
        if (cubeRenderer == null)
        {
            Debug.LogError("Le cube n'a pas de Renderer !");
            return;
        }

        // Initialiser la couleur du cube
        cubeRenderer.material.color = Color.red;
    }

    void Update()
    {
        CheckRotationMatch();
    }

    public void CheckRotationMatch()
    {
        if (cube == null || contour == null || cubeRenderer == null)
        {
            return;
        }

        // Récupère l'angle de rotation sur l'axe Z des deux objets
        float cubeRotationZ = cube.transform.eulerAngles.z;
        float contourRotationZ = contour.transform.eulerAngles.z;

        // Calculer la différence absolue entre les angles
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(cubeRotationZ, contourRotationZ));

        // Vérifie si la différence est inférieure à 10°
        if (angleDifference < 10f)
        {
            if (cubeRenderer.material.color != Color.green)
            {
                Debug.Log("✅ Le cube et le contour ont une rotation similaire sur Z.");
                cubeRenderer.material.color = Color.green;
            }
        }
        else
        {
            if (cubeRenderer.material.color != Color.red)
            {
                Debug.Log("❌ Le cube et le contour ont une différence de rotation trop grande sur Z.");
                cubeRenderer.material.color = Color.red;
            }
        }
    }
}
