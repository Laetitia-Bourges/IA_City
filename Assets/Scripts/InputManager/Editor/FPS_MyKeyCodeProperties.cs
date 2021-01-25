using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(FPS_MyKeyCode))]
public class PFS_MyKeyCodeProperties : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty _keyCodeProperty = property.FindPropertyRelative("key");
        SerializedProperty _findKeyProperty = property.FindPropertyRelative("findKey");

        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label.text));
        Rect _finderBox = new Rect(position.x, position.y, 50, position.height);
        Rect _enumKeyBox = new Rect(position.x + 60, position.y, 150, position.height);

        EditorGUI.PropertyField(_enumKeyBox, _keyCodeProperty, GUIContent.none);
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(_finderBox, _findKeyProperty, GUIContent.none);
        if (EditorGUI.EndChangeCheck())
            _keyCodeProperty.enumValueIndex = FindPositionByKey(_findKeyProperty.stringValue);
        EditorGUI.EndProperty();
    }

    int FindPositionByKey(string _key)
    {
        string[] _allKeys = Enum.GetNames(typeof(KeyCode));
        string _input = _key.ToLower();

        for (int i = 0; i < _allKeys.Length; i++)
        {
            string _currentKey = _allKeys[i].ToLower();
            if (_input.Length == 1 && _currentKey.Equals(_input)) return i;
            else if (_input.Length > 1 && _currentKey.StartsWith(_input)) return i;
        }
        return 0;
    }
}
