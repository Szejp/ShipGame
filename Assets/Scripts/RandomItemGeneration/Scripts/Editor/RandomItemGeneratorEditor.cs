using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomItemGenerator))]
[CanEditMultipleObjects]
public class RandomItemGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RandomItemGenerator generator = (RandomItemGenerator) target;
        generator.enableGeneration = GUILayout.Toggle(generator.enableGeneration, "GenerationEnabled");
        generator.generateInGame = GUILayout.Toggle(generator.generateInGame, "GenerateInGame");

        var itemsListSize = generator.items.Length;
        itemsListSize = EditorGUILayout.IntField("List Size", itemsListSize);

        if (itemsListSize > generator.items.Length)
            generator.items = generator.items.Concat(new GameObject[itemsListSize - generator.items.Length]).ToArray();

        if (itemsListSize < generator.items.Length)
            generator.items = generator.items.Take(itemsListSize).ToArray();

        for (int i = 0; i < generator.items.Length; i++)
        {
            var item = generator.items[i];
            var currentItem = item;

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Select"))
            {
                generator.enableGeneration = true;
                generator.Generate(currentItem);
                generator.enableGeneration = false;
            }

            generator.items[i] = EditorGUILayout.ObjectField(currentItem, typeof(Object), true) as GameObject;
            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Randomize"))
        {
            var generationCached = generator.enableGeneration;
            generator.enableGeneration = true;
            generator.Generate();
            generator.enableGeneration = generationCached;
        }
    }
}