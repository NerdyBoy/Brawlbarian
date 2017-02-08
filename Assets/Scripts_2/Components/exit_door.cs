using UnityEngine;
using System.Collections;

public class exit_door : MonoBehaviour {

    public int level_number;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<weapon_component>() != null)
        {
            if(rage_component.global_rage_component.current_rage == 100)
            {
                character_controller character = FindObjectOfType<character_controller>();
                if(character != null && level_number == 0)
                {
                    Destroy(character.gameObject);
                }
                Application.LoadLevel(level_number);
            }
        }
    }
}
