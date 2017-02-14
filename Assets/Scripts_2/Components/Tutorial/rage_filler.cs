using UnityEngine;
using System.Collections;

public class rage_filler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            rage_component.global_rage_component.current_rage = 100.0f;
        }
    }
}
