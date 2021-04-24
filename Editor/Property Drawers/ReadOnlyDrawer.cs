using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SLIDDES.Attributes;

namespace SLIDDES.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Override the GUI
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool previousState = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = previousState;
        }
    }
}

// readonly drawer https://www.patrykgalach.com/2020/01/20/readonly-attribute-in-unity-editor/
// GUID connection with runtime scripts https://makaka.org/unity-tutorials/guid (In sliddes.tools.editor set GUID to sliddes.tools