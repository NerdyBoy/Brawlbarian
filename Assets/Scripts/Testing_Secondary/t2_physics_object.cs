using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(t2_hitting_object))]

public class t2_physics_object : MonoBehaviour {
    protected Rigidbody physics_rigidbody = null;
    protected BoxCollider physics_collider = null;
    protected t2_hitting_object hitting_object = null;
    
    void Start() {
        Initialise();
    }   
    
    protected virtual void Initialise() {
        physics_rigidbody = GetComponent<Rigidbody>();
        physics_collider = GetComponent<BoxCollider>();
        hitting_object = GetComponent<t2_hitting_object>();
    } 

    void OnCollisionEnter(Collision _col) {
        Handle_Collision(_col);
    }

    protected virtual void Handle_Collision(Collision _col) {
        if(null != _col.gameObject.GetComponent<t_weapon>() && true == _col.gameObject.GetComponent<t_weapon>().Is_Attacking()) {
            t_weapon weapon_hit_object = _col.gameObject.GetComponent<t_weapon>();
            Add_Force(weapon_hit_object.Get_Direction(), weapon_hit_object.Get_Velocity() * weapon_hit_object.Get_Weapon_Strength());
        }
        else if(null != _col.gameObject.GetComponent<t2_physics_object>()) {
            Add_Force(Vector3.zero, _col.relativeVelocity.magnitude);
            //Add_Force(_col.gameObject.transform.forward, _col.relativeVelocity.magnitude);
        }
        
    }

    protected virtual void Add_Force(Vector3 _direction, float _force) {
        if(null == physics_rigidbody || null == physics_collider || null == hitting_object) {
            Initialise();
        }
        physics_rigidbody.AddForce(_direction * _force, ForceMode.Impulse);
    }
}
