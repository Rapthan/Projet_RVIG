using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksTrack : MonoBehaviour
{
    [SerializeField] private float xScale;
    [SerializeField] private float yScale;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private RectTransform taskDotPrefab;
    private List<RectTransform> taskDots;
    
    IEnumerator Start()
    {
        yield return null; //on attend que ce soit initialisé
        Dictionary<Task, Vector3> posistions = TaskManager.Instance.TasksPositions();
        foreach (var pos in posistions)
        {
            Vector2 posOnCanvas = new Vector2(xOffset + xScale * pos.Value.z, yOffset + yScale * pos.Value.x);
            RectTransform dot =  Instantiate(taskDotPrefab, transform);
            dot.anchoredPosition = posOnCanvas;
            
            pos.Key.onComplete.AddListener(() => Destroy(dot.gameObject));
        }
    }
}
