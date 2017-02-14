using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vornoi_subdivisio_tree : MonoBehaviour {

    public GameObject object_to_destroy;
    public int tree_width;
    public int tree_depth;
    Material object_material;

    // Use this for initialization
    void Start() {
        Split_Object(object_to_destroy, 0, 10);
    }

    void Split_Object(GameObject _object, int _count, int _max)
    {
        if(_count >= _max)
        {
            return;
        }
        Mesh mesh_to_split = _object.GetComponent<MeshFilter>().mesh;
        object_material = _object.GetComponent<MeshRenderer>().material;
        List<Vector3> points = Generate_Points(mesh_to_split, 2);
        List<GameObject> planes = Generate_Planes(points);
        List<int> tris_onside = Get_All_Triangles_On_Side(mesh_to_split, planes[0]);
        List<Vector3> verts_onside = Get_On_Side_Verts(mesh_to_split, tris_onside);
        Split_Object(Generate_Gameobject(mesh_to_split, mesh_to_split.vertexCount, tris_onside, verts_onside, object_material), _count + 1, _max);
    }

    Vector3 Get_Center_Of_Mesh(Mesh _mesh)
    {
        Vector3 center = Vector3.zero;
        for(int i = 0; i < _mesh.vertexCount; i++)
        {
            center += _mesh.vertices[i];
        }
        return center / _mesh.vertexCount;
    }

    //num points must be divisible by 2
    List<Vector3> Generate_Points(Mesh _mesh, int _num_points)
    {
        List<int> used_selections = new List<int>();

        Vector3 center = Get_Center_Of_Mesh(_mesh);
        List<Vector3> points = new List<Vector3>();

        for(int i = 0; i < _num_points; i++)
        {
            //get random unused vert from mesh
            int selection = Random.Range(0, _mesh.vertexCount);
            while(used_selections.Contains(selection) == true)
            {
                selection = Random.Range(0, _mesh.vertexCount);
            }
            used_selections.Add(selection); //add selection to list of used selections
            Vector3 vert = _mesh.vertices[selection];
            //get point between mesh and center
            Vector3 point = Random_Position_Along_Line(vert, center);
            //add point
            points.Add(point);
        }

        return points;
    }

    Vector3 Random_Position_Along_Line(Vector3 _start, Vector3 _end)
    {
        Vector3 vector = _end - _start;
        Vector3 position = _start + (Random.Range(0, 1) * vector.normalized);
        return position;
    }

    List<GameObject> Generate_Planes(List<Vector3> _points)
    {
        List<GameObject> planes = new List<GameObject>();
        for(int i = 0; i < _points.Count; i+=2)
        {
            Vector3 point_one = _points[i];
            Vector3 point_two = _points[i + 1];

            GameObject plane_point = new GameObject("plane point");
            plane_point.transform.position = (point_two - point_one) / 2.0f;
            plane_point.transform.forward = Vector3.Cross(point_one, point_two);
            plane_point.transform.Rotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            planes.Add(plane_point);
        }
        return planes;
    }

    //get triangles I want to keep
    List<int> Get_All_Triangles_On_Side(Mesh _mesh, GameObject _plane)
    {
        List<int> triangles_on_side = new List<int>();
        for(int i = 0; i < _mesh.triangles.Length; i+=3)
        {
            Vector3 vert_one = _mesh.vertices[_mesh.triangles[i]];
            Vector3 vert_two = _mesh.vertices[_mesh.triangles[i + 1]];
            Vector3 vert_three = _mesh.vertices[_mesh.triangles[i + 2]];

            float cross_y_one = Vector3.Cross(vert_one, _plane.transform.forward).y;
            float cross_y_two = Vector3.Cross(vert_two, _plane.transform.forward).y;
            float cross_y_three = Vector3.Cross(vert_three, _plane.transform.forward).y;

            if(cross_y_one > 0 && cross_y_two > 0 && cross_y_three > 0)
            {
                triangles_on_side.Add(_mesh.triangles[i]);
                triangles_on_side.Add(_mesh.triangles[i + 1]);
                triangles_on_side.Add(_mesh.triangles[i + 2]);
            } 
        }
        return triangles_on_side;
    }
	
    List<Vector3> Get_On_Side_Verts(Mesh _mesh, List<int> _on_side_triangles)
    {
        List<Vector3> on_side_verts = new List<Vector3>();
        for(int i = 0; i < _on_side_triangles.Count; i++)
        {
            int vert_index = _on_side_triangles[i];
            on_side_verts.Add(_mesh.vertices[_on_side_triangles[i]]);
            _on_side_triangles[i] = on_side_verts.Count - 1;
            
        }
        return on_side_verts;
    }

    GameObject Generate_Gameobject(Mesh _mesh, int _initial_vert_length, List<int> _triangles, List<Vector3> _verts, Material _material)
    {
        GameObject destructed = new GameObject("Destructed Object");
        destructed.AddComponent<MeshFilter>().mesh = Generate_Mesh(_mesh, _initial_vert_length, _triangles, _verts);
        destructed.AddComponent<MeshRenderer>().material = _material;
        return destructed;
    }

    Mesh Generate_Mesh(Mesh _mesh, int _initial_vert_length, List<int> _triangles, List<Vector3> _verts)
    {
        Mesh mesh = new Mesh();
        int max_vert_index = 0;
        mesh.vertices = _verts.ToArray();
        for(int i = 0; i < _triangles.Count; i++)
        {
            if(_triangles[i] >= _verts.Count)
            {
                print("OUT OF BOUNDS VALUES IS " + _triangles[i]);
            }
        }
        print(max_vert_index);
        mesh.triangles = _triangles.ToArray();
        mesh.uv = _mesh.uv;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }
}
