using System;
using UnityEngine;

[Serializable]
public class FPS_MyKeyCode 
{
    [SerializeField] KeyCode key = KeyCode.None;
    [SerializeField] string findKey = "";

    public KeyCode Key => key;
    public string FindKey => findKey;
}
