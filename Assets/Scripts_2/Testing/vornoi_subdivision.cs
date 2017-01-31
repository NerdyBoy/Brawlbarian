using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vornoi_subdivision : MonoBehaviour {

    int[] directions;

    public class voronoi_tree_node
    {
        public List<Vector3> node_verts{ get; set; }
        public List<Vector3> node_points { get; set; }
        public List<voronoi_tree_node> children { get; set; }
    }

    [SerializeField]
    private int width, depth;
    private int total_cells;

    [SerializeField]
    private Mesh mesh;

    private voronoi_tree_node root_node { get; set; }

    private void Start()
    {
        total_cells = (int)Mathf.Pow(width, depth);
    }

    private Vector3 Calculate_Random_Center_Offset(Vector3 _center, Vector3 _bounds)
    {
        return Vector3.zero;
    }
    
    private void Add_Points()
    {
        
    }

}
