using UnityEngine;
using System.Collections;

public class Destroy_Players : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Character_Controller[] characters = FindObjectsOfType<Character_Controller>();
        for(int i = 0; i < characters.Length; i++)
        {
            Destroy(characters[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
