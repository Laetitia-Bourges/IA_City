using System;
using System.Collections.Generic;
using UnityEngine;

public class City_GameManager : City_Singleton<City_GameManager>
{
    public static event Action<float, float> OnTimer = null;
    public static event Action OnGameManagerInstantiate = null;
    public static event Action<float> OnUpdateSpeed = null;

    [SerializeField, Range(0.1f, 10)] float daySpeed = 1;
    [SerializeField] Light sunLight = null;
    [SerializeField] float dayTimer = 0;
    [SerializeField] float dayHour = 0;
    bool gameIsStarted = false;

    Color sunColor = UnityColor.DarkBlueDark;

    public bool IsValid => sunLight;

    protected override void Awake()
    {
        base.Awake();
        //OnGameManagerInstantiate?.Invoke();
    }
    private void Start()
    {
        InvokeRepeating("UpdateTimer", 0, 1);
        InvokeRepeating("UpdateVelocity", .5f, .5f);
        sunLight.color = UnityColor.DarkBlueDark;
        OnGameManagerInstantiate?.Invoke();
    }
    private void OnDestroy()
    {
        OnTimer = null;
    }
    private void Update()
    {
        UpdateSunLight();
    }

    void UpdateSunLight()
    {
        sunLight.color = Color.Lerp(sunLight.color, sunColor, Time.deltaTime * 0.02f * daySpeed);
    }

    void UpdateTimer()
    {
        dayTimer += 3 * daySpeed;
        if (dayTimer >= 60)
        {
            dayHour++;
            dayTimer = 0;
            if(dayHour >= 24)
            {
                dayHour = 0;
            }
        }
        SetLightColor();
        OnTimer?.Invoke(dayHour, dayTimer);
    }

    void SetLightColor()
    {
        if (dayHour == 4) sunColor = UnityColor.Orange;
        else if (dayHour == 6)
        {
            sunColor = UnityColor.LightYellow;
            City_LightsManager.Instance?.SetLightStatus(false);
        }
        else if (dayHour == 8) sunColor = Color.white;
        else if (dayHour == 17) sunColor = UnityColor.Strawberry;
        else if (dayHour == 18) sunColor = UnityColor.Purple;
        else if (dayHour == 20)
        {
            sunColor = UnityColor.DarkBlueDark;
            City_LightsManager.Instance?.SetLightStatus(true);
        }
    }

    void UpdateVelocity() => OnUpdateSpeed?.Invoke(daySpeed);
}
