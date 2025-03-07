using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CameraButton : MonoBehaviour
{
    public Camera associatedCamera;
    [SerializeField] private ManualPushButton button;
    private UnityEvent<Camera> _activateMyCamera;
    
    private void Start()
    {
        button.trigger.AddListener(Activate);
        _activateMyCamera = new UnityEvent<Camera>();
        print(SwitchCamera.Instance);
        _activateMyCamera.AddListener(SwitchCamera.Instance.ChangeCamera);
    }

    private void Activate()
    {
        _activateMyCamera.Invoke(associatedCamera);
    }
}
