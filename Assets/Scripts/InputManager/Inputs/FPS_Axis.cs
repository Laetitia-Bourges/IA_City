using System;
using UnityEngine;

[Serializable]
public class FPS_Axis : FPS_Input<AxisAction, float>
{
    [SerializeField] AxisAction action = AxisAction.None;
    [SerializeField] float axisValue = 0;
    public override AxisAction InputAction => action;
    public override float InputFeedback => axisValue;
    public override float GetInputValue 
    {
        get
        {
            try
            {
                return Input.GetAxis(inputName);
            }
            catch (ArgumentException _exception)
            {
                Debug.LogError($"Error Axis Input : {_exception.Message}");
                return 0;
            }
        }
    }
}
