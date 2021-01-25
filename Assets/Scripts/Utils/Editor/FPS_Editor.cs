using UnityEditor;
using UnityEngine;

public class FPS_Editor<T> : Editor where T : MonoBehaviour
{
    protected T eTarget = default(T);
    protected virtual void OnEnable()
    {
        eTarget = (T)target;
    }
}
