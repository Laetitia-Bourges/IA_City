using UnityEngine;

public class City_LightsManager : City_Singleton<City_LightsManager>
{
    [SerializeField] GameObject lightsContent = null;
    Light[] lights = new Light[0];

    void Start()
    {
        GetLights();
    }
    void GetLights()
    {
        lights = lightsContent.GetComponentsInChildren<Light>();
    }
    public void SetLightStatus(bool _value)
    {
        for (int i = 0; i < lights.Length; i++)
            lights[i].enabled = _value;
    }
}
