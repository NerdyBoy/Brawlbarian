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
        physics_object_component physics_object = collision.gameObject.GetComponent<physics_object_component>();
        physics_damage_component physics_damage_object = collision.gameObject.GetComponent<physics_damage_component>();
        if (null != light_rigidbody && (null != physics_object || null != physics_damage_object)) 
        {
            light_rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
