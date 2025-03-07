using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    public static BatteryManager Instance;
    
    private bool _isConsumingBattery;
    public float batteryLeft;
    [SerializeField] private float maxBattery;

    [SerializeField] private TextMeshProUGUI debugText;

    private void Awake()
    {
        Instance = this;
        batteryLeft = maxBattery;
    }

    private void Update()
    {
        if (_isConsumingBattery)
        {
            batteryLeft -= Time.deltaTime;
            if (debugText) debugText.text = batteryLeft.ToString(CultureInfo.InvariantCulture);
        }
    }

    public void OnActivation()
    {
        _isConsumingBattery = true;
    }

    public void OnDisactivation()
    {
        _isConsumingBattery = false;
    }
}
