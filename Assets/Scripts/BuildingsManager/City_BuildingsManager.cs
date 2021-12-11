using System;
using System.Collections.Generic;
using UnityEngine;

public class City_BuildingsManager : City_Singleton<City_BuildingsManager>, IHandler<City_BuildingsName, City_Buildings>
{
    public static event Action OnInitBuildings = null;
    public static event Action OnAllBuildingsAdded = null;

    int nbTotalBuildings = 0;
    Dictionary<City_BuildingsName, City_Buildings> handler = new Dictionary<City_BuildingsName, City_Buildings>();
    public Dictionary<City_BuildingsName, City_Buildings> Handler => handler;

    void OnDestroy() => OnInitBuildings = null;
    protected override void Awake()
    {
        base.Awake();
        nbTotalBuildings = FindObjectsOfType<City_Buildings>().Length;
        //OnInitBuildings?.Invoke();
    }

    public void Start()
    {
        OnInitBuildings?.Invoke();
    }

    public void Add(City_Buildings _item)
    {
        if (Exist(_item.ID))
        {
            Debug.Log($"{_item.ID} alredy exist");
            return;
        }
        //Debug.Log($"{_item.ID} added");
        handler.Add(_item.ID, _item);
        if (handler.Count >= nbTotalBuildings) OnAllBuildingsAdded?.Invoke();
    }

    public void Disable(City_BuildingsName _id)
    {
        throw new System.NotImplementedException();
    }

    public void Enable(City_BuildingsName _id)
    {
        throw new System.NotImplementedException();
    }

    public bool Exist(City_BuildingsName _id) => handler.ContainsKey(_id);

    public City_Buildings Get(City_BuildingsName _id)
    {
        if (Exist(_id)) return handler[_id];
        return null;
    }

    public void Remove(City_Buildings _item)
    {
        if (!Exist(_item.ID))
        {
            Debug.Log($"{_item.ID} does not exist");
            return;
        }
        handler.Remove(_item.ID);
    }
}
