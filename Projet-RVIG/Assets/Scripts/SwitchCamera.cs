using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour
{
    /*[SerializeField] private Button button;
    [SerializeField] private List<Camera> cameras;
    private int _currentCamNbr;*/
    
    private Camera _currentCam;
    public static SwitchCamera Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
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
        print(_currentCam);
        print(newCamera);
        if (_currentCam) _currentCam.gameObject.SetActive(false);
        _currentCam = newCamera;
        _currentCam.gameObject.SetActive(true);
    }
}
