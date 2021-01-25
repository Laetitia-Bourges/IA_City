using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class City_IABehaviour : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] List<City_PlanningCell> planning = new List<City_PlanningCell>();
    Vector3 target = Vector3.zero;
    float initialSpeed = 0, initialAccel = 0;
    int currentPlanningCell = 0;

    public City_PlanningCell CurrentPlanning => planning[CurrentPlanningCell];
    public int CurrentPlanningCell
    {
        get => currentPlanningCell;
        set
        {
            currentPlanningCell = value;
            currentPlanningCell %= planning.Count;
        }
    }
    public bool IsValid => agent && target != Vector3.zero;
    public bool IsAtPosition 
    {
        get
        {
            Vector3 _targetPos = new Vector3(target.x, 0, target.z);
            Vector3 _myPos = new Vector3(transform.position.x, 0, transform.position.z);
            return Vector3.Distance(_myPos, _targetPos) < 2;
        }
    }
    void MoveTo()
    {
        SetDestination(IsValid ? target : transform.position);
        if (IsAtPosition)
        {
            gameObject.SetActive(false);
        }
    }

    void SetDestination(Vector3 _targetPosition)
    {
        if (!IsValid || !isActiveAndEnabled) return;
        agent.SetDestination(_targetPosition);
    }


    void VerifyPlanning(float _hour, float _minutes)
    {
        if (_hour == CurrentPlanning.Hour && _minutes >= CurrentPlanning.Minutes)
        {
            target = City_BuildingsManager.Instance.Get(CurrentPlanning.TargetName).BuildingPosition;
            gameObject.SetActive(true);
            CurrentPlanningCell += 1;
        }
    }

    public void InitBehaviour()
    {
        InitPlanning();
        initialSpeed = agent.speed;
        initialAccel = agent.acceleration;
        InvokeRepeating("MoveTo",  Random.Range(0f, .5f), .5f);
        City_GameManager.OnTimer += VerifyPlanning;
        transform.position = City_BuildingsManager.Instance.Get(planning[planning.Count - 1].TargetName).BuildingPosition;
        gameObject.SetActive(false);
    }

    void InitPlanning()
    {
        int _nbPlanningCells = Random.Range(3, 5);
        int _currentHour = 0;
        for (int i = 0; i < _nbPlanningCells; i++)
        {
            _currentHour = Random.Range(_currentHour + 2 + (5 - _nbPlanningCells), _currentHour + 6 + (5-_nbPlanningCells));
            planning.Add(new City_PlanningCell(
                (City_BuildingsName)Random.Range(0, 23),
                _currentHour, 
                Random.Range(0, 59)
                ));
        }
    }

    public void SetVelocity(float _speedCoef)
    {
        agent.speed = initialSpeed * _speedCoef;
        agent.acceleration = initialAccel * _speedCoef;
    }
}


/*
 * Version FSM : 
 * 
 * Move => vers la target demande
 * SearchNextTarget => set sa target au prochain point
 * Wait => attend l'heure de partir
 */