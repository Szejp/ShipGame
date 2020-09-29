using ExternalTools._2DMovements.Scripts.Movements;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PlatformMovements
{
    [CustomEditor(typeof(PendulumMovement))]
    public class PendulumMovementEditor : Editor
    {
        private int _eventsCount;

        private void OnSceneGUI()
        {
            BaseMovement script = (BaseMovement) target;
            CommonEditorLogic.SetStartingPointHandle(script);
        }

        public override void OnInspectorGUI()
        {
            PendulumMovement script = (PendulumMovement) target;
            EditorGUI.BeginChangeCheck();
            CommonEditorLogic.SetupGizmos(script);
            GUILayout.Space(10);
            CommonEditorLogic.SetMovementSpace(script);
            GUILayout.Space(10);
            CommonEditorLogic.SetStartingPoint(script);
            GUILayout.Space(10);
            SetupMovements(script);
            GUILayout.Space(10);
            script.KeepFacedToCenter = EditorGUILayout.ToggleLeft(new GUIContent("Keep faced to center", "Rotate object with movement to face center of rotation"), script.KeepFacedToCenter);
            GUILayout.Space(10);
            SetupEvents(script);

            if (EditorGUI.EndChangeCheck())
            {
                PrefabUtility.RecordPrefabInstancePropertyModifications(script);
                EditorUtility.SetDirty(script);
                EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
            }
        }

        private void SetupMovements(PendulumMovement script)
        {
            script.Radius = EditorGUILayout.FloatField("Radius", script.Radius);
            script.Angles = EditorGUILayout.IntSlider("Angles", script.Angles, -359, 359);
            script.MovementDuration = EditorGUILayout.FloatField(new GUIContent("Movement duration", "Duration of a movement"), script.MovementDuration);
            script.WaitingTime = EditorGUILayout.FloatField(new GUIContent("Waiting time", "A delay between consecutive moves"), script.WaitingTime);
            script.StartDelay = EditorGUILayout.FloatField(new GUIContent("Start delay", "Idle time before the movement will start"), script.StartDelay);
        }

        private void SetupEvents(PendulumMovement script)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("CustomEvents"), true);
            serializedObject.ApplyModifiedProperties();

            if (script.CustomEvents == null)
            {
                return;
            }

            foreach (var customEvent in script.CustomEvents)
            {
                if (customEvent.HasInitialValues())
                {
                    customEvent.SetDefaultValues();
                }
            }
        }
    }
}