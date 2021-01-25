using UnityEngine;

public abstract class FPS_Input<TAction, TValue>
{
    [SerializeField] protected string inputName = "Input Name";
    [SerializeField] protected bool isVisible = true;
    public abstract TAction InputAction { get; }
    public abstract TValue InputFeedback { get; }
    public abstract TValue GetInputValue { get; }
}
