using UnityEngine;

namespace QFramework.Helpers.UI.Views
{
    public class ParticleEnablerView : View
    {
        [SerializeField] ParticleSystem[] particleSystems;
        
        public override bool CanShow { get; }
        public override void Show()
        {
            foreach (var p in particleSystems)
                p.Play();
        }

        public override void Hide()
        {
            foreach (var p in particleSystems)
                p.Stop();
        }
    }
}