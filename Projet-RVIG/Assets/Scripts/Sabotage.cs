using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Sabotage : MonoBehaviour
{
    public static UnityEvent startSabotaging;
    public static UnityEvent endSabotaging;
    [SerializeField] private float minimalTimeBeforeSabotage;
    [SerializeField] private float maximalTimeBeforeSabotage;
    private IEnumerator _onGoingCoroutine;

    private void Awake()
    {
        if (startSabotaging == null) startSabotaging = new UnityEvent();
        if (endSabotaging == null) endSabotaging = new UnityEvent();
        
        startSabotaging.AddListener(() =>
        {
            if (_onGoingCoroutine != null)
            {
                StopCoroutine(_onGoingCoroutine);
                _onGoingCoroutine = null;
            }
        });

        StartingCorouting();
    }

    public void StartingCorouting()
    {
        float timer = Random.Range(minimalTimeBeforeSabotage, maximalTimeBeforeSabotage);
        _onGoingCoroutine = TimeBeforeActivate(timer);
        StartCoroutine(_onGoingCoroutine);
    }

    private void Activation()
    {
        startSabotaging.Invoke();
    }
    
    public void Disactivation()
    {
        //à appeler par une classe sur l'objet implémentant la tache à faire pour annuler le sabotage
        endSabotaging.Invoke();
        StartingCorouting();
    }

    private IEnumerator TimeBeforeActivate(float timer)
    {
        yield return new WaitForSeconds(timer);
        _onGoingCoroutine = null;
        Activation();
    }
}
