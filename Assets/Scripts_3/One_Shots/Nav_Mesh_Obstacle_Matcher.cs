using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Nav_Mesh_Obstacle_Matcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnGUI()
    {
        print("WORKINg");
        NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();
        BoxCollider box = GetComponent<BoxCollider>();
        obstacle.size = box.size;
        obstacle.center = box.center;
        Destroy(this);
    }
}
