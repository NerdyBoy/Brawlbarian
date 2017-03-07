using UnityEngine;
using System.Collections;

public class Player_Spawn : MonoBehaviour {

    public GameObject player_prefab;

	// Use this for initialization
	void OnLevelWasLoaded () {
        Character_Controller character = FindObjectOfType<Character_Controller>();
        if(character == null && player_prefab != null)
        {
            Instantiate(player_prefab, transform.position, this.transform.rotation);
        }
	}

    void Start()
    {
        Character_Controller character = FindObjectOfType<Character_Controller>();
        if (character == null && player_prefab != null)
        {
            Instantiate(player_prefab, transform.position, this.transform.rotation);
        }
    }
}
