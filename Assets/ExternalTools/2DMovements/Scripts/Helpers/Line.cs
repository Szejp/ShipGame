using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExternalTools._2DMovements.Scripts.Helpers
{
    [Serializable]
    public class Line
    {
        private const float ZERO_CHECK_TOLERANCE = 0.005f;

        [HideInInspector] public Vector2 FirstEdge;
        [HideInInspector] public Vector2 SecondEdge;

        public AnimationCurve Ease;
        public float AfterReachDelay;
        public float MovementDuration;
        public int Frequency;
        public float Amplitude;
        public List<EventsExtension> CustomEvents;

        public Line(Vector2 first, Vector2 second)
        {
            FirstEdge = first;
            SecondEdge = second;
        }

        public bool HasInitialValues()
        {
            return Math.Abs(MovementDuration) < ZERO_CHECK_TOLERANCE && (Ease == null || Ease.length == 0);
        }
    }
}