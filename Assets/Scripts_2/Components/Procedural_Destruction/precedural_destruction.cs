using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class precedural_destruction : MonoBehaviour {

    
    public GameObject object_to_destroy;
    List<GameObject> destroyed_game_objects;
    

    // Use this for initialization
    void Start() {
        //get all meshes
        //for each mesh
        //get triangle list
        //create a new mesh using that
        //assign mesh to new game object
        Mesh object_mesh = object_to_destroy.GetComponent<MeshFilter>().mesh; //get the mesh we want to split
        Vector3 mesh_center = Get_Mesh_Center(object_mesh); //get the center of the vertices
        GameObject center_point = Create_Center_Point(mesh_center); //create game object at center position with random rotation
        List<int> tri_indexes = Get_All_Triangles_On_Side(object_mesh, center_point); //get all triangles that are 
        
    }

    List<int> Get_All_Triangles_On_Side(Mesh _object_mesh, GameObject _center_point)
    {
        int[] triangles = _object_mesh.triangles;
        for(int i = 0; i < triangles.Length; i+=3)
        {
            bool on_side = true;
            //do check
            
    }

    GameObject Create_Center_Point(Vector3 _position)
    {
        GameObject center = new GameObject("center point"); //create object
        Vector3 rotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)); //calculate random rotation
        center.transform.Rotate(rotation); //rotate
        center.transform.position = _position; //move to center of vertices
        return center; //return object
    }

    Vector3 Get_Mesh_Center(Mesh _mesh)
    {
        Vector3 center = Vector3.zero; //set center at zero
        Vector3[] verts = _mesh.vertices; //get verts from mesh
        for(int i = 0; i < verts.Length; i++) //iterate over verts
        {
            center += verts[i]; //add to center point
        }
        center /= verts.Length; //divide by total number of verts to get center
        return center;
    }

    Mesh Create_Mesh(List<Vector3> _verts, List<int> _triangles, List<Vector3> _normals, List<Vector2> _uvs)
    {
        Mesh new_mesh = new Mesh(); //create new mesh object
        new_mesh.SetVertices(_verts); //set verts
        new_mesh.SetTriangles(_triangles.ToArray(), 0); //set triangles
        new_mesh.SetNormals(_normals); //set normals
        new_mesh.SetUVs(0, _uvs); //set uvs
        new_mesh.RecalculateBounds(); //recalculate bounds
        new_mesh.RecalculateNormals(); //recalculate normals
        return new_mesh;
    }
    
}
