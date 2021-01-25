using System;
using UnityEngine;

[Serializable]
public class City_PlanningCell
{
    [SerializeField] City_BuildingsName target = City_BuildingsName.Building3;
    [SerializeField, Range(0, 24)] float dayHourPlanning = 0;
    [SerializeField, Range(0, 60)] float dayMinutesPlanning = 0;

    public City_BuildingsName TargetName => target;
    public float Hour => dayHourPlanning;
    public float Minutes => dayMinutesPlanning;

    public City_PlanningCell(City_BuildingsName _name, float _hour, float _minute)
    {
        target = _name;
        dayHourPlanning = _hour;
        dayMinutesPlanning = _minute;
    }
}
