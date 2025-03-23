using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class VignetteController : MonoBehaviour
{
    [SerializeField] private TunnelingVignetteController vignette;  
    [SerializeField] private Slider vignetteSlider;     // Référence au Slider VR
    [SerializeField] private bool sliderMove;

    void Start()
    {
        if (vignette && sliderMove)
        {
            // Synchroniser le slider avec la valeur actuelle
            vignetteSlider.value = 1-vignette.defaultParameters.apertureSize;
            vignetteSlider.onValueChanged.AddListener(UpdateVignetteMove);
        }
        else 
        {
            // Synchroniser le slider avec la valeur actuelle
            vignetteSlider.value = 1-vignette.defaultParameters.apertureSize;
            vignetteSlider.onValueChanged.AddListener(UpdateVignette);
        }
    }

    // Mise à jour de la vignette avec le slider
    public void UpdateVignette(float value)
    {
        foreach(var provider in vignette.locomotionVignetteProviders){
            if (provider != null && provider.locomotionProvider.name.Contains("Turn")){
                provider.vignetteParameters.apertureSize = 1-value;
            }
        }
    }

    // Mise à jour de la vignette avec le slider
    public void UpdateVignetteMove(float value)
    {
        foreach(var provider in vignette.locomotionVignetteProviders){
            if (provider != null && provider.locomotionProvider.name.Contains("Move")){
                provider.vignetteParameters.apertureSize = 1-value;
            }
        }
    }
}