﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QFramework.Helpers.Procedural {
    [RequireComponent(typeof(MeshFilter))]
    public class UniformCirclePlane : MonoBehaviour
    {
        public int resolution = 4;
        public string path;

        Mesh m;

        static int GetPointIndex(int c, int x)
        {
            if (c < 0) return 0; // In case of center point
            x = x % ((c + 1) * 6); // Make the point index circular
            // Explanation: index = number of points in previous circles + central point + x
            // hence: (0+1+2+...+c)*6+x+1 = ((c/2)*(c+1))*6+x+1 = 3*c*(c+1)+x+1

            return (3 * c * (c + 1) + x + 1);
        }

        [ContextMenu("Generate")]
        public void Generate()
        {
            GetComponent<MeshFilter>().mesh = GenerateCircle(resolution);
        }

#if UNITY_EDITOR

        [ContextMenu("SaveMesh")]
        public void SaveMesh()
        {
            AssetDatabase.CreateAsset(m, path);
            AssetDatabase.SaveAssets();
        }

#endif

        public Mesh GenerateCircle(int res)
        {
            float d = 1f / res;

            var vtc = new List<Vector3>();
            vtc.Add(Vector3.zero); // Start with only center point
            var tris = new List<int>();

            // First pass => build vertices
            for (int circ = 0; circ < res; ++circ)
            {
                float angleStep = (Mathf.PI * 2f) / ((circ + 1) * 6);
                for (int point = 0; point < (circ + 1) * 6; ++point)
                {
                    vtc.Add(new Vector2(
                                Mathf.Cos(angleStep * point),
                                Mathf.Sin(angleStep * point)) * d * (circ + 1));
                }
            }

            // Second pass => connect vertices into triangles
            for (int circ = 0; circ < res; ++circ)
            {
                for (int point = 0, other = 0; point < (circ + 1) * 6; ++point)
                {
                    if (point % (circ + 1) != 0)
                    {
                        // Create 2 triangles
                        tris.Add(GetPointIndex(circ - 1, other + 1));
                        tris.Add(GetPointIndex(circ - 1, other));
                        tris.Add(GetPointIndex(circ, point));
                        tris.Add(GetPointIndex(circ, point));
                        tris.Add(GetPointIndex(circ, point + 1));
                        tris.Add(GetPointIndex(circ - 1, other + 1));
                        ++other;
                    }
                    else
                    {
                        // Create 1 inverse triange
                        tris.Add(GetPointIndex(circ, point));
                        tris.Add(GetPointIndex(circ, point + 1));
                        tris.Add(GetPointIndex(circ - 1, other));
                        // Do not move to the next point in the smaller circle
                    }
                }
            }

            // Create the mesh
            m = new Mesh();
            m.SetVertices(vtc);
            m.SetTriangles(tris, 0);
            m.RecalculateNormals();
            m.UploadMeshData(true);

            return m;
        }
    }
}