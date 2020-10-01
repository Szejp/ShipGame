using System.Linq;
using UnityEditor;
using UnityEngine;

public class RandomItemGenerator : MonoBehaviour
{
    public bool enableGeneration = true;
    public bool generateInGame = false;
    public GameObject[] items = new GameObject[] {};

    GameObject spawnedObj;
    
    void OnEnable(){
        Generate();
    }

    public void Generate()
    {
        var item = items[Random.Range(0, items.Length)];
        Generate(item);
    }

    public void Generate(GameObject item)
    {
        if (!enableGeneration || item.Equals(null))
            return;

        if (spawnedObj != null)
            DestroyImmediate(spawnedObj.gameObject);

        foreach (var t in GetComponentsInChildren<Transform>().ToList())
        {
            if (t != null && transform != null && t != transform)
                DestroyImmediate(t.gameObject);
        }

        if (PrefabUtility.GetPrefabParent(item) == null && PrefabUtility.GetPrefabObject(item) != null)
            spawnedObj = PrefabUtility.InstantiatePrefab(item, transform) as GameObject;
        else
            spawnedObj = Instantiate(item, transform);

        spawnedObj.transform.localPosition = Vector3.zero;
    }
}