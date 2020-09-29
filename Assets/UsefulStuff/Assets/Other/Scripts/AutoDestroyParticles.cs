using UnityEngine;

namespace UsefulStuff.Assets.Other.Scripts {
    public class AutoDestroyParticles : MonoBehaviour {
        private ParticleSystem ps;


        public void Start() {
            ps = GetComponent<ParticleSystem>();
        }

        public void Update() {
            if (ps) {
                if (!ps.IsAlive()) {
                    Destroy(gameObject);
                }
            }
        }
    }
}