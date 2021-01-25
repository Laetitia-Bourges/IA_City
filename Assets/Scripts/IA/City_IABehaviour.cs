using System.Collections.Generic;
using UnityEngine;

public class City_IABehaviour : MonoBehaviour
{
    [SerializeField] List<City_PlanningCell> planning = new List<City_PlanningCell>();
    [SerializeField] City_IAMovement movement = null;
    int currentPlanningCell = 0;

    public bool IsValid => movement;

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
    public City_IAMovement Movement => movement;

    public void InitBehaviour()
    {
        InitPlanning();
        movement.InitializeMovement();
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
            _currentHour = Random.Range(_currentHour + 2 + (5 - _nbPlanningCells), _currentHour + 6 + (5 - _nbPlanningCells));
            planning.Add(new City_PlanningCell(
                (City_BuildingsName)Random.Range(0, 23),
                _currentHour,
                Random.Range(0, 59)
                ));
        }
    }
    void VerifyPlanning(float _hour, float _minutes)
    {
        if (_hour == CurrentPlanning.Hour && _minutes >= CurrentPlanning.Minutes)
        {
            movement.SetTarget(City_BuildingsManager.Instance.Get(CurrentPlanning.TargetName).BuildingPosition);
            gameObject.SetActive(true);
            CurrentPlanningCell += 1;
        }
    }
}


/*
 * Version FSM : 
 * 
 * Move => vers la target demande
 * SearchNextTarget => set sa target au prochain point
 * Wait => attend l'heure de partir
 */