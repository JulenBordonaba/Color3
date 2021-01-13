using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ExtendedSlider))]
public class ExtendedSliderEditor : SliderEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ExtendedSlider extendedSlider = (ExtendedSlider)target;
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("OnPointerEnterEvent"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
