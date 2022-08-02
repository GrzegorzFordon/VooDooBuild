using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PaperIO
{
    public class MeshCreator : MonoBehaviour
    {

        Mesh mesh;
        MeshCollider meshCollider;

        public int meshesCreatedCounter = 0;
        private void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            meshCollider = GetComponent<MeshCollider>();
        }

        public void CreateMesh(Vector3[] vertices)
        {


            Vector2[] uvs = new Vector2[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                uvs[i] = Vector2.one * (i % 2);
            }

            int[] tris = new int[(vertices.Length - 2) * 3];


            int a = 1;
            int b = 2;
            for (int j = 0; j < tris.Length; j += 3)
            {
                tris[j] = 0;
                tris[j + 1] = a;
                tris[j + 2] = b;

                a++;
                b++;

            }

            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = tris;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.Optimize();

            meshesCreatedCounter++;
            mesh.name = "NewMesh";

            meshCollider.sharedMesh = mesh;
            CanvasManager.instance.TrySetText("Score", mesh.bounds.size.magnitude.ToString("0000"));
        }

    }
}

