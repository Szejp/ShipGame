using ExternalTools._2DMovements.Scripts.Helpers;
using ExternalTools._2DMovements.Scripts.Movements;
using UnityEditor;
using UnityEngine;

namespace PlatformMovements
{
    public static class CommonEditorLogic
    {
        public static void SetStartingPoint(BaseMovement script)
        {
            var oldStartingPosition = script.StartingPosition;
            script.StartingPosition = (StartingPosition) EditorGUILayout.EnumPopup(new GUIContent("Starting point", "Poin from which game object will start is't movement"), script.StartingPosition);
            if (oldStartingPosition == StartingPosition.ObjectsPosition && script.StartingPosition != oldStartingPosition)
            {
                script.StartingPointHandlePosition = script.transform.position - Vector3.up * 10;
            }
        }

        public static void SetupGizmos(BaseMovement script)
        {
            script.ShowGizmos = EditorGUILayout.ToggleLeft("Show gizmos", script.ShowGizmos);
            if (script.ShowGizmos)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                script.ShowGizmosIfSelectedInHierarchy =
                    EditorGUILayout.ToggleLeft("Only if selected", script.ShowGizmosIfSelectedInHierarchy);
                GUILayout.EndHorizontal();
            }
            else
            {
                script.ShowGizmosIfSelectedInHierarchy = false;
            }

            if (!script.ShowGizmos)
            {
                return;
            }

            script.GizmosColor = EditorGUILayout.ColorField("Connection line color", script.GizmosColor);
        }
        
        public static void SetStartingPointHandle(BaseMovement script)
        {
            if (script.StartingPosition == StartingPosition.CustomPoint)
            {
                EditorGUI.BeginChangeCheck();

                var customStartingPoint = Handles.PositionHandle(script.StartingPointHandlePosition, Quaternion.identity);
                Handles.Label(script.StartingPointHandlePosition, "Custom starting point");
                if (EditorGUI.EndChangeCheck())
                {
                    script.StartingPointHandlePosition = customStartingPoint;
                }
            }
        }

        public static void SetMovementSpace(BaseMovement script)
        {
            script.MovementSpace = (MovementSpace)EditorGUILayout.EnumPopup("Movement space", script.MovementSpace);
        }
    }
}