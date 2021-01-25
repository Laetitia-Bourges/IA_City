using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class City_UIManager : City_Singleton<City_UIManager>
{
    [SerializeField] Image sliderIcon = null;
    [SerializeField] Slider timeSlider = null;
    [SerializeField] TMP_Text hourText = null;

    float hourValue = 0;

    public bool IsUIValid => sliderIcon && timeSlider && hourText;

    protected override void Awake()
    {
        base.Awake();
        City_GameManager.OnTimer += SetSliderValue;
    }
    private void Update()
    {
        UpdateSlider();
    }

    void SetSliderValue(float _hour, float _minute)
    {
        hourValue = (_hour / 24) + ((_minute / 60) * 0.05f);
        if (_hour < 6 || _hour > 18) sliderIcon.sprite = Resources.Load<Sprite>("moon");
        else sliderIcon.sprite = Resources.Load<Sprite>("sun");
        hourText.text = $"{(int)_hour} : {(int)_minute}";
    }

    void UpdateSlider()
    {
        timeSlider.value = Mathf.Lerp(timeSlider.value, hourValue, Time.deltaTime);
    }
}
