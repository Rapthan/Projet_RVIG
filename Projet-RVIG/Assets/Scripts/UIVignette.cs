using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class VignetteController : MonoBehaviour
{
    [SerializeField] private TunnelingVignetteController vignette;  
    [SerializeField] private Slider vignetteSlider;     // Référence au Slider VR

    void Start()
    {
        if (vignette)
        {
            // Synchroniser le slider avec la valeur actuelle
            vignetteSlider.value = vignette.defaultParameters.featheringEffect;
            vignetteSlider.onValueChanged.AddListener(UpdateVignette);
        }
    }

    // Mise à jour de la vignette avec le slider
    public void UpdateVignette(float value)
    {
        vignette.defaultParameters.featheringEffect = value;
        Debug.Log("Vignette intensity: " + value);
    }
}