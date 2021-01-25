using UnityEditor;
using UnityEngine;
using System;

public static class FPS_EditorTool
{
    public static void CreateButton(string _buttonName, Action _callback, Color _textColor, Color _backgroundColor, int _textSize = 12, FontStyle _fontStyle = FontStyle.Bold, TextAnchor _alignement = TextAnchor.MiddleCenter)
    {
        if (GUILayout.Button(_buttonName, ButtonStyle(_textColor, _backgroundColor, _textSize, _fontStyle, _alignement)))
            _callback?.Invoke();
    }
    public static void CreateButtonConfirmation(string _buttonName, string _msgBox, string _windowName, Action _callback, Color _textColor, Color _backgroundColor, string _yesMsg = "Confirm", string _noMsg = "Cancel", int _textSize = 12, FontStyle _fontStyle = FontStyle.Bold, TextAnchor _alignement = TextAnchor.MiddleCenter)
    {
        if (GUILayout.Button(_buttonName, ButtonStyle(_textColor, _backgroundColor, _textSize, _fontStyle, _alignement)))
        {
            bool _validation = EditorUtility.DisplayDialog(_windowName, _msgBox, _yesMsg, _noMsg);
            if (_validation) _callback?.Invoke();
        }
    }
    public static bool Foldout(SerializedProperty _isVisible, string _label, GUIStyle _style = null)
    {
        if (_style != null)
            return _isVisible.boolValue = EditorGUILayout.Foldout(_isVisible.boolValue, _label, true, _style);
        else return _isVisible.boolValue = EditorGUILayout.Foldout(_isVisible.boolValue, _label, true);
    }
    public static void TextField(SerializedProperty _stringValue) => _stringValue.stringValue = EditorGUILayout.TextField(_stringValue.stringValue);
    public static void Space(int _nbSpace = 1)
    {
        for (int i = 0; i < _nbSpace; i++)
            EditorGUILayout.Space();
    }
    public static GUIStyle ButtonStyle(Color _textColor, Color _backGroundColor, int _textSize, FontStyle _fontStyle, TextAnchor _alignement)
    {
        GUIStyle _style = new GUIStyle();
        _style.normal.textColor = _textColor;
        _style.normal.background = GetTexture(_backGroundColor);
        _style.fontSize = _textSize;
        _style.fontStyle = _fontStyle;
        _style.alignment = _alignement;
        return _style;
    }
    public static Texture2D GetTexture(Color _color)
    {
        Texture2D _texture = new Texture2D(1, 1);
        _texture.SetPixel(1, 1, _color);
        _texture.Apply();
        return _texture;
    }
}
