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

    private bool _batteryBlocked;
    
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

        _batteryBlocked = false;
    }

    private void Start()
    {
        _batteryManager = BatteryManager.Instance;
        _batteryManager.batteryEmptied.AddListener(OnBatteryBlocked);
        _batteryManager.batteryUnemptied.AddListener(OnBatteryUnblocked);
    }

    public void ChangeCamera(Camera newCamera)
    {
        if (_batteryBlocked) return;
        
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

    public void Sabotaged()
    {
        
    }

    public void Unsabotaged()
    {
        
    }

    private void OnBatteryBlocked()
    {
        display.SetActive(false);
        _batteryBlocked = true;
    }

    private void OnBatteryUnblocked()
    {
        _batteryBlocked = false;
    }
}
