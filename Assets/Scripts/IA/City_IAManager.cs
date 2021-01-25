﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class City_IAManager : MonoBehaviour
{
    public event Action OnInstantiateIA = null;

    [SerializeField, Range(0, 500)] int nbIA = 5;
    [SerializeField] List<GameObject> IAPrefab = new List<GameObject>();

    List<City_IABehaviour> IABehaviours = new List<City_IABehaviour>();

    bool buildingsAdded = false, gameManagerInstatiate = false;

    public bool IsValid => IAPrefab.Count > 0;

    private void Awake()
    {
        OnInstantiateIA += InstantiateIA;
        Debug.Log("c'est trop nul");
        City_BuildingsManager.OnAllBuildingsAdded += () => { buildingsAdded = true; };
        City_GameManager.OnGameManagerInstantiate += () => { gameManagerInstatiate = true; };
        City_GameManager.OnUpdateSpeed += SetIASpeed;
    }
    private void Update()
    {
        OnInstantiateIA?.Invoke();
    }

    void InstantiateIA()
    {
        if (!buildingsAdded || !gameManagerInstatiate) return;
        OnInstantiateIA -= InstantiateIA;
        for (int i = 0; i < nbIA; i++)
        {
            int _randomIA = UnityEngine.Random.Range(0, IAPrefab.Count);
            City_IABehaviour _ia = Instantiate(IAPrefab[_randomIA], transform.position, transform.rotation).GetComponent<City_IABehaviour>();
            _ia.InitBehaviour();
            _ia.transform.SetParent(transform);
            IABehaviours.Add(_ia);
        }
    }

    void SetIASpeed(float _speedCoeff)
    {
        for (int i = 0; i < IABehaviours.Count; i++)
            IABehaviours[i].Movement.SetVelocity(_speedCoeff);
    }
}
