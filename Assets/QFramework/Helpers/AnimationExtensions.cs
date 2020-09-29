using UnityEngine;

namespace QFramework.Helpers
{
    public class AnimationPlay : MonoBehaviour
    {
        [SerializeField] Animation anim;

        public Animation Anim
        {
            get
            {
                if (anim == null)
                    anim = GetComponent<Animation>();

                return anim;
            }
        } 
        
        public void Play()
        {
            Anim.Play();
        }
    }
}