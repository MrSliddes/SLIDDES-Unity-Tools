using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SLIDDES.Components;
using SLIDDES.Components.Enums;

namespace SLIDDES.Editor
{
    /// <summary>
    /// Editor script, only used in Unity Editor. Displays FieldOfView values.
    /// </summary>
    [CustomEditor(typeof(FieldOfView))]
    public class EditorFieldOfView : UnityEditor.Editor // Error, cannot be used in build project???
    {
        void OnSceneGUI()
        {
            // Get the FieldOfView script from selected object
            FieldOfView fov = (FieldOfView)target;

            // Based on dimension draw GUI
            Vector3 viewAngleA;
            Vector3 viewAngleB;
            switch(fov.dimension)
            {
                case Dimension.dimension2D:
                    // Draw view radius
                    Handles.color = Color.blue;
                    Handles.DrawWireArc(fov.transform.position, fov.transform.forward, fov.transform.right, 360, fov.viewRadius);

                    // Draw view angle
                    viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
                    viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
                    Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
                    Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

                    // Draw a line to visable targets
                    Handles.color = Color.red;
                    foreach(Transform visibleTarget in fov.visibleTargets)
                    {
                        Handles.DrawLine(fov.transform.position, visibleTarget.position);
                    }
                    break;
                case Dimension.dimension3D:
                    // Draw view radius
                    Handles.color = Color.blue;
                    Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

                    if(fov.debugDraw3DSphere)
                    {
                        Handles.color = new Color(Handles.color.r, Handles.color.g, Handles.color.b, 0.1f);
                        Handles.SphereHandleCap(0, fov.transform.position, fov.transform.rotation, fov.viewRadius * 2, EventType.Repaint);
                        Handles.color = new Color(Handles.color.r, Handles.color.g, Handles.color.b, 1f);
                    }

                    // Draw view angle
                    viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
                    viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
                    Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
                    Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

                    // Draw a line to visable targets
                    Handles.color = Color.red;
                    foreach(Transform visibleTarget in fov.visibleTargets)
                    {
                        Handles.DrawLine(fov.transform.position, visibleTarget.position);
                    }
                    break;
                default: Debug.LogError("switch unassigned");
                    break;
            }            
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = target as FieldOfView;

            //fov.is3D = GUILayout.Toggle(fov.draw3DSphere, "3D sphere");
            if(script.dimension == Dimension.dimension3D)
            {
                script.debugDraw3DSphere = EditorGUILayout.Toggle("Draw 3D Sphere", script.debugDraw3DSphere);
            }

        }
    }
}