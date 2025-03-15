using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BatteryManager : MonoBehaviour
{
    public static BatteryManager Instance;
    
    private bool _isConsumingBattery;
    public float batteryLeft;
    [SerializeField] private float maxBattery;
    public UnityEvent batteryEmptied;
    public UnityEvent batteryUnemptied;

    [SerializeField] private TextMeshProUGUI debugText;

    private void Awake()
    {
        //print("aa");
        Instance = this;
        batteryLeft = maxBattery;

        batteryEmptied = new UnityEvent();
        batteryUnemptied = new UnityEvent();
    }

    private void Update()
    {
        //print(_isConsumingBattery);
        if (_isConsumingBattery)
        {
            batteryLeft -= Time.deltaTime;
            if (debugText) debugText.text = batteryLeft.ToString(CultureInfo.InvariantCulture);
            if (batteryLeft <= 0)
            {
                _isConsumingBattery = false;
                batteryEmptied.Invoke();
            }
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
        if (batteryLeft <= 0) batteryUnemptied.Invoke();
        
        batteryLeft = Mathf.Min(batteryLeft + refill, maxBattery);
    }
}
