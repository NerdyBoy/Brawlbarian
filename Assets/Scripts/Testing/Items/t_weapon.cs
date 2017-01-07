using UnityEngine;
using System.Collections;

public struct damage_information {
    public float weapon_damage;
    public float weapon_force;
    public GameObject damaging_object;
    public Vector3 force_direction;
    public GameObject player_object;
    public int collision_combo;
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
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

    private bool weapon_collided = false;

    GameObject player_object;

    // Use this for initialization
    void Start() {
        player_object = this.transform.root.gameObject;
        weapon_rigidbody = GetComponent<Rigidbody>();
        weapon_rigidbody.isKinematic = true;
        weapon_animation = GetComponent<Animation>();

        if (null != impact_point) {
            last_position = impact_point.transform.position;
            current_position = impact_point.transform.position;
        }
        else {
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

    public void Start_Attack(int _mouse_id) {
        print(_mouse_id);
        if (false == Is_Attacking()) {
            
            if (0 == _mouse_id) {
                is_attacking = true;
            }
            else if(1 == _mouse_id) {
                Launch();
            }
        }
    }

    public void Launch() {
        weapon_rigidbody.isKinematic = false;
        weapon_rigidbody.freezeRotation = false;
        weapon_rigidbody.constraints = RigidbodyConstraints.None;
        weapon_rigidbody.AddForce(this.transform.root.forward * 50, ForceMode.Impulse);
        this.transform.parent = null;
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

    public void Set_Weapon_Strength(float _new_strength) {
        weapon_strength = _new_strength;
    }

    void OnCollisionExit(Collision _col) {
        weapon_collided = false;
    }
}
