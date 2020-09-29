using ExternalTools._2DMovements.Scripts.Helpers;
using UnityEngine;

namespace ExternalTools._2DMovements.Scripts.Movements
{
    public abstract class BaseMovement : MonoBehaviour
    {
#if (UNITY_EDITOR)
        public bool ShowGizmos = true;
        public bool ShowGizmosIfSelectedInHierarchy = false;
        public Color GizmosColor = Color.white;
#endif

        public float StartDelay;

        public StartingPosition StartingPosition = StartingPosition.ObjectsPosition;

        public MovementSpace MovementSpace = MovementSpace.Global;


        
        /// <summary>
        /// Point from which object will start its movement if "start from custom point" enum is choosen
        /// </summary>
        public Vector2 StartingPointHandlePosition;

        /// <summary>
        /// Initial position that is being set up on start of movement
        /// </summary>
        protected Vector2 InitialPosition
        {
            get
            {
                if (StartingPosition != StartingPosition.ObjectsPosition)
                {
                    return StartingPointHandlePosition;
                }

                switch (MovementSpace)
                {
                    case MovementSpace.Global:
                        return transform.position;
                    case MovementSpace.Local:
                        if (transform.parent != null)
                        {
                            return transform.parent.position + transform.localPosition;
                        }
                        else
                        {
                            return transform.position;
                        }
                    default:
                        Debug.LogError("Undefined movement space! Returning transform.position");
                        return transform.position;
                }
            }
        }

        /////////////////////////
        public void LogMessage(string message)
        {
            Debug.Log(message);
        }
    }
}