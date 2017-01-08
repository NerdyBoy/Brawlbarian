using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class physics_enable_on_hit : MonoBehaviour {

    private Rigidbody light_rigidbody;

	// Use this for initialization
	void Start ()
    {
        light_rigidbody = GetComponent<Rigidbody>();
        if (null != light_rigidbody)
        {
            light_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(null != light_rigidbody)
        {
            light_rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
