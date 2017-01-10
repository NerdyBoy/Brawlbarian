using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(physics_object_component))]
[RequireComponent(typeof(health_component))]
[RequireComponent(typeof(physics_object_component))]
[RequireComponent(typeof(damage_component))]
[RequireComponent(typeof(velocity_tracking_component))]
[RequireComponent(typeof(hit_tracking_component))]
[RequireComponent(typeof(destruction_object_component))]
[RequireComponent(typeof(physics_damage_component))]
[RequireComponent(typeof(pickupable_object_component))]
public class destructible_object_setup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
