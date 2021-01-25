using System;
using UnityEngine;

[Serializable]
public class FPS_Buttons : FPS_Input<ButtonAction, bool>
{
    [SerializeField] ButtonAction action = ButtonAction.None;
    [SerializeField] FPS_MyKeyCode keyCode = null;
    [SerializeField] ButtonState buttonState = ButtonState.Down;
    [SerializeField] bool buttonValue = false;
    public override ButtonAction InputAction => action;
    public override bool InputFeedback => buttonValue;
    public override bool GetInputValue
    {
        get
        {
            if (buttonState == ButtonState.Down)
                return buttonValue = Input.GetKeyDown(keyCode.Key);
            else if (buttonState == ButtonState.Pressed)
                return buttonValue = Input.GetKey(keyCode.Key);
            else return buttonValue = Input.GetKeyUp(keyCode.Key);
        }
    }
}
