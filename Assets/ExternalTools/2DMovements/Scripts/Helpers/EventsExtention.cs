using System;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

namespace ExternalTools._2DMovements.Scripts.Helpers
{
    [Serializable]
    public class EventsExtension
    {
        private const float ZERO_CHECK_TOLERANCE = 0.005f;

        [Space] public bool Enabled;
        [Range(0, 1f)] public float Position;

        [SerializeField] [Tooltip("Fire an event every X triggers")]
        private int _triggersToFire;

        private int _triggersCounter = 0;
        public Color Color;
        [SerializeField] private UnityEvent _event;

        public bool Fired { get; private set; }

        public void Fire()
        {
            if (!Enabled)
            {
                return;
            }

            Fired = true;
            _triggersCounter++;
            if (_triggersCounter < _triggersToFire)
            {
                return;
            }

            _event.Invoke();
            _triggersCounter = 0;
        }

        public EventsExtension()
        {
            Color = Color.red;
            Enabled = true;
            Position = .5f;
        }

        public void Reset()
        {
            Fired = false;
        }

        public bool HasInitialValues()
        {
            return Math.Abs(Position) < ZERO_CHECK_TOLERANCE
                 && Math.Abs(Color.a) < ZERO_CHECK_TOLERANCE
                 && Math.Abs(Color.r) < ZERO_CHECK_TOLERANCE
                 && Math.Abs(Color.g) < ZERO_CHECK_TOLERANCE
                 && Math.Abs(Color.b) < ZERO_CHECK_TOLERANCE
                 && !Enabled;
        }

        public void SetDefaultValues()
        {
            Enabled = true;
            Color = Color.white;
            Position = 0;
        }
    }
}