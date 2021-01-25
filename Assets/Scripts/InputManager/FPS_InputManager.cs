using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FPS_InputManager : City_Singleton<FPS_InputManager>
{
    #region event
    public event Action OnUpdateInputs = null;
    #endregion

    #region F/P
    [SerializeField] List<FPS_Axis> axis = new List<FPS_Axis>();
    [SerializeField] List<FPS_Buttons> buttons = new List<FPS_Buttons>();
    #endregion

    #region UnityMethods
    private void Update() => OnUpdateInputs?.Invoke();
    private void OnDestroy() => OnUpdateInputs = null;
    #endregion

    #region Others Methods
    public void RegisterAxis(AxisAction _action, Action<float> _callback)
    {
        List<FPS_Axis> _axis = axis.Where(a => a.InputAction == _action).ToList();
        _axis.ForEach(a => OnUpdateInputs += () => _callback.Invoke(a.GetInputValue));

    }
    public void RegisterButton(ButtonAction _action, Action<bool> _callback)
    {
        List<FPS_Buttons> _buttons = buttons.Where(b => b.InputAction == _action).ToList();
        _buttons.ForEach(b => OnUpdateInputs += () => _callback.Invoke(b.GetInputValue));
    }
    public void UnRegisterAxis(AxisAction _action, Action<float> _callback)
    {
        List<FPS_Axis> _axis = axis.Where(a => a.InputAction == _action).ToList();
        _axis.ForEach(a => OnUpdateInputs -= () => _callback.Invoke(a.GetInputValue));
    }
    public void UnRegisterButton(ButtonAction _action, Action<bool> _callback)
    {
        List<FPS_Buttons> _buttons = buttons.Where(b => b.InputAction == _action).ToList();
        _buttons.ForEach(b => OnUpdateInputs -= () => _callback.Invoke(b.GetInputValue));
    }

    public void AddAxis() => axis.Add(new FPS_Axis());
    public void RemoveAxis(int _index) => axis.RemoveAt(_index);
    public void AddButton() => buttons.Add(new FPS_Buttons());
    public void RemoveButton(int _index) => buttons.RemoveAt(_index);
    public FPS_Axis GetAxis(int _i) => axis[_i];
    public FPS_Buttons GetButton(int _i) => buttons[_i];
    #endregion
}
