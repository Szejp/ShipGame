using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ExternalTools._2DMovements.Scripts.Helpers;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace ExternalTools._2DMovements.Scripts.Movements
{
    [AddComponentMenu("2D Movements/Elliptical movement")]
    public class EllipticalMovement : BaseMovement
    {
        public bool Clockwise = true;

        private int Sign
        {
            get { return Clockwise ? -1 : 1; }
        }

        public float MovementDuration = 2;
        public float WaitingTime = 0;
        public Vector2 ElipseParameters = new Vector2(5, 5);
        public int StartAngle = 0;

        private float StartRadians
        {
            get { return 2 * Mathf.PI * StartAngle / 360; }
        }

        public AnimationCurve Ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private int _stepsCount;
        private int _currentStep = 0;
        private int _waitingSteps = 0;

        public bool Rotate = false;
        public float RotationSpeed;
        public bool InvertedRotation = false;

        private int RotationSign
        {
            get { return (InvertedRotation) ? -Sign : Sign; }
        }

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
            _startLocalPosition = SetStartLocalPosition();
            _stepsCount = (int) (MovementDuration / Time.fixedDeltaTime);
            _waitingSteps = (int) (WaitingTime / Time.fixedDeltaTime);
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
            float radians = StartRadians + Mathf.PI / 2;

            if (Time.time - StartDelay < StartDelay)
            {
                transform.position = BasePositionInChoosenSpace + new Vector2((ElipseParameters.x * Mathf.Cos(radians)), (ElipseParameters.y * Mathf.Sin(radians)));
                return;
            }

            if (_currentStep < _stepsCount)
            {
                _currentStep++;
                float angleHelper = Ease.Evaluate((float) _currentStep / _stepsCount);
                float lerpedAngle = Mathf.Lerp(0, 2 * Mathf.PI, angleHelper) * Sign;
                transform.position = BasePositionInChoosenSpace + new Vector2((ElipseParameters.x * Mathf.Cos(radians + lerpedAngle)), (ElipseParameters.y * Mathf.Sin(radians + lerpedAngle)));

                if (Rotate)
                {
                    float rotationStep = RotationSpeed * 360 * Time.fixedDeltaTime / MovementDuration * RotationSign;
                    transform.localEulerAngles += new Vector3(0, 0, rotationStep);
                }

                HandleCustomEvents(angleHelper);
            }
            else if (_currentStep < _stepsCount + _waitingSteps)
            {
                _currentStep++;
                float lerpedAngle = Mathf.Lerp(0, 2 * Mathf.PI, Ease.Evaluate(1)) * Sign;
                transform.position = BasePositionInChoosenSpace + new Vector2((ElipseParameters.x * Mathf.Cos(radians + lerpedAngle)), (ElipseParameters.y * Mathf.Sin(radians + lerpedAngle)));
            }
            else
            {
                _currentStep = 1;

                float lerpedAngle = Mathf.Lerp(0, 2 * Mathf.PI, Ease.Evaluate(0)) * Sign;
                transform.position = BasePositionInChoosenSpace + new Vector2((ElipseParameters.x * Mathf.Cos(radians + lerpedAngle)), (ElipseParameters.y * Mathf.Sin(radians + lerpedAngle)));

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

                if (percentage >= customEvent.Position)
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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!ShowGizmos && (!ShowGizmosIfSelectedInHierarchy || !Selection.Contains(gameObject)))
            {
                return;
            }

            DrawPath();
            DrawCustomEvents();
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private void DrawPath()
        {
            Gizmos.color = GizmosColor;

            var startPoint = (Application.isPlaying) ? BasePositionInChoosenSpace : InitialPosition;

            Vector3[] positions = CreateEllipse(ElipseParameters, startPoint, 500);
            for (int i = 0; i < positions.Length - 2; i++)
            {
                Gizmos.DrawLine(positions[i], positions[i + 1]);
            }

            if (Application.isPlaying)
            {
                return;
            }

            float X = startPoint.x + (ElipseParameters.x * Mathf.Cos(StartRadians + Mathf.PI / 2));
            float Y = startPoint.y + (ElipseParameters.y * Mathf.Sin(StartRadians + Mathf.PI / 2));
            Vector2 startRotationPlace = new Vector2(X, Y);

            float size = HandleUtility.GetHandleSize(startPoint);

            Gizmos.DrawSphere(startRotationPlace, size * GizmosData.GIZMOS_DEFAULT_SIZE);
        }

        private void DrawCustomEvents()
        {
            if (CustomEvents == null)
            {
                return;
            }

            var startPoint = (Application.isPlaying) ? BasePositionInChoosenSpace : InitialPosition;

            foreach (var customEvent in CustomEvents)
            {
                float offset = StartRadians + Mathf.PI / 2 + customEvent.Position * Mathf.PI * 2 * Sign;
                float X = startPoint.x + (ElipseParameters.x * Mathf.Cos(offset));
                float Y = startPoint.y + (ElipseParameters.y * Mathf.Sin(offset));

                Gizmos.color = customEvent.Color;
                float size = HandleUtility.GetHandleSize(startPoint);
                Gizmos.DrawCube(new Vector2(X, Y), Vector3.one * size * GizmosData.CUSTOM_EVENTS_DEFAULT_SIZE);
            }
        }

        Vector3[] CreateEllipse(Vector2 elipseParams, Vector2 elicpseCenter, int resolution)
        {
            Vector3[] positions = new Vector3[resolution + 1];
            Vector3 center = new Vector3(elicpseCenter.x, elicpseCenter.y, 0.0f);

            for (int i = 0; i <= resolution; i++)
            {
                float angle = (float) i / resolution * 2.0f * Mathf.PI;
                positions[i] = new Vector3(elipseParams.x * Mathf.Cos(angle + Mathf.PI / 2), elipseParams.y * Mathf.Sin(angle + Mathf.PI / 2), 0.0f);
                positions[i] = positions[i] + center;
            }

            return positions;
        }
#endif
    }
}