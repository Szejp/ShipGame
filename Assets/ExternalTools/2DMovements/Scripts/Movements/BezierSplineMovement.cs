using System;
using System.Collections.Generic;
using ExternalTools._2DMovements.Scripts.Helpers;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace ExternalTools._2DMovements.Scripts.Movements
{
    [AddComponentMenu("2D Movements/Bezier movement")]
    public class BezierSplineMovement : BaseMovement
    {
#if (UNITY_EDITOR)
        private const float DIRECTION_SCALE = 0.3f;
        private const int STEPS_PER_CURVE = 1;
#endif

        public enum BezierControlPointMode
        {
            Free,
            Aligned
        }

        public MovementTypes MovementType;

        [SerializeField] private Vector3[] _localPoints;
        [SerializeField] private BezierControlPointMode[] _modes;

        private float _startTime = 0;
        public float MovementDuration = 2;
        public float WaitingTime = 0;
        public AnimationCurve Ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public int ControlPointCount
        {
            get { return _localPoints.Length; }
        }

        [SerializeField] private bool _loop;

        public bool Loop
        {
            get { return _loop; }
            set
            {
                _loop = value;
                if (value != true)
                {
                    return;
                }

                _modes[_modes.Length - 1] = _modes[0];
                SetControlPoint(0, _localPoints[0]);
            }
        }

        private int _stepsCount;
        private int _currentStep = 0;
        private int _waitingSteps = 0;

        private bool _goingBack = false;

        public List<EventsExtension> CustomEvents;

        public bool MoveWithGameObject = true;

        public Vector3 BasePosition
        {
            get
            {
                if (MovementSpace == MovementSpace.Global)
                {
                    return Vector3.zero;
                }

                if (transform.parent != null)
                {
                    return transform.parent.transform.position;
                }

                return Vector3.zero;
            }
        }

        private void Start()
        {
            _startTime = Time.time;
            _stepsCount = (int) (MovementDuration / Time.fixedDeltaTime);
            _waitingSteps = (int) (WaitingTime / Time.fixedDeltaTime);
        }

        private void FixedUpdate()
        {
            if (Time.time - _startTime < StartDelay)
            {
                return;
            }

            if (_currentStep < _stepsCount)
            {
                _currentStep++;
                float percentage = (float) _currentStep / _stepsCount;
                if (MovementType == MovementTypes.BackAndForth && _goingBack)
                {
                    percentage = 1 - percentage;
                }

                float easedProgress = Ease.Evaluate(percentage);

                Vector3 position = BasePosition + GetPoint(easedProgress);
                transform.position = position;

                HandleCustomEvents(percentage);
            }
            else if (_currentStep < _stepsCount + _waitingSteps)
            {
                _currentStep++;
            }
            else
            {
                switch (MovementType)
                {
                    case MovementTypes.Repetitive:
                        _currentStep = 1;
                        break;
                    case MovementTypes.BackAndForth:
                        _goingBack = !_goingBack;
                        _currentStep = 1;
                        break;
                    case MovementTypes.Once:
                        break;
                }

                ClearCustomEvents();
            }
        }

        private void HandleCustomEvents(float percentage)
        {
            foreach (var customEvent in CustomEvents)
            {
                if (customEvent.Fired)
                {
                    continue;
                }

                if (!_goingBack && percentage >= customEvent.Position)
                {
                    customEvent.Fire();
                }
                else if (_goingBack && percentage <= customEvent.Position)
                {
                    customEvent.Fire();
                }
            }
        }

        private void ClearCustomEvents()
        {
            foreach (var customEvent in CustomEvents)
            {
                customEvent.Reset();
            }
        }

        private Vector3 GetPoint(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = _localPoints.Length - 4;
            }
            else
            {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int) t;
                t -= i;
                i *= 3;
            }

            return Bezier.GetPoint(_localPoints[i], _localPoints[i + 1], _localPoints[i + 2], _localPoints[i + 3], t);
        }

        private Vector3 GetVelocity(float t)
        {
            int i;
            if (t >= 1f)
            {
                t = 1f;
                i = _localPoints.Length - 4;
            }
            else
            {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int) t;
                t -= i;
                i *= 3;
            }

            return Bezier.GetFirstDerivative(
                       _localPoints[i], _localPoints[i + 1], _localPoints[i + 2], _localPoints[i + 3], t) -
                   transform.position;
        }

        private Vector3 GetDirection(float t)
        {
            return GetVelocity(t).normalized;
        }

        public void AddCurve()
        {
            Vector3 point = _localPoints[_localPoints.Length - 1];
            Array.Resize(ref _localPoints, _localPoints.Length + 3);
            point.x += 1f;
            _localPoints[_localPoints.Length - 3] = point;
            point.x += 1f;
            _localPoints[_localPoints.Length - 2] = point;
            point.x += 1f;
            _localPoints[_localPoints.Length - 1] = point;

            Array.Resize(ref _modes, _modes.Length + 1);
            _modes[_modes.Length - 1] = _modes[_modes.Length - 2];
            EnforceMode(_localPoints.Length - 4);

            if (!_loop)
            {
                return;
            }

            _localPoints[_localPoints.Length - 1] = _localPoints[0];
            _modes[_modes.Length - 1] = _modes[0];
            EnforceMode(0);
        }

        public void Reset()
        {
            _localPoints = new[]
            {
                new Vector3(1f, 0f, 0f),
                new Vector3(2f, 0f, 0f),
                new Vector3(3f, 0f, 0f),
                new Vector3(4f, 0f, 0f)
            };

            _modes = new[]
            {
                BezierControlPointMode.Free,
                BezierControlPointMode.Free
            };
        }

        private int CurveCount
        {
            get { return (_localPoints.Length - 1) / 3; }
        }

        public Vector3 GetControlPoint(int index)
        {
            return _localPoints[index];
        }

        public void MoveControlPoints(Vector3 movement)
        {
            for (int i = 0; i < _localPoints.Length; i++)
            {
                _localPoints[i] += movement;
            }
        }

        public void SetControlPoint(int index, Vector3 point)
        {
            if (index % 3 == 0)
            {
                Vector3 delta = point - _localPoints[index];
                if (_loop)
                {
                    if (index == 0)
                    {
                        _localPoints[1] += delta;
                        _localPoints[_localPoints.Length - 2] += delta;
                        _localPoints[_localPoints.Length - 1] = point;
                    }
                    else if (index == _localPoints.Length - 1)
                    {
                        _localPoints[0] = point;
                        _localPoints[1] += delta;
                        _localPoints[index - 1] += delta;
                    }
                    else
                    {
                        _localPoints[index - 1] += delta;
                        _localPoints[index + 1] += delta;
                    }
                }
                else
                {
                    if (index > 0)
                    {
                        _localPoints[index - 1] += delta;
                    }

                    if (index + 1 < _localPoints.Length)
                    {
                        _localPoints[index + 1] += delta;
                    }
                }
            }

            _localPoints[index] = point;
            EnforceMode(index);
        }

        public BezierControlPointMode GetControlPointMode(int index)
        {
            return _modes[(index + 1) / 3];
        }

        public void SetControlPointMode(int index, BezierControlPointMode mode)
        {
            int modeIndex = (index + 1) / 3;
            _modes[modeIndex] = mode;
            if (_loop)
            {
                if (modeIndex == 0)
                {
                    _modes[_modes.Length - 1] = mode;
                }
                else if (modeIndex == _modes.Length - 1)
                {
                    _modes[0] = mode;
                }
            }

            EnforceMode(index);
        }

        private void EnforceMode(int index)
        {
            int modeIndex = (index + 1) / 3;
            BezierControlPointMode mode = _modes[modeIndex];
            if (mode == BezierControlPointMode.Free || !_loop && (modeIndex == 0 || modeIndex == _modes.Length - 1))
            {
                return;
            }

            int middleIndex = modeIndex * 3;
            int fixedIndex, enforcedIndex;
            if (index <= middleIndex)
            {
                fixedIndex = middleIndex - 1;
                if (fixedIndex < 0)
                {
                    fixedIndex = _localPoints.Length - 2;
                }

                enforcedIndex = middleIndex + 1;
                if (enforcedIndex >= _localPoints.Length)
                {
                    enforcedIndex = 1;
                }
            }
            else
            {
                fixedIndex = middleIndex + 1;
                if (fixedIndex >= _localPoints.Length)
                {
                    fixedIndex = 1;
                }

                enforcedIndex = middleIndex - 1;
                if (enforcedIndex < 0)
                {
                    enforcedIndex = _localPoints.Length - 2;
                }
            }

            Vector3 middle = _localPoints[middleIndex];
            Vector3 enforcedTangent = middle - _localPoints[fixedIndex];
            if (mode == BezierControlPointMode.Aligned)
            {
                enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, _localPoints[enforcedIndex]);
            }

            _localPoints[enforcedIndex] = middle + enforcedTangent;
        }

        #region Editor code

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!DrawGizmos())
            {
                return;
            }

            DrawPath();
            DrawEvents();
        }

        private bool DrawGizmos()
        {
            if (!ShowGizmos)
            {
                return false;
            }

            if (ShowGizmosIfSelectedInHierarchy)
            {
                if (Selection.Contains(gameObject))
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        private void DrawPath()
        {
            Vector3 point0 = GetPoint(0);
            for (int i = 1; i < ControlPointCount; i += 3)
            {
                Vector2 point1 = GetPoint(i);
                Vector2 point2 = GetPoint(i + 1);
                Vector2 point3 = GetPoint(i + 2);
                Handles.color = Color.gray;
                Handles.DrawBezier(point0, point3, point1, point2, GizmosColor, null, 2f);
                point0 = point3;
            }

            ShowDirections();
        }

        private Vector3 GetPoint(int index)
        {
            Vector3 point = BasePosition + GetControlPoint(index);
            return point;
        }

        private void ShowDirections()
        {
            Handles.color = Color.green;
            Vector3 point = GetPoint(0f);
            Handles.color = GizmosColor;
            Handles.DrawLine(point, point + GetDirection(0f) * DIRECTION_SCALE);
            int steps = STEPS_PER_CURVE * CurveCount;
            for (int i = 1; i <= steps; i++)
            {
                point = GetPoint(i / (float) steps);
                Handles.DrawLine(point, point + GetDirection(i / (float) steps) * DIRECTION_SCALE);
            }
        }

        private void DrawEvents()
        {
            if (CustomEvents == null)
            {
                return;
            }

            foreach (var customEvent in CustomEvents)
            {
                if (!customEvent.Enabled)
                {
                    continue;
                }

                Gizmos.color = customEvent.Color;
                Vector2 point = GetPoint(Ease.Evaluate(customEvent.Position));
                float size = HandleUtility.GetHandleSize(point + (Vector2) BasePosition);
                Gizmos.DrawCube(point + (Vector2) BasePosition, Vector3.one * size * GizmosData.CUSTOM_EVENTS_DEFAULT_SIZE);
            }
        }

        public void SendDebugMessage(string message)
        {
            Debug.Log(message);
        }
#endif

        #endregion
    }
}