using UnityEngine;
using System.Collections;

public class ai_spawn : MonoBehaviour {

    
    public GameObject ai_prefab;
    public GameObject furniture_object;
    public float destruction_limit_percentage;
    private float initial_number;
    private float destruction_number;
    bool spawned = false;

	// Use this for initialization
	void Start () {
	    if(ai_prefab != null && furniture_object != null)
        {
            initial_number = furniture_object.transform.childCount;
            destruction_number = initial_number * destruction_limit_percentage;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (spawned == false && furniture_object.transform.childCount < destruction_number)
        {
            Spawn_AI();
            spawned = true;
        }
	}

    void Spawn_AI()
    {
        Instantiate(ai_prefab, this.transform.position, this.transform.rotation);
    }
}
