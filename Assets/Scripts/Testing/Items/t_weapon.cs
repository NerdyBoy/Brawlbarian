using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animation))]
public class t_weapon : MonoBehaviour {

    private Rigidbody weapon_rigidbody;
    private Animation weapon_animation;

    [SerializeField]
    float weapon_strength = 10.0f;

	// Use this for initialization
	void Start () {
        weapon_rigidbody = GetComponent<Rigidbody> ();
        weapon_rigidbody.isKinematic = true;

        weapon_animation = GetComponent<Animation> ();
	}
	
	public void Attack () {
        weapon_animation.Play ();
    }

    public void OnCollisionEnter(Collision _col) {
        if (true == weapon_animation.isPlaying) {
            if (null != _col.gameObject.GetComponent<t_physics_object> ()) {
                _col.gameObject.GetComponent<t_physics_object> ().Add_Force (this.transform.forward, weapon_strength);
            }
        }
    }
}
