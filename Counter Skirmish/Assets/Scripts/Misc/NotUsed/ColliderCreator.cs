/*using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class ColliderCreator : MonoBehaviour
{
    public float _radius = 1f, _height = 2f;

    private MeshCollider _meshCollider;

    private void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        GenerateColliderMesh();
    }

    private void GenerateColliderMesh()
    {
        Mesh mesh = new Mesh();

        // Define vertices for the cone
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(0f, 0f, 0f);
        vertices[1] = new Vector3(_radius, 0f, 0f);
        vertices[2] = new Vector3(0f, _height, 0f);
        vertices[3] = new Vector3(0f, 0f, _radius);

        // Define triangles
        int[] triangles = { 0, 1, 2, 0, 3, 1 };

        // Assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals and bounds
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Assign the mesh to the mesh collider
        _meshCollider.sharedMesh = mesh;
    }
}*/