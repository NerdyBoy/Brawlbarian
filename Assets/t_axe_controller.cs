using UnityEngine;
using System.Collections;

public class t_axe_controller : MonoBehaviour {

    Animator axe_animator;
    public bool is_attacking = false;
    public GameObject impact_point = null;
    public float axe_strength;
    bool weapon_colliding = false;
    Rigidbody axe_rigidbody = null;

	// Use this for initialization
	void Start () {
        axe_rigidbody = GetComponent<Rigidbody>();
        axe_animator = GetComponent<Animator>();
        Collider[] colliders = GetComponents<Collider>();
        for(int i = 0; i< colliders.Length; i++) {
            Physics.IgnoreCollision(colliders[i], this.transform.root.GetComponent<Collider>());
        }
	}

    void Update() {
    }

    public void Calculate_Swing(weapon_struct _weapon_struct) {
        Vector2 _mouse_velocity = _weapon_struct.mouse_delta;
        if (1 == _weapon_struct.button_index)
        {
            Launch();
        }
        else
        {
            if (Mathf.Abs(_mouse_velocity.x) > Mathf.Abs(_mouse_velocity.y))
            {
                if (_mouse_velocity.x < 0)
                {
                    Swing_Left();
                }
                else
                {
                    Swing_Right();
                }
            }
            else
            {
                if (_mouse_velocity.y < 0)
                {
                    Swing_Down();
                }
                else
                {
                    Thrust();
                }
            }
        }
    }

    void Reset_Animations() {
        is_attacking = false;
        axe_animator.SetBool("axe_swing_left", false);
        axe_animator.SetBool("axe_swing_right", false);
        axe_animator.SetBool("axe_swing_down", false);
        axe_animator.SetBool("axe_thrust", false);
    }

    public void Swing_Left() {
        Reset_Animations();
        is_attacking = true;
        axe_animator.SetBool("axe_swing_left", true);
        
    }

    public void Swing_Right() {
        Reset_Animations();
        is_attacking = true;
        axe_animator.SetBool("axe_swing_right", true);
    }

    public void Swing_Down() {
        Reset_Animations();
        is_attacking = true;
        axe_animator.SetBool("axe_swing_down", true);
    }

    public void Thrust() {
        Reset_Animations();
        is_attacking = true;
        axe_animator.SetBool("axe_thrust", true);
    }

    void Launch(){
        axe_animator.enabled = false;
        axe_rigidbody.isKinematic = false;
        axe_rigidbody.freezeRotation = false;
        axe_rigidbody.constraints = RigidbodyConstraints.None;
        axe_rigidbody.AddForce(this.transform.forward * 1000);
        this.transform.parent = null;
    }

    void OnCollisionEnter(Collision _col) {
        if (false == weapon_colliding) {
            weapon_colliding = true;
            damage_information damage_information;
            damage_information.weapon_damage = 100;
            damage_information.weapon_force = 200;
            damage_information.damaging_object = this.gameObject;
            damage_information.force_direction = transform.root.forward;
            damage_information.player_object = this.transform.root.gameObject;
            damage_information.collision_combo = 1;
            _col.gameObject.GetComponent<t_destructible_object>().Take_Damage(damage_information);
        }
    }

    void OnCollisionExit(Collision _col) {
        weapon_colliding = false;
    }

}
