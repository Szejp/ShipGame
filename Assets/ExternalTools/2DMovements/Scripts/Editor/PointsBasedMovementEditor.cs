using ExternalTools._2DMovements.Scripts.Helpers;
using ExternalTools._2DMovements.Scripts.Movements;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace PlatformMovements
{
    [CustomEditor(typeof(PointsBasedMovement))]
#pragma warning disable 0618
    public class PointsBasedMovementEditor : Editor
    {
        private PointsBasedMovement _script;

        private const float PICK_SIZE = 0.06f;
        private int _selectedIndex = -1;

        private void OnSceneGUI()
        {
            _script = target as PointsBasedMovement;

            if (_script.Edges == null || _script.Edges.Count < 2)
            {
                _script.SetInitialEdges();
            }

            for (int i = 0; i < _script.Edges.Count; i++)
            {
                ShowPoint(i);
            }
        }

        private void ShowPoint(int i)
        {
            Vector2 point;
            var referencePosition = (!Application.isPlaying) ? (Vector2) _script.transform.position : _script.BasePosition;
            point = referencePosition + _script.Edges[i];

            float size = HandleUtility.GetHandleSize(point);

            if (i == 0)
            {
                size *= 1.5f;
            }

            Handles.color = Color.white;
            if (Handles.Button(point, Quaternion.identity, size * GizmosData.HANDLE_DEFAULT_SIZE, size * PICK_SIZE, Handles.DotCap))
            {
                _selectedIndex = i;
                Repaint();
            }

            if (_selectedIndex != i)
            {
                return;
            }

            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, Quaternion.identity);
            if (!EditorGUI.EndChangeCheck())
            {
                return;
            }

            Undo.RecordObject(_script, "Move Point");
            EditorUtility.SetDirty(_script);
            PrefabUtility.RecordPrefabInstancePropertyModifications(_script);
            EditorSceneManager.MarkSceneDirty(_script.gameObject.scene);

            var pointMinusPosition = point - referencePosition;

            _script.Edges[i] = pointMinusPosition;
            _script.MoveLinesWithEdge(i, pointMinusPosition);
        }

        public override void OnInspectorGUI()
        {
            _script = target as PointsBasedMovement;

            CommonEditorLogic.SetupGizmos(_script);
            GUILayout.Space(10);
            CommonEditorLogic.SetMovementSpace(_script);
            GUILayout.Space(10);
            SetupMovements();
            GUILayout.Space(10);
            SetupLines();

            EditorGUI.BeginChangeCheck();

            GUILayout.Space(20);
            if (GUILayout.Button("Add point"))
            {
                Undo.RecordObject(_script, "Add point");
                _script.AddPoint();
                EditorUtility.SetDirty(_script);
            }

            if (_selectedIndex >= 0 && _selectedIndex < _script.Edges.Count)
            {
                DrawSelectedPointInspector();
            }

            if (!_script.Loop)
            {
                if (GUILayout.Button("Connect ends"))
                {
                    _script.Loop = true;
                    _script.AddLoopLine();
                }
            }
            else
            {
                if (GUILayout.Button("Disconnect ends"))
                {
                    _script.Loop = false;
                    _script.Lines.RemoveAt(_script.Lines.Count - 1);
                }
            }
        }

        private void SetupMovements()
        {
            _script.MovementType = (MovementTypes) EditorGUILayout.EnumPopup(
                new GUIContent("Movement type",
                    "Repetetive: after reaching end will teleport to start and go again. Back and Forth: after reaching end will move to start, and so on. Once: after reaching the end, movement will stop."),
                _script.MovementType);
            _script.StartDelay = EditorGUILayout.FloatField(new GUIContent("Start delay", "Idle time before the movement will start"), _script.StartDelay);
        }

        private void SetupLines()
        {
            for (int i = 0; i < _script.Lines.Count; i++)
            {
                if (_script.Lines[i].HasInitialValues())
                {
                    _script.Lines[i].Ease = AnimationCurve.EaseInOut(0, 0, 1, 1);
                    _script.Lines[i].MovementDuration = 1;
                }

                EditorGUILayout.LabelField("Line " + i);
                _script.Lines[i].Ease = EditorGUILayout.CurveField(new GUIContent("Ease", "Ease of the movement"), _script.Lines[i].Ease == null ? new AnimationCurve() : _script.Lines[i].Ease);
                _script.Lines[i].AfterReachDelay = EditorGUILayout.FloatField(new GUIContent("Delay after reach", "Time that moveing object will be idle after reaching point"), _script.Lines[i].AfterReachDelay);
                _script.Lines[i].MovementDuration = EditorGUILayout.FloatField(new GUIContent("Movement duration", "Duration of the movement in seconds"), _script.Lines[i].MovementDuration);
                _script.Lines[i].Frequency = EditorGUILayout.IntField(new GUIContent("Frequency", "Frequency of sinusoidal movement"), _script.Lines[i].Frequency);
                _script.Lines[i].Amplitude = EditorGUILayout.FloatField(new GUIContent("Amplitude", "Amplitude of sinusoidal movement"), _script.Lines[i].Amplitude);

                EditorGUILayout.PropertyField(serializedObject.FindProperty("Lines.Array.data[" + i + "].CustomEvents"), true);
                serializedObject.ApplyModifiedProperties();

                if (_script.Lines[i].CustomEvents == null)
                {
                    continue;
                }

                foreach (var customEvent in _script.Lines[i].CustomEvents)
                {
                    if (customEvent.HasInitialValues())
                    {
                        customEvent.SetDefaultValues();
                    }
                }
            }
        }

        private void DrawSelectedPointInspector()
        {
            GUILayout.Label("Selected Point");
            EditorGUI.BeginChangeCheck();
            Vector2 point = EditorGUILayout.Vector2Field("Position", _script.GetControlPointsPosition(_selectedIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_script, "Move Point");
                EditorUtility.SetDirty(_script);
                _script.Edges[_selectedIndex] = point;
                _script.MoveLinesWithEdge(_selectedIndex, point);
            }

            if (GUILayout.Button("Remove selected point"))
            {
                Undo.RecordObject(_script, "Remove point");
                _script.RemovePoint(_selectedIndex);
                EditorUtility.SetDirty(_script);
            }
        }
    }
}