using UnityEngine;
using System.Collections;

public class minion_spawner : MonoBehaviour {

    public GameObject minion_prefab;
    public int max_minions_to_spawn;
    public float spawn_delay;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn_Minions());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn_Minion()
    {
        Instantiate(minion_prefab, this.transform.position, Quaternion.identity);
    }

    IEnumerator Spawn_Minions()
    {
        int minions_spawned = 0;
        while(minions_spawned < max_minions_to_spawn)
        {
            Spawn_Minion();
            minions_spawned++;
            yield return new WaitForSeconds(spawn_delay);
        }

    }
}
