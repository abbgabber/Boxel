using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonGenerator : MonoBehaviour
{
    // List of every vertex of the mesh thats going to be rendered
    public List<Vector3> newVertices = new List<Vector3>();
    // The triangles tells unity how to build each section of the mesh joining the vertices
    public List<int> newTriangles = new List<int>();
    // Tells unity how the texture is aligned on each polygon
    public List<Vector2> newUV = new List<Vector2>();

    // After the vertices, triangles and UVs get saved as this mesh
    private Mesh mesh;

    // Some texture info
    private float tUnit = 0.25f; // The fraction of the width of the texture takes up in the tilesheet
    // Some texture coords
    private Vector2 tStone = new Vector2(0, 0);
    private Vector2 tGrass = new Vector2(0, 1);

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x, y - 1, z));

        newTriangles.Add(0);
        newTriangles.Add(1);
        newTriangles.Add(3);
        newTriangles.Add(1);
        newTriangles.Add(2);
        newTriangles.Add(3);

        newUV.Add(new Vector2(tUnit * tStone.x, tUnit * tStone.y + tUnit));
        newUV.Add(new Vector2(tUnit * tStone.x + tUnit, tUnit * tStone.y + tUnit));
        newUV.Add(new Vector2(tUnit * tStone.x + tUnit, tUnit * tStone.y));
        newUV.Add(new Vector2(tUnit * tStone.x, tUnit * tStone.y));
    }

    void Update()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }
}
