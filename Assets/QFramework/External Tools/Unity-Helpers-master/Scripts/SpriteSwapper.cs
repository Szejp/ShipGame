using System.Collections.Generic;
using UnityEngine;

namespace QFramework.External_Tools.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSwapper : MonoBehaviour
    {
        public Dictionary<Sprite, Sprite> swapLookup;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void LateUpdate()
        {
            if (swapLookup == null) return;
            if (swapLookup.ContainsKey(spriteRenderer.sprite))
                spriteRenderer.sprite = swapLookup[spriteRenderer.sprite];
        }
    }
}
