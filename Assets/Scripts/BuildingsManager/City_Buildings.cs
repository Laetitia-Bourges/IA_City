using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Buildings : MonoBehaviour, IHandlerItem<City_BuildingsName>
{
    [SerializeField] City_BuildingsName id = City_BuildingsName.Buildings1;
    public City_BuildingsName ID => id;

    public Vector3 BuildingPosition => transform.position;
    // Horraires d'ouverture

    void Awake() 
    {
        City_BuildingsManager.OnInitBuildings += InitHandlerItem;
    }
    void OnDestroy() => RemoveHandlerItem();

    public void Disable()
    {
        throw new System.NotImplementedException();
    }

    public void Enable()
    {
        throw new System.NotImplementedException();
    }

    public void InitHandlerItem()
    {
        City_BuildingsManager.Instance?.Add(this);
    }

    public void RemoveHandlerItem()
    {
        City_BuildingsManager.Instance?.Remove(this);
    }
}
