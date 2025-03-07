using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SwitchCamera : MonoBehaviour
{
    /*[SerializeField] private Button button;
    [SerializeField] private List<Camera> cameras;
    private int _currentCamNbr;*/

    [SerializeField] private GameObject display;
    private Camera _currentCam;
    public static SwitchCamera Instance;

    private BatteryManager _batteryManager;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else if (Instance != this) Destroy(this);
        
        /*button.onClick.AddListener(ChangeCameras);
        _currentCamNbr = 0;*/
    }

    private void Start()
    {
        _batteryManager = BatteryManager.Instance;
    }

    public void ChangeCamera(Camera newCamera)
    {
        if (!display.activeSelf) display.SetActive(true);
        if (_currentCam) _currentCam.gameObject.SetActive(false);
        _currentCam = newCamera;
        _currentCam.gameObject.SetActive(true);
        
        _batteryManager.OnActivation();
    }

    public void CloseCameras()
    {
        display.SetActive(false);
        
        _batteryManager.OnDisactivation();
    }
}
