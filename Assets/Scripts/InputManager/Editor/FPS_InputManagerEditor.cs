using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FPS_InputManager))]
public class FPS_InputManagerEditor : FPS_Editor<FPS_InputManager>
{
    SerializedProperty axisListProperty = null, buttonsListProperty = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        axisListProperty = serializedObject.FindProperty("axis");
        buttonsListProperty = serializedObject.FindProperty("buttons");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawAllAxis();
        FPS_EditorTool.Space(3);
        DrawAllButtons();
        serializedObject.ApplyModifiedProperties();
    }

    void DrawAllAxis()
    {
        FPS_EditorTool.CreateButton("Add Axis", eTarget.AddAxis, Color.white, UnityColor.Lagon);
        for (int i = 0; i < axisListProperty.arraySize; i++)
            DrawAxis(i);
    }
    void DrawAxis(int _index)
    {
        SerializedProperty _axis = axisListProperty.GetArrayElementAtIndex(_index);
        SerializedProperty _axisName = _axis.FindPropertyRelative("inputName");
        SerializedProperty _axisAction = _axis.FindPropertyRelative("action");
        SerializedProperty _axisValue = _axis.FindPropertyRelative("axisValue");
        SerializedProperty _isAxisVisible = _axis.FindPropertyRelative("isVisible");

        EditorGUILayout.BeginHorizontal();
        FPS_EditorTool.Foldout(_isAxisVisible, $"{_axisName.stringValue}");
        FPS_EditorTool.CreateButtonConfirmation("X", $"You are going to delete {_axisName.stringValue}", "Delete Axis", () => eTarget.RemoveAxis(_index), Color.white, UnityColor.Strawberry);
        EditorGUILayout.EndHorizontal();

        if (_isAxisVisible.boolValue)
        {
            FPS_EditorTool.TextField(_axisName);
            EditorGUILayout.PropertyField(_axisAction, new GUIContent("Axis Action : "));
            EditorGUILayout.Slider("Feedback : ", _axisValue.floatValue, -1, 1);
        }

        FPS_EditorTool.Space(2);
    }
    void DrawAllButtons()
    {
        FPS_EditorTool.CreateButton("Add Button", eTarget.AddButton, Color.white, UnityColor.Orange);
        for (int i = 0; i < buttonsListProperty.arraySize; i++)
            DrawButton(i);
    }
    void DrawButton(int _index)
    {
        SerializedProperty _button = buttonsListProperty.GetArrayElementAtIndex(_index);
        SerializedProperty _buttonName = _button.FindPropertyRelative("inputName");
        SerializedProperty _buttonAction = _button.FindPropertyRelative("action");
        SerializedProperty _buttonKey = _button.FindPropertyRelative("keyCode");
        SerializedProperty _buttonState = _button.FindPropertyRelative("buttonState");
        SerializedProperty _buttonValue = _button.FindPropertyRelative("buttonValue");
        SerializedProperty _isButtonVisible = _button.FindPropertyRelative("isVisible");

        EditorGUILayout.BeginHorizontal();
        FPS_EditorTool.Foldout(_isButtonVisible, $"{_buttonName.stringValue}");
        FPS_EditorTool.CreateButtonConfirmation("X", $"You are going to delete {_buttonName.stringValue}", "Delete Axis", () => eTarget.RemoveButton(_index), Color.white, UnityColor.Strawberry);
        EditorGUILayout.EndHorizontal();

        if (_isButtonVisible.boolValue)
        {
            FPS_EditorTool.TextField(_buttonName);
            EditorGUILayout.PropertyField(_buttonKey, new GUIContent("Button KeyCode : "));
            EditorGUILayout.PropertyField(_buttonAction, new GUIContent("Button Action : "));
            EditorGUILayout.PropertyField(_buttonState, new GUIContent("Button State : "));
            EditorGUILayout.Toggle("Feedback : ", _buttonValue.boolValue);
        }

        FPS_EditorTool.Space(2);
    }
}
