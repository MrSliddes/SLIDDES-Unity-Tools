#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SLIDDES.Components;
using SLIDDES.Components.Enums;
using System;

namespace SLIDDES.Editor
{
    /// <summary>
    /// Editor script, only used in Unity Editor.
    /// </summary>
    [CustomEditor(typeof(ParallaxScrolling))]
    public class EditorParallaxScrolling : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            ParallaxScrolling script = target as ParallaxScrolling;
            int lineHalfLength = 10;

            // Display scroll direction
            switch(script.scrollingAxis)
            {
                case ParallaxScrollingAxis.horizontal:
                    Handles.color = Handles.xAxisColor;
                    Handles.DrawDottedLine(script.transform.position - Vector3.right * lineHalfLength, script.transform.position + Vector3.right * lineHalfLength, 5);
                    break;
                case ParallaxScrollingAxis.vertical:
                    Handles.color = Handles.yAxisColor;
                    Handles.DrawDottedLine(script.transform.position - Vector3.up * lineHalfLength, script.transform.position + Vector3.up * lineHalfLength, 5);
                    break;
                default:
                    break;
            }
        }

        public override void OnInspectorGUI()
        {
            ParallaxScrolling script = target as ParallaxScrolling;
            
            if(!script.debugUseCustomEditor)
            {
                base.OnInspectorGUI();
                return;
            }

            EditorGUILayout.LabelField("Values", EditorStyles.boldLabel);
            // Scrolling axis
            script.scrollingAxis = (ParallaxScrollingAxis)EditorGUILayout.EnumPopup("Scrolling Axis", script.scrollingAxis);
            // Scrolling speed
            script.scrollingSpeed = EditorGUILayout.FloatField("Scrolling Speed", script.scrollingSpeed);
            // Static scrolling
            EditorGUILayout.LabelField("Static Scrolling", EditorStyles.boldLabel);
            script.isStaticScrolling = EditorGUILayout.Toggle("Is Static Scrolling", script.isStaticScrolling);
            if(script.isStaticScrolling) EditorGUILayout.ObjectField("Target", script.target, typeof(Transform), true);
            // Scrolling objects
            SerializedProperty scroll = new SerializedObject(script).FindProperty("scrollingObjects");
            EditorGUILayout.PropertyField(scroll, true);
            //TODO display editable array

            /*
            if(script.scrollingObjects.Length != 0 && script.scrollingObjects != null)
            {
                for(int i = 0; i < script.scrollingObjects.Length; i++)
                {
                    script.scrollingObjects[i].limitLeft = EditorGUILayout.Vector3Field(script.limitAName, script.scrollingObjects[i].limitLeft);
                }
            }*/

            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
            script.debugUseCustomEditor = EditorGUILayout.Toggle("Use Custom Editor", script.debugUseCustomEditor);
        }

    }
      
}
#endif