using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(ToggleGroupButton))]
public class ToggleGroupButtonEditor : ToggleEditor
{
    SerializedProperty FullSprite;
    SerializedProperty PartialSprite;
    SerializedProperty EmptySprite;
    SerializedProperty CheckImage;
    SerializedProperty BackgroundImage;
    SerializedProperty Label;
    SerializedProperty Content;
    protected override void OnEnable()
    {
        base.OnEnable();
        Label = serializedObject.FindProperty("Label");
        Content = serializedObject.FindProperty("Content");
        FullSprite = serializedObject.FindProperty("FullSprite");
        PartialSprite = serializedObject.FindProperty("PartialSprite");
        EmptySprite = serializedObject.FindProperty("EmptySprite");
        CheckImage = serializedObject.FindProperty("CheckImage");
        BackgroundImage = serializedObject.FindProperty("BackgroundImage");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(FullSprite, new GUIContent("FullSprite"));
        EditorGUILayout.PropertyField(PartialSprite, new GUIContent("PartialSprite"));
        EditorGUILayout.PropertyField(EmptySprite, new GUIContent("EmptySprite"));
        EditorGUILayout.PropertyField(CheckImage, new GUIContent("CheckImage"));
        EditorGUILayout.PropertyField(BackgroundImage, new GUIContent("BackgroundImage"));
        EditorGUILayout.PropertyField(Label, new GUIContent("Label"));
        EditorGUILayout.PropertyField(Content, new GUIContent("Content"));
        serializedObject.ApplyModifiedProperties();
    }
}