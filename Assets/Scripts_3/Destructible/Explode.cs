using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Rigidbody[] contained_rigidbodies = GetComponentsInChildren<Rigidbody>();
        for(int i = 0; i < contained_rigidbodies.Length; i++)
        {
            contained_rigidbodies[i].AddExplosionForce(500, this.transform.position, 50, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
