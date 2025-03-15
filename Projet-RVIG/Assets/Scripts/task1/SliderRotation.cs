using UnityEngine;

public class SliderRotation : MonoBehaviour
{
    [SerializeField] private Transform sphere; // Référence à la sphère
    [SerializeField] private Transform cube;   // Référence au cube
    [SerializeField] private float rotationSpeed = 100f; // Vitesse de rotation du cube

    private void Update()
    {
        if (sphere != null && cube != null)
        {
            // Calculer la position relative de la sphère par rapport au centre du slider
            Vector3 spherePosition = sphere.position;
            Vector3 sliderCenter = transform.position + new Vector3(transform.localScale.x / 2, 0, 0);
            float relativePosition = (spherePosition.x - sliderCenter.x) / (transform.localScale.x / 2);

            // Faire pivoter le cube en fonction de la position de la sphère
            float rotationAngle = relativePosition * 180f; // Rotation de -180 à 180 degrés
            cube.rotation = Quaternion.Euler(0, 0, rotationAngle); // Rotation sur l'axe Z
        }
    }
}