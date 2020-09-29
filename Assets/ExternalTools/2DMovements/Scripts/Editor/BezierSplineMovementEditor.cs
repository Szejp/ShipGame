using ExternalTools._2DMovements.Scripts.Helpers;
using ExternalTools._2DMovements.Scripts.Movements;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PlatformMovements
{
#pragma warning disable 0618  
    [CustomEditor(typeof(BezierSplineMovement))]
    public class BezierSplineMovementEditor : Editor
    {
        private BezierSplineMovement _script;

        private const float PICK_SIZE = 0.06f;
        private int _selectedIndex = -1;

        private Vector2 _prevPosition;

        private static readonly Color[] ModeColors =
        {
            Color.white,
            Color.yellow,
            Color.cyan
        };

        private void OnSceneGUI()
        {
            _script = target as BezierSplineMovement;

            HandleMovementWithObject();

            Vector3 point0 = ShowPoint(0);
            for (int i = 1; i < _script.ControlPointCount; i += 3)
            {
                Vector2 point1 = ShowPoint(i);
                Vector2 point2 = ShowPoint(i + 1);
                Vector2 point3 = ShowPoint(i + 2);

                Handles.color = Color.gray;
                Handles.DrawLine(point0, point1);
                Handles.DrawLine(point2, point3);

                point0 = point3;
            }
        }

        private void HandleMovementWithObject()
        {
            if (_script.MoveWithGameObject && !Application.isPlaying && _prevPosition != (Vector2) _script.transform.position && _prevPosition != Vector2.zero)
            {
                var difference = (Vector2) _script.transform.position - _prevPosition;
                MoveTargetsWithGameobject(difference);
            }

            _prevPosition = _script.transform.position;
        }

        private void MoveTargetsWithGameobject(Vector2 difference)
        {
            _script.MoveControlPoints(difference);
        }

        private Vector3 ShowPoint(int index)
        {
            Vector3 point = _script.BasePosition + _script.GetControlPoint(index);
            float size = HandleUtility.GetHandleSize(point);

            if (index == 0)
            {
                size *= 2f;
            }

            Handles.color = ModeColors[(int) _script.GetControlPointMode(index)];
            if (Handles.Button(point, Quaternion.identity, size * GizmosData.HANDLE_DEFAULT_SIZE, size * PICK_SIZE, Handles.DotCap))
            {
                _selectedIndex = index;
                Repaint();
            }

            if (_selectedIndex != index)
            {
                return point;
            }

            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, Quaternion.identity);

            if (!EditorGUI.EndChangeCheck())
            {
                return point;
            }

            Undo.RecordObject(_script, "Move Point");
            EditorUtility.SetDirty(_script);
            PrefabUtility.RecordPrefabInstancePropertyModifications(_script);
            EditorSceneManager.MarkSceneDirty(_script.gameObject.scene);
            _script.SetControlPoint(index, point);

            return point;
        }

        public override void OnInspectorGUI()
        {
            _script = target as BezierSplineMovement;

            CommonEditorLogic.SetupGizmos(_script);
            GUILayout.Space(10);
            CommonEditorLogic.SetMovementSpace(_script);
            GUILayout.Space(10);
            _script.MoveWithGameObject = EditorGUILayout.ToggleLeft("Move with game object", _script.MoveWithGameObject);
            GUILayout.Space(10);
            EditorGUI.BeginChangeCheck();
            bool loop = EditorGUILayout.Toggle("Loop", _script.Loop);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_script, "Toggle Loop");
                EditorUtility.SetDirty(_script);
                _script.Loop = loop;
            }

            GUILayout.Space(10);
            SetupMovement();

            GUILayout.Space(10);
            if (_selectedIndex >= 0 && _selectedIndex < _script.ControlPointCount)
            {
                DrawSelectedPointInspector();
            }

            GUILayout.Space(10);
            if (GUILayout.Button("Add point"))
            {
                Undo.RecordObject(_script, "Add point");
                _script.AddCurve();
                EditorUtility.SetDirty(_script);
                _selectedIndex = _script.ControlPointCount - 1;
            }

            GUILayout.Space(10);
            SetupEvents(_script);
        }

        private void SetupMovement()
        {
            _script.MovementDuration = EditorGUILayout.FloatField(
                new GUIContent("Movement duration", "Duration of the movement in seconds"), _script.MovementDuration);
            _script.StartDelay = EditorGUILayout.FloatField(new GUIContent("Start delay", "Idle time before the movement will start"), _script.StartDelay);
            _script.Ease = EditorGUILayout.CurveField(new GUIContent("Ease", "Ease of the movement"), _script.Ease);
            _script.MovementType = (MovementTypes) EditorGUILayout.EnumPopup(new GUIContent("Movement type",
                    "Repetetive: after reaching end will teleport to start and go again. Back and Forth: after reaching end will move to start, and so on. Once: after reaching the end, movement will stop."),
                _script.MovementType);

            if (_script.MovementType != MovementTypes.Once)
            {
                _script.WaitingTime = EditorGUILayout.FloatField(new GUIContent("Waiting time", "A delay between consecutive moves"), _script.WaitingTime);
            }
            else
            {
                _script.WaitingTime = 0;
            }
        }

        private void SetupEvents(BezierSplineMovement script)
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

        private void DrawSelectedPointInspector()
        {
            GUILayout.Label("Selected Point");
            EditorGUI.BeginChangeCheck();
            Vector3 point = EditorGUILayout.Vector3Field("Position", _script.GetControlPoint(_selectedIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_script, "Move Point");
                EditorUtility.SetDirty(_script);
                _script.SetControlPoint(_selectedIndex, point);
            }

            EditorGUI.BeginChangeCheck();
            BezierSplineMovement.BezierControlPointMode mode = (BezierSplineMovement.BezierControlPointMode) EditorGUILayout.EnumPopup("Mode", _script.GetControlPointMode(_selectedIndex));

            if (!EditorGUI.EndChangeCheck())
            {
                return;
            }

            Undo.RecordObject(_script, "Change Point Mode");
            _script.SetControlPointMode(_selectedIndex, mode);
            EditorUtility.SetDirty(_script);
        }
    }
}