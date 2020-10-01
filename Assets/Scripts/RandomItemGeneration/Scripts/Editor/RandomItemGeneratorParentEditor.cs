using RandomItemGeneration;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomItemGenerationParent))]
[CanEditMultipleObjects]
public class RandomItemGenerationParentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RandomItemGenerationParent myScript = (RandomItemGenerationParent) target;
        if (GUILayout.Button("Randomize"))
            myScript.Generate();
    }
}