using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animation))]
public class t_weapon : MonoBehaviour {

    private Rigidbody weapon_rigidbody = null;
    private Animation weapon_animation = null;
    private bool is_attacking = false;

    private Vector3 last_position = Vector3.zero;
    private Vector3 current_position = Vector3.zero;
    private float velocity = 0.0f;

    private Vector3 direction = Vector3.zero;

    [SerializeField]
    private float weapon_strength = 10.0f;
    [SerializeField]
    private GameObject impact_point = null;

	// Use this for initialization
	void Start () {
        weapon_rigidbody = GetComponent<Rigidbody> ();
        weapon_rigidbody.isKinematic = true;
        weapon_animation = GetComponent<Animation> ();

        if (null != impact_point) {
            last_position = impact_point.transform.position;
            current_position = impact_point.transform.position;
        } else {
            Debug.Log("No impact point given");
        }
	}

    void Update() {
        is_attacking = weapon_animation.isPlaying;
        current_position = impact_point.transform.position;
        velocity = Vector3.Distance(last_position, current_position);
        direction = current_position - last_position;
        last_position = impact_point.transform.position;
    }
	
	public void Attack () {
        weapon_animation.Play ();
    }

    public float Get_Weapon_Strength() {
        return weapon_strength;
    }

    public float Get_Velocity() {
        return velocity;
    }

    public Vector3 Get_Direction() {
        return direction;
    }

    public bool Is_Attacking() {
        return is_attacking;
    }
}
