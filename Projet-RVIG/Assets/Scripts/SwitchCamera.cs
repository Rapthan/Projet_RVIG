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
    private bool _sabotaged;
    
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
        _sabotaged = false;
    }

    private void Start()
    {
        _batteryManager = BatteryManager.Instance;
        _batteryManager.batteryEmptied.AddListener(OnBatteryBlocked);
        _batteryManager.batteryUnemptied.AddListener(OnBatteryUnblocked);
        
        //Sabotage.startSabotaging.AddListener(OnSabotaged);
        //Sabotage.endSabotaging.AddListener(OnUnsabotaged);
    }

    public void ChangeCamera(Camera newCamera)
    {
        if (_batteryBlocked || _sabotaged) return;
        
        if (!display.activeSelf) display.SetActive(true);
        if (_currentCam) _currentCam.gameObject.SetActive(false);
        _currentCam = newCamera;
        _currentCam.gameObject.SetActive(true);
        
        _batteryManager.OnActivation();
    }

    public void CloseCameras()
    {
        if (display.activeSelf)
        {
            display.SetActive(false);
            _batteryManager.OnDisactivation();
        }
        else
        {
            display.SetActive(true);
            if (_currentCam) _batteryManager.OnActivation();
        }
    }

    public void OnSabotaged()
    {
        display.SetActive(false);//ou afficher qu'il y a un sabotage
        _sabotaged = true;
    }

    public void OnUnsabotaged()
    {
        _sabotaged = false;
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
