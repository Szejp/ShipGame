using UnityEngine;

namespace QFramework.Helpers
{
    public static class TargetingHelper
    {
        public static Transform SelectClosestTarget(Transform origin, Transform[] targets)
        {
            Transform selectedTarget = null;
            
            foreach (var t in targets)
            {
                if (selectedTarget.Equals(null) ||
                    Vector3.Distance(origin.position, t.transform.position) <
                    Vector3.Distance(origin.position, selectedTarget.position))
                    selectedTarget = t;
            }

            return selectedTarget;
        }
    }
}