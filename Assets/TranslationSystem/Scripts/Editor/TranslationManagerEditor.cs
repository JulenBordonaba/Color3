using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TranslationSystem
{
    [CustomEditor(typeof(TranslationManager))]
    public class TranslationManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TranslationManager tm = (TranslationManager)target;

            GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 20, stretchHeight = true, clipping = TextClipping.Overflow, border = new RectOffset() };
            EditorGUILayout.LabelField(tm.currentLenguage, style, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }
    }
}
