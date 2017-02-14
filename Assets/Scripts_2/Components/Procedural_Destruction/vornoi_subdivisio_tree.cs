using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vornoi_subdivisio_tree : MonoBehaviour {

    public GameObject object_to_destroy;
    public int tree_width;
    public int tree_depth;

    // Use this for initialization
    void Start() {
        Mesh mesh_to_split = object_to_destroy.GetComponent<MeshFilter>().mesh;
    }

    Vector3 Get_Center_Of_Mesh(Mesh _mesh)
    {
        Vector3 center = Vector3.zero;
        return center;
    }

    List<Vector3> Generate_Points(Vector3[] _verts, Vector3 _center)
    {
        List<Vector3> points = new List<Vector3>();
        return points;
    }

    List<GameObject> Generate_Planes(List<Vector3> _points)
    {
        List<GameObject> planes = new List<GameObject>();
        return planes;
    }

    List<int> Get_All_Triangles_On_Side(Mesh _mesh, GameObject _plane)
    {
        List<int> triangles_on_side = new List<int>();
        return triangles_on_side;
    }
	
    List<Vector3> Get_On_Side_Verts(Mesh _mesh, List<int> _on_side_triangles)
    {
        List<Vector3> on_side_verts = new List<Vector3>();

        return on_side_verts;
    }

    Mesh Generate_Mesh(List<int> _triangles, List<Vector3> _verts)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = _verts.ToArray();
        mesh.triangles = _triangles.ToArray();
        return mesh;
    }
}
