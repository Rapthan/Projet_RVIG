using System;
using UnityEngine;

public class MapTracker : MonoBehaviour
{
    [SerializeField] private float xScale;
    [SerializeField] private float yScale;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private RectTransform thisTransform;
    [SerializeField] private Transform player;

    private void Awake()
    {
        thisTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        thisTransform.anchoredPosition = new Vector2(xOffset + xScale * player.position.z, yOffset + yScale * player.position.x);
    }
}
