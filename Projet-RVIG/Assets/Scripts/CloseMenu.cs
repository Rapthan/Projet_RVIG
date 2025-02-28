using UnityEngine.UI;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] private GameObject downMenu;
    [SerializeField] private GameObject upDisplay;
    private bool _displayActive;
    [SerializeField] private Button button;

    private void Start()
    {
        _displayActive = upDisplay.activeSelf;
        button.onClick.AddListener(SwitchState);
    }

    public void SwitchState()
    {
        if (downMenu.activeSelf)
        {
            _displayActive = upDisplay.activeSelf;
            upDisplay.SetActive(false);
            downMenu.SetActive(false);
        }
        else
        {
            downMenu.SetActive(true);
            upDisplay.SetActive(_displayActive);
        }
    }
}
