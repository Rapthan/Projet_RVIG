using UnityEngine.UI;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] private GameObject downMenu;
    [SerializeField] private GameObject upDisplay;
    private bool _displayActive;
    [SerializeField] private ManualPushButton button;

    private BatteryManager _batteryManager;

    private void Start()
    {
        _batteryManager = BatteryManager.Instance;
        _displayActive = upDisplay.activeSelf;
        button.trigger.AddListener(SwitchState);
    }

    public void SwitchState()
    {
        if (downMenu.activeSelf)
        {
            _displayActive = upDisplay.activeSelf;
            upDisplay.SetActive(false);
            downMenu.SetActive(false);
            _batteryManager.OnDisactivation();
        }
        else
        {
            downMenu.SetActive(true);
            if (_displayActive)
            {
                upDisplay.SetActive(true);
                _batteryManager.OnActivation();
            }
        }
    }
}
