using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class City_IAManager : MonoBehaviour
{
    public event Action OnInstantiateIA = null;

    [SerializeField, Range(0, 100)] int nbIA = 5;
    [SerializeField] GameObject IAPrefab = null;

    List<City_IABehaviour> IABehaviours = new List<City_IABehaviour>();

    bool buildingsAdded = false, gameManagerInstatiate = false;

    public bool IsValid => IAPrefab;

    private void Awake()
    {
        OnInstantiateIA += InstantiateIA;
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
            City_IABehaviour _ia = Instantiate(IAPrefab, transform.position, transform.rotation).GetComponent<City_IABehaviour>();
            _ia.InitBehaviour();
            _ia.transform.SetParent(transform);
            IABehaviours.Add(_ia);
        }
    }

    void SetIASpeed(float _speedCoeff)
    {
        for (int i = 0; i < IABehaviours.Count; i++)
            IABehaviours[i].SetVelocity(_speedCoeff);
    }
}
