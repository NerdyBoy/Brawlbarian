using UnityEngine;
using System.Collections;

public class t_destructible_object : MonoBehaviour {

    public float object_health;
    public GameObject shattered_object;
    public GameObject text_object;
    private Rigidbody object_rigidbody;
    private damage_information last_damage_information;

	// Use this for initialization
	void Start () {
        object_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Take_Damage(damage_information _damaging_object) {
        print("Take Damage");
        Apply_Force(_damaging_object);
        last_damage_information = _damaging_object;
        if(null != _damaging_object.player_object) {
            Give_Score(last_damage_information);
        }
        Apply_Damage(_damaging_object.weapon_damage);
    }

    void Apply_Force(damage_information _damaging_object){
        object_rigidbody.AddForce(_damaging_object.player_object.transform.forward * 800, ForceMode.Impulse);
    }

    void Give_Score(damage_information _damage_information) {
        print(_damage_information.collision_combo);
        t_player player_script = _damage_information.player_object.GetComponent<t_player>();
        if(null != player_script) {
            player_script.Add_Score((int)_damage_information.weapon_force * last_damage_information.collision_combo);
        }
    }

    void Apply_Damage(float _damage) {
        object_health -= _damage;
        if (null != text_object && (int)_damage > 0) {
            GameObject spawned_text_object = Instantiate(text_object, this.transform.position, this.transform.rotation) as GameObject;
            t_object_score_display score_display = spawned_text_object.GetComponent<t_object_score_display>();
            if (last_damage_information.collision_combo > 1) {
                score_display.Setup(((int)_damage).ToString(), " X" + last_damage_information.collision_combo.ToString());
            }
            else {
                score_display.Setup(((int)_damage).ToString(), "");
            }
            
        }
        if(object_health <= 0) {
            Spawn_Shattered_Version();
            Destroy(this.gameObject);
        }
    }

    void Spawn_Shattered_Version() {
        if(null != shattered_object) {
            GameObject shattered_version = Instantiate(shattered_object, this.transform.position, this.transform.rotation) as GameObject;
        }
    }

    void OnCollisionEnter(Collision _col) {
        if (null != last_damage_information.player_object) {
            damage_information object_damage_information;
            if (null != object_rigidbody) {
                object_damage_information.weapon_damage = object_rigidbody.velocity.magnitude * 20;
            }
            else {
                object_damage_information.weapon_damage = 10.0f;
            }
            object_damage_information.force_direction = this.transform.forward;
            object_damage_information.damaging_object = this.gameObject;
            object_damage_information.weapon_force = object_rigidbody.velocity.magnitude;
            object_damage_information.player_object = last_damage_information.player_object;
            object_damage_information.collision_combo = last_damage_information.collision_combo + 1;
            _col.gameObject.GetComponent<t_destructible_object>().Take_Damage(object_damage_information);
            //_col.gameObject.SendMessage("Take_Damage", object_damage_information);
        }
    }
}
