using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(t_hitting_object))]
public class t_physics_object : MonoBehaviour {

    protected Rigidbody physics_object_rigidbody;
    protected t_hitting_object hitting_object;

    void Start () {
        if(null == GetComponent<Collider>()) {
            Debug.LogError ("t_physics object does not have a collider attached, this will not work.");
        }
        Initialise ();
    }

    protected virtual void Initialise () {
        physics_object_rigidbody = GetComponent<Rigidbody> ();
        hitting_object = GetComponent<t_hitting_object>();
    }

	public virtual void Add_Force(Vector3 _direction, float _force) {
        if(null != physics_object_rigidbody) {
            physics_object_rigidbody.AddForce (_direction * _force, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision _col) {
        Handle_Collision(_col);
    }

    protected virtual void Handle_Collision(Collision _col) {
        if(null != _col.gameObject.GetComponent<t_hitting_object>()) {

            hitting_object.Set_Hitter(_col.gameObject.GetComponent<t_hitting_object>().Get_Hitter());
        }
    }
}
