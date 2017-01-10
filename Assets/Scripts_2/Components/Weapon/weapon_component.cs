using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class weapon_component : MonoBehaviour {

    [SerializeField]
    private float launch_force;
    [SerializeField]
    private float spin_speed;

    private Rigidbody weapon_rigidbody;
    private Collider weapon_collider;

	// Use this for initialization
	void Start () {
        weapon_rigidbody = GetComponent<Rigidbody>();
        weapon_collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Launch_Weapon(Vector3 _direction)
    {
        weapon_rigidbody.isKinematic = false;
        weapon_rigidbody.AddForce(_direction * launch_force, ForceMode.Impulse);
        weapon_rigidbody.AddTorque(0, 0, spin_speed, ForceMode.Impulse);

    }
}
