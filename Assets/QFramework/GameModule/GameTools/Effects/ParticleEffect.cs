using UnityEngine;

namespace QFramework.GameModule.GameTools.Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleEffect : Effect
    {
        float duration;

        public override float Duration
        {
            get
            {
                if (duration.Equals(0))
                    duration = GetComponent<ParticleSystem>().main.duration;

                return duration;
            }
        }
    }
}