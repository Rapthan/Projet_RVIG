using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CameraButton : MonoBehaviour
{
    public Camera associatedCamera;
    [SerializeField] private Button button;
    private UnityEvent<Camera> _activateMyCamera;
    
    private void Start()
    {
        button.onClick.AddListener(Activate);
        _activateMyCamera = new UnityEvent<Camera>();
        print(SwitchCamera.Instance);
        _activateMyCamera.AddListener(SwitchCamera.Instance.ChangeCamera);
    }

    private void Activate()
    {
        _activateMyCamera.Invoke(associatedCamera);
    }
}
