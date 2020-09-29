using System.Collections.Generic;
using ExternalTools._2DMovements.Scripts.Helpers;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace ExternalTools._2DMovements.Scripts.Movements
{
    [AddComponentMenu("2D Movements/Pendulum movement")]
    public class PendulumMovement : BaseMovement
    {
        public float Radius = 5;

        [Tooltip("Use different signs to start swinging from different side")] [Range(-359, 359)]
        public int Angles = 90;

        private float Radians
        {
            get { return Angles * Mathf.Deg2Rad / 2; }
        }

        public float MovementDuration = 2;

        private float _startTime = 0;
        public float WaitingTime = 0;

        private AnimationCurve _interpolationCurve;
        private bool _goingForth = true;

        private int _stepsCount;
        private int _currentStep = 0;
        private int _waitingSteps = 0;

        public bool KeepFacedToCenter = false;

        private Vector2 _startLocalPosition;

        private Vector2 BasePositionInChoosenSpace
        {
            get
            {
                if (MovementSpace == MovementSpace.Global)
                {
                    return _startLocalPosition;
                }

                if (transform.parent != null)
                {
                    return (Vector2) transform.parent.position + _startLocalPosition;
                }

                return _startLocalPosition;
            }
        }

        public List<EventsExtension> CustomEvents;

        private void Start()
        {
            _startTime = Time.time;

            _stepsCount = (int) (MovementDuration / Time.fixedDeltaTime);
            _waitingSteps = (int) (WaitingTime / Time.fixedDeltaTime);

            _interpolationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

            _startLocalPosition = SetStartLocalPosition();
        }

        private Vector2 SetStartLocalPosition()
        {
            if (MovementSpace == MovementSpace.Global)
            {
                if (StartingPosition == StartingPosition.ObjectsPosition)
                {
                    return transform.position;
                }

                return StartingPointHandlePosition;
            }

            if (StartingPosition == StartingPosition.ObjectsPosition)
            {
                return transform.localPosition;
            }

            if (transform.parent != null)
            {
                return (StartingPointHandlePosition - (Vector2) transform.parent.position);
            }

            return StartingPointHandlePosition;
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
                int sign = _goingForth ? 1 : -1;
                float lerpAngle = Radians * sign;

                float t = _interpolationCurve.Evaluate((float) _currentStep / _stepsCount);
                float percentage = Mathf.Lerp(lerpAngle, -lerpAngle, t);
                transform.position = BasePositionInChoosenSpace + new Vector2(Mathf.Sin(Mathf.PI + percentage), Mathf.Cos(Mathf.PI + percentage)) * Radius;

                if (KeepFacedToCenter)
                {
                    transform.localEulerAngles = new Vector3(0, 0, -1 * ((percentage) * 360 / (2 * Mathf.PI)));
                }

                HandleCustomEvents(t);
            }
            else if (_currentStep < _stepsCount + _waitingSteps)
            {
                int sign = _goingForth ? 1 : -1;
                float lerpAngle = Radians * sign;
                transform.position = BasePositionInChoosenSpace + new Vector2(Mathf.Sin(Mathf.PI - lerpAngle), Mathf.Cos(Mathf.PI - lerpAngle)) * Radius;
                _currentStep++;
            }
            else
            {
                _goingForth = !_goingForth;
                _currentStep = 1;

                int sign = _goingForth ? 1 : -1;
                float lerpAngle = Radians * sign;
                transform.position = BasePositionInChoosenSpace + new Vector2(Mathf.Sin(Mathf.PI + lerpAngle), Mathf.Cos(Mathf.PI + lerpAngle)) * Radius;

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

                if (_goingForth)
                {
                    if (percentage > customEvent.Position)
                    {
                        customEvent.Fire();
                    }
                }
                else
                {
                    if (percentage > 1 - customEvent.Position)
                    {
                        customEvent.Fire();
                    }
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
            Gizmos.color = GizmosColor;

            Vector2 startingPoint = (Application.isPlaying) ? BasePositionInChoosenSpace : InitialPosition;

            Vector2 startPoint = startingPoint + new Vector2(Mathf.Sin(Mathf.PI + Radians), Mathf.Cos(Mathf.PI + Radians)) * Radius;
            Vector2 endPoint = startingPoint + new Vector2(Mathf.Sin(Mathf.PI - Radians), Mathf.Cos(Mathf.PI - Radians)) * Radius;

            float size = HandleUtility.GetHandleSize(startingPoint);
            Gizmos.DrawSphere(startPoint, size * GizmosData.GIZMOS_DEFAULT_SIZE);
            Gizmos.DrawSphere(endPoint, size * GizmosData.GIZMOS_DEFAULT_SIZE / 1.5f);

            Gizmos.DrawLine(startPoint, startingPoint);
            Gizmos.DrawLine(endPoint, startingPoint);

            Vector2[] midpoints = GenerateMidpoints(100);
            for (int i = 0; i < midpoints.Length - 1; i++)
            {
                Gizmos.DrawLine(midpoints[i], midpoints[i + 1]);
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

                var initialPoint = Application.isPlaying ? BasePositionInChoosenSpace : InitialPosition;
                float percentage = Mathf.Lerp(Radians, -Radians, customEvent.Position);

                Gizmos.color = customEvent.Color;

                var position = initialPoint + new Vector2(Mathf.Sin(Mathf.PI + percentage), Mathf.Cos(Mathf.PI + percentage)) * Radius;
                float size = HandleUtility.GetHandleSize(initialPoint);

                Gizmos.DrawCube(position, Vector3.one * size * GizmosData.CUSTOM_EVENTS_DEFAULT_SIZE);
            }
        }

        private Vector2[] GenerateMidpoints(int samples)
        {
            Vector2[] points = new Vector2[samples];
            float angleInRadiansOfPiece = 2 * Radians / samples;

            int sign = (Radians < 0) ? -1 : 1;

            Vector2 startingPoint = (Application.isPlaying) ? BasePositionInChoosenSpace : InitialPosition;

            for (int i = 0; i < samples; i++)
            {
                Vector2 offset = new Vector2(Mathf.Sin(Mathf.PI - Radians + sign * Mathf.Abs(angleInRadiansOfPiece * i)), Mathf.Cos(Mathf.PI - Radians + sign * Mathf.Abs(angleInRadiansOfPiece * i))) * Radius;
                points[i] = startingPoint + offset;
            }

            return points;
        }
#endif
    }
}