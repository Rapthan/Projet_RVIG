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
        //print("aa");
        Instance = this;
        batteryLeft = maxBattery;
    }

    private void Update()
    {
        //print(_isConsumingBattery);
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

    public void AddBattery(float refill)
    {
        batteryLeft = Mathf.Min(batteryLeft + refill, maxBattery);
    }
}
