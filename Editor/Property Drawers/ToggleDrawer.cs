#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using SLIDDES.Attributes;

namespace SLIDDES.Editor
{
    [CustomPropertyDrawer(typeof(ToggleAttribute))]
    public class ToggleDrawer : PropertyDrawer
    {
        // TODO https://answers.unity.com/questions/1471758/create-an-attribute-that-references-another-proper.html

        /// <summary>
        /// Override the GUI
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
#endif