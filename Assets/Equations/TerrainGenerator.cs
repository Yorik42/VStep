using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Size of terrain
        int terrainSize = 10;

        //Properties to populate later
        Vector3[] vertices = new Vector3[terrainSize * terrainSize];
        Vector2[] uvs = new Vector2[vertices.Length];
        int size = (terrainSize - 1) * (terrainSize - 1); // sub quad count
        int[] triangles = new int[size * 6]; // 6 indices per quad

        // For each z..
        for (int _z = 0; _z < terrainSize; _z++)
        {
            // ..and each x..
            for (int _x = 0; _x < terrainSize; _x++)
            {
                // ..generate y off equation, then populate vertices and uvs
                float _y = float.Parse((0.3 * Mathf.Sin(3 * Mathf.Sqrt(Mathf.Pow((_x - 5), 2) + Mathf.Pow((_z - 5), 2))) + 0.5 * Mathf.Cos(_x + _z)).ToString());
                vertices[_z * terrainSize + _x] = new Vector3(_x, _y, _z);
                uvs[_z * terrainSize + _x] = new Vector2(_x, _z) / (terrainSize - 1);
            }
        }

        // Populate triangles
        for (int i = 0; i < size; i++)
        {
            triangles[i * 6 + 0] = i;
            triangles[i * 6 + 1] = i + terrainSize;
            triangles[i * 6 + 2] = i + 1;
            triangles[i * 6 + 3] = i + terrainSize;
            triangles[i * 6 + 4] = i + terrainSize + 1;
            triangles[i * 6 + 5] = i + 1;
        }

        // Create mesh
        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.name = "myMesh";

        // Add data
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        // Refine mesh
        mesh.Optimize();
        mesh.RecalculateNormals();

        // Create terrain
        GameObject terrain = new GameObject("Terrain");

        // Add mesh to terrain
        MeshFilter meshFilter = (MeshFilter)terrain.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = mesh;

        // Make it nice and blue
        MeshRenderer renderer = terrain.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material.shader = Shader.Find("Particles/Additive");
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.blue);
        tex.Apply();
        renderer.material.mainTexture = tex;
        renderer.material.color = Color.blue;

        // Add mesh collider
        MeshCollider meshCollider = terrain.AddComponent(typeof(MeshCollider)) as MeshCollider;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
