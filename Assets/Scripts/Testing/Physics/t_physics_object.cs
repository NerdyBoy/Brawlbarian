using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class t_physics_object : MonoBehaviour {

    protected Rigidbody physics_object_rigidbody;

    void Start () {
        if(null == GetComponent<Collider>()) {
            Debug.LogError ("t_physics object does not have a collider attached, this will not work.");
        }
        Initialise ();
    }

    protected virtual void Initialise () {
        physics_object_rigidbody = GetComponent<Rigidbody> ();
    }

	public virtual void Add_Force(Vector3 _direction, float _force) {
        if(null != physics_object_rigidbody) {
            physics_object_rigidbody.AddForce (_direction * _force, ForceMode.Impulse);
        }
    }
}
