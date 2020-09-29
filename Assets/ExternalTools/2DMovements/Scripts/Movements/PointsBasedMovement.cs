using System.Collections.Generic;
using System.Linq;
using ExternalTools._2DMovements.Scripts.Helpers;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace ExternalTools._2DMovements.Scripts.Movements
{
    [AddComponentMenu("2D Movements/Points based movement")]
    public class PointsBasedMovement : BaseMovement
    {
        public MovementTypes MovementType = MovementTypes.Repetitive;

#if (UNITY_EDITOR)
        private int samples = 1000;
#endif
        [HideInInspector] public List<Vector2> Edges;
        public List<Line> Lines;
        public bool Loop;
        private float _startTime;
        private int _currentLineIndex;
        private int _stepsCount;
        private int _currentStep = 0;
        private int _waitingSteps = 0;
        private bool _goingForth = true;

        public Vector2 StartPosition;
        public Vector2 StartLocalPosition;

        public Vector2 BasePosition
        {
            get
            {
                if (MovementSpace == MovementSpace.Global)
                {
                    return StartPosition;
                }

                if (transform.parent != null)
                {
                    return (Vector2) transform.parent.transform.position + StartLocalPosition;
                }

                return StartPosition;
            }
        }

        private bool _movementFinished;

        private void Start()
        {
            StartLocalPosition = transform.localPosition;
            StartPosition = transform.position;

            if (Lines == null)
            {
                return;
            }

            _startTime = Time.time;
        }

        private void FixedUpdate()
        {
            if (Time.time - _startTime < StartDelay || _movementFinished)
            {
                return;
            }

            var currentLine = DetermineCurrentLine();

            Vector2 startPoint;
            Vector2 targetpoint;

            if (_goingForth)
            {
                startPoint = currentLine.FirstEdge;
                targetpoint = currentLine.SecondEdge;
            }
            else
            {
                startPoint = currentLine.SecondEdge;
                targetpoint = currentLine.FirstEdge;
            }

            if (_currentStep < _stepsCount)
            {
                _currentStep++;
                float percentage = (float) _currentStep / _stepsCount;

                Move(startPoint, targetpoint, currentLine, percentage);
                HandleCustomEvents(currentLine, currentLine.Ease.Evaluate(percentage));
            }
            else if (_currentStep < _stepsCount + _waitingSteps)
            {
                _currentStep++;
            }
            else
            {
                HandleCustomEvents(currentLine, 1);
                ResetCustomEvents(currentLine);

                HandleReachPoint();
            }
        }

        private void Move(Vector2 startPoint, Vector2 targetpoint, Line currentLine, float percentage)
        {
            Vector2 lerpedLinearPosition = Vector2.Lerp(startPoint, targetpoint, currentLine.Ease.Evaluate(percentage));

            Vector2 perpendicularToBaseMovement = GetPerpendicularVector(targetpoint - startPoint).normalized;
            float sineTimeBase = currentLine.Ease.Evaluate(percentage) * 2 * Mathf.PI;
            float sinLean = Mathf.Sin(sineTimeBase * currentLine.Frequency) * currentLine.Amplitude;
            Vector2 sinDelta = new Vector2(perpendicularToBaseMovement.x * sinLean, perpendicularToBaseMovement.y * sinLean);

            transform.position = BasePosition + lerpedLinearPosition + sinDelta;
        }

        private Line DetermineCurrentLine()
        {
            Line currentLine = Lines[_currentLineIndex];
            _stepsCount = (int) ((currentLine.MovementDuration) / Time.fixedDeltaTime);
            _waitingSteps = (int) (currentLine.AfterReachDelay / Time.fixedDeltaTime);
            return currentLine;
        }

        private void ResetCustomEvents(Line line)
        {
            foreach (EventsExtension customEvent in line.CustomEvents)
            {
                customEvent.Reset();
            }
        }

        private void HandleReachPoint()
        {
            var reachedFinalPoint = ReachedFinalPoint();
            if (!reachedFinalPoint)
            {
                _currentLineIndex = DetermineNextTargetPoint(false);
                _currentStep = 1;
                MoveToCurrentLinesStart();
                return;
            }

            switch (MovementType)
            {
                case MovementTypes.Once:
                    _movementFinished = true;
                    break;
                case MovementTypes.Repetitive:
                    transform.position = BasePosition + Lines[0].FirstEdge;
                    _currentLineIndex = 0;
                    _currentStep = 1;
                    break;
                case MovementTypes.BackAndForth:
                    _goingForth = !_goingForth;
                    _currentLineIndex = DetermineNextTargetPoint(true);
                    _currentStep = 1;
                    break;
            }
        }

        private void MoveToCurrentLinesStart()
        {
            if (_goingForth)
            {
                transform.position = BasePosition + Lines[_currentLineIndex].FirstEdge;
            }
            else
            {
                transform.position = BasePosition + Lines[_currentLineIndex].SecondEdge;
            }
        }

        private bool ReachedFinalPoint()
        {
            if (_goingForth)
            {
                return _currentLineIndex == Lines.Count - 1;
            }

            return _currentLineIndex == 0;
        }

        private int DetermineNextTargetPoint(bool reachedFinalPoint)
        {
            if (_goingForth)
            {
                return reachedFinalPoint ? _currentLineIndex : _currentLineIndex + 1;
            }

            return reachedFinalPoint ? _currentLineIndex : _currentLineIndex - 1;
        }

        private Vector2 GetPerpendicularVector(Vector2 vector2)
        {
            return new Vector2(-vector2.y, vector2.x);
        }

        private void HandleCustomEvents(Line line, float percentage)
        {
            if (line.CustomEvents == null)
            {
                return;
            }

            foreach (EventsExtension customEvent in line.CustomEvents)
            {
                float eventsPosition = _goingForth ? customEvent.Position : 1 - customEvent.Position;

                if (!customEvent.Fired && percentage >= eventsPosition)
                {
                    customEvent.Fire();
                }
            }
        }

        #region Managing points

        public Vector2 GetControlPointsPosition(int i)
        {
            return Edges[i];
        }

        public void AddPoint()
        {
            var newEdge = Edges.Last() + new Vector2(2, 0);
            Edges.Add(newEdge);
            if (Loop)
            {
                Line lastLine = Lines.Last();
                Lines.Remove(lastLine);
                Line newLine = new Line(Edges[Edges.Count - 2], Edges.Last());
                Lines.Add(newLine);
                Line loopLine = new Line(newEdge, Edges[0]);
                Lines.Add(loopLine);
            }
            else
            {
                Line newLine = new Line(Edges[Edges.Count - 2], Edges[Edges.Count - 1]);
                Lines.Add(newLine);
            }
        }

        public void RemovePoint(int index)
        {
            Vector2 edge = Edges[index];
            bool addLoop = false;

            if (index == 0 || index == Edges.Count - 1)
            {
                if (Loop)
                {
                    Lines.Remove(Lines.Last());
                    if (index == 0)
                    {
                        Lines.RemoveAt(0);
                    }
                    else
                    {
                        Lines.Remove(Lines.Last());
                    }

                    addLoop = true;
                }
                else
                {
                    if (index == 0)
                    {
                        Lines.RemoveAt(0);
                    }
                    else
                    {
                        Lines.Remove(Lines.Last());
                    }
                }
            }
            else
            {
                Lines.RemoveAt(index);
                Lines.RemoveAt(index - 1);
                Line newLine = new Line(Edges[index - 1], Edges[index + 1]);
                Lines.Insert(index - 1, newLine);
            }

            Edges.Remove(edge);
            if (addLoop)
            {
                AddLoopLine();
            }
        }

        public void MoveLinesWithEdge(int index, Vector2 position)
        {
            if (index == 0)
            {
                Lines[0].FirstEdge = position;
                if (Loop)
                {
                    Lines.Last().SecondEdge = position;
                }
            }
            else if (index == Lines.Count)
            {
                if (Loop)
                {
                    Lines[Lines.Count - 2].SecondEdge = position;
                    Lines.Last().FirstEdge = position;
                }
                else
                {
                    Lines.Last().SecondEdge = position;
                }
            }
            else
            {
                Lines[index - 1].SecondEdge = position;
                Lines[index].FirstEdge = position;
            }
        }

        public void AddLoopLine()
        {
            Line loopLine = new Line(Edges.Last(), Edges.First());
            Lines.Add(loopLine);
        }

        public void SetInitialEdges()
        {
            Edges = new List<Vector2>();
            Lines = new List<Line>();
            Vector2 first = new Vector2(-2, 0);
            Vector2 second = new Vector2(2, 0);

            Line line = new Line(first, second);

            Edges.Add(first);
            Edges.Add(second);
            Lines.Add(line);
        }

        #endregion

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            if (!DrawGizmos())
            {
                return;
            }

            DrawPaths();
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

        private void DrawPaths()
        {
            if (Lines == null || Lines.Count <= 0)
            {
                return;
            }

            Gizmos.color = GizmosColor;
            for (int i = 0; i < Lines.Count; i++)
            {
                Vector2 currentPos = (Vector2) transform.position + Lines[i].FirstEdge;

                if (Application.isPlaying)
                {
                    currentPos = BasePosition + Lines[i].FirstEdge;
                }

                Vector2 baseMovementDirection = Lines[i].SecondEdge - Lines[i].FirstEdge;
                Vector2 perpendicularToBaseMovement = GetPerpendicularVector(baseMovementDirection);
                perpendicularToBaseMovement = perpendicularToBaseMovement.normalized;
                float prevSinLean = 0;
                Vector2 baseMovementStep = baseMovementDirection / samples;

                for (int j = 0; j < samples; j++)
                {
                    float percentage = GetPercentage(j, samples);
                    float sinTimeBase = percentage * 2 * Mathf.PI;
                    float sinLean = Mathf.Sin(sinTimeBase * Lines[i].Frequency) * Lines[i].Amplitude;
                    Vector2 delta = new Vector2(perpendicularToBaseMovement.x * (sinLean - prevSinLean), perpendicularToBaseMovement.y * (sinLean - prevSinLean)) + baseMovementStep;

                    prevSinLean = sinLean;
                    Gizmos.DrawLine(currentPos, currentPos + delta);
                    currentPos += delta;
                }
            }
        }

        private float GetPercentage(int j, int gizmosLinesCounter)
        {
            float a = j;
            float b = gizmosLinesCounter;
            float div = a / b;
            return div;
        }

        private void DrawEvents()
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].CustomEvents == null)
                {
                    continue;
                }

                foreach (EventsExtension customEvent in Lines[i].CustomEvents)
                {
                    Vector2 baseMovementDirection = Lines[i].SecondEdge - Lines[i].FirstEdge;
                    Vector2 perpendicularToBaseMovement = GetPerpendicularVector(baseMovementDirection).normalized;

                    float sinTimeBase = customEvent.Position * 2 * Mathf.PI;
                    float sinLean = Mathf.Sin(sinTimeBase * Lines[i].Frequency) * Lines[i].Amplitude;
                    Vector2 delta = new Vector2(perpendicularToBaseMovement.x * (sinLean), perpendicularToBaseMovement.y * (sinLean));

                    var initialPoint = Application.isPlaying ? BasePosition : (Vector2) transform.position;

                    Vector2 startPosition = Lines[i].FirstEdge + initialPoint;
                    Vector2 endPosition = Lines[i].SecondEdge + initialPoint;
                    Vector2 eventPosition = Vector2.Lerp(startPosition, endPosition, customEvent.Position) + delta;

                    Gizmos.color = customEvent.Color;

                    float size = HandleUtility.GetHandleSize(initialPoint);

                    Gizmos.DrawCube(eventPosition, Vector3.one * size * GizmosData.CUSTOM_EVENTS_DEFAULT_SIZE);
                }
            }
        }
#endif
    }
}