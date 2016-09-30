using UnityEngine;
using System.Collections;

public class t_destructible_physics_object : t_physics_object {

    [Header("If no shattered verios available add a non-shattered replacement mesh")]
    [SerializeField]
    private bool has_shattered_version;
    [SerializeField]
    private GameObject shattered_version;
    [SerializeField]
    private GameObject loot_object;

    [SerializeField]
    private float break_force;

    [Header("Modifies the force applied by the weapon (easier or harder to break item)")]
    [SerializeField]
    private float incoming_force_modifier = 1;

    [Header("Modiefies the velocity of the hit object (make it faster or slower)")]
    [SerializeField]
    private float outgoing_velocity_modifier = 1;

	// Use this for initialization
	void Start () {
        Initialise ();
	}

    public override void Add_Force (Vector3 _direction, float _force) {
        base.Add_Force (_direction, _force * outgoing_velocity_modifier);
        Hit ((_direction * _force).magnitude);
    }

    public void Hit (float _force) {
        if (true == Break_Check (_force * incoming_force_modifier)) {
            Break_Physics_Object ();
            Destroy (this.gameObject);
        }
        else {
            break_force -= _force * incoming_force_modifier;
        }
        
    }

    protected virtual bool Break_Check (float _force) {
        if(_force > break_force) {
            return true;
        }
        return false;
    }

    protected virtual void Break_Physics_Object () {
        Spawn_Replacement ();
        Spawn_Loot_Object ();
        Destroy (this.gameObject);
    }

    private void Spawn_Replacement () {
        if (null != shattered_version) {
            GameObject replacement = Instantiate (shattered_version, this.transform.position, this.transform.rotation) as GameObject;
            if (null != replacement.GetComponent<Rigidbody> ()) {
                replacement.GetComponent<Rigidbody> ().velocity = physics_object_rigidbody.velocity;
            }
            else {
                Debug.LogError ("Replacement gameobject does not have a rigidbody, have you attached an incorrect gameObject?");
            }
        }
        else {
            Debug.LogError ("No shattered version set.");
        }
    }

    private void Spawn_Loot_Object () {
        GameObject loot = Instantiate (loot_object, this.transform.position, this.transform.rotation) as GameObject;
        if (null != loot.GetComponent<Rigidbody> ()) {
            loot.GetComponent<Rigidbody> ().velocity = physics_object_rigidbody.velocity;
        }
        else {
            Debug.LogError ("Loot gameobject does not have a rigidbody, have you attached an incorrect gameObject?");
        }
    }

    void OnCollisionEnter(Collision _col) {
        
        Hit (_col.relativeVelocity.magnitude);
    }
}
