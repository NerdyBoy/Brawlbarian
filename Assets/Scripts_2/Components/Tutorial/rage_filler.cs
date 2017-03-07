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
            Rage.rage.total_rage = 1000.0f;
        }
    }
}
