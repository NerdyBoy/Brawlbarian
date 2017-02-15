using UnityEngine;
using System.Collections;

public class ai_spawn : MonoBehaviour {

    
    public GameObject ai_prefab;
    public GameObject furniture_object;
    public float rage_limit;
    bool spawned = false;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (spawned == false && rage_component.global_rage_component.total_rage > rage_limit)
        {
            Spawn_AI();
            spawned = true;
        }
	}

    void Spawn_AI()
    {
        if (ai_prefab != null)
        {
            Instantiate(ai_prefab, this.transform.position, this.transform.rotation);
        }
    }
}
