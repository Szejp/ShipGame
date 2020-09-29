using System.Linq;
using UnityEngine;

namespace QFramework.Helpers {
    [CreateAssetMenu(fileName = "TextureToLevelConverter", menuName = "Tools/TextureToLevelConverter")]
    public class TextureToLevelConverter : ScriptableObject
    {
        [SerializeField] Texture2D[] textures;

        public TextureLevelData GetLevel()
        {
            if (textures.Length.Equals(0))
                return null;

            var texture = textures[Random.Range(0, textures.Length)];
            var pixels = texture.GetPixels();

            return new TextureLevelData
            {
                size = new Vector2(texture.width, texture.height),
                values = pixels.Select(p => p.r).ToArray()
            };
        }
    }
}