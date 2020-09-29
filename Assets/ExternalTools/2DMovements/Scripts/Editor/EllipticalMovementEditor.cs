using ExternalTools._2DMovements.Scripts.Movements;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PlatformMovements
{
    [CustomEditor(typeof(EllipticalMovement))]
    public class EllipticalMovementEditor : Editor
    {
        private int _eventsCount;

        private void OnSceneGUI()
        {
            BaseMovement script = (BaseMovement) target;
            CommonEditorLogic.SetStartingPointHandle(script);
        }

        public override void OnInspectorGUI()
        {
            EllipticalMovement script = (EllipticalMovement) target;
            EditorGUI.BeginChangeCheck();
            CommonEditorLogic.SetupGizmos(script);
            GUILayout.Space(10);
            CommonEditorLogic.SetMovementSpace(script);
            GUILayout.Space(10);
            CommonEditorLogic.SetStartingPoint(script);
            GUILayout.Space(10);
            SetupMovements(script);
            GUILayout.Space(10);
            SetupRotation(script);
            GUILayout.Space(10);
            SetupEvents(script);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(script);
                PrefabUtility.RecordPrefabInstancePropertyModifications(script);
                EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
            }
        }

        private void SetupMovements(EllipticalMovement script)
        {
            script.ElipseParameters = EditorGUILayout.Vector2Field(new GUIContent("Ellipse parameters", "Width and height of the ellipse"), script.ElipseParameters);
            script.MovementDuration = EditorGUILayout.FloatField(new GUIContent("Movement duration", "Duration of the movement in seconds"), script.MovementDuration);
            script.StartAngle = EditorGUILayout.IntSlider(new GUIContent("Start angle", "Position on ellipse in degrees from where to start the movement"), script.StartAngle, 0, 359);
            script.Ease = EditorGUILayout.CurveField(new GUIContent("Ease", "Ease of the movement"), script.Ease);
            script.Clockwise = EditorGUILayout.ToggleLeft("Clockwise", script.Clockwise);
            script.WaitingTime = EditorGUILayout.FloatField(new GUIContent("Waiting time", "A delay between consecutive moves"), script.WaitingTime);
            script.StartDelay = EditorGUILayout.FloatField(new GUIContent("Start delay", "Idle time before the movement will start"), script.StartDelay);
        }

        private void SetupRotation(EllipticalMovement script)
        {
            script.Rotate = GUILayout.Toggle(script.Rotate,
                new GUIContent("Rotate", "Should Game Object rotate durning the movement?"));
            if (!script.Rotate)
            {
                return;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.HelpBox("How fast object should rotate. Value 1 means it will do one full spin per one full eplipce.", MessageType.None);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            script.RotationSpeed = EditorGUILayout.FloatField("Rotation speed", script.RotationSpeed);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            script.InvertedRotation = GUILayout.Toggle(script.InvertedRotation, "Inverted rotation");
            GUILayout.EndHorizontal();
        }

        private void SetupEvents(EllipticalMovement script)
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