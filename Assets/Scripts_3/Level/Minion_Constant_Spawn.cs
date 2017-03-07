using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minion_Constant_Spawn : MonoBehaviour {

    public GameObject minion_prefab;
    public int number_of_minions_to_spawn;
    List<GameObject> minions;
    

	// Use this for initialization
	void Start () {
        minions = new List<GameObject>();
        StartCoroutine(Spawn_Minions());
	}

    IEnumerator Spawn_Minions()
    {
        if(minion_prefab != null)
        {
            while(minions.Count < number_of_minions_to_spawn)
            {
                Instantiate(minion_prefab, this.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(0.75f);
            }
        }
    }
}
