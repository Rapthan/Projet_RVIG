using System;
using UnityEngine;
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else Destroy(this);
        
        /*button.onClick.AddListener(ChangeCameras);
        _currentCamNbr = 0;*/
    }

    /*private void ChangeCameras()
    {
        cameras[_currentCamNbr].gameObject.SetActive(false);
        _currentCamNbr = (_currentCamNbr == cameras.Count - 1) ? 0 : _currentCamNbr + 1;
        cameras[_currentCamNbr].gameObject.SetActive(true);
    }*/

    public void ChangeCamera(Camera newCamera)
    {
        if (!display.activeSelf) display.SetActive(true);
        if (_currentCam) _currentCam.gameObject.SetActive(false);
        _currentCam = newCamera;
        _currentCam.gameObject.SetActive(true);
    }

    public void CloseCameras()
    {
        display.SetActive(false);
    }
}
