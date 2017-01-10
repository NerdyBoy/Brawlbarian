using UnityEngine;
using System.Collections;

public class TEMP_spawn_player : MonoBehaviour {

    [SerializeField]
    private GameObject player_prefab;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        if(null == player)
        {
            Instantiate(player_prefab, this.transform.position, this.transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
