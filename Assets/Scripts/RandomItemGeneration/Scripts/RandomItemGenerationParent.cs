using UnityEngine;

namespace RandomItemGeneration
{
    public class RandomItemGenerationParent : MonoBehaviour
    {
        public void Generate()
        {
            foreach (var generator in GetComponentsInChildren<RandomItemGenerator>())
                if (generator != null && generator.transform.parent == transform && generator.transform.parent == transform)
                    generator.Generate();
        }
    }
}