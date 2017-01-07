using UnityEngine;
using System.Collections;

public class t_destructible_physics_object : t_physics_object {

    [Header("If no shattered verios available add a non-shattered replacement mesh")]
    [SerializeField]
    private bool has_shattered_version;
    [SerializeField]
    private GameObject shattered_version;

    bool object_shattered = false;

    [SerializeField]
    private GameObject loot_object = null;

    [SerializeField]
    private float break_force = 0.0f;

    [Header("Modifies the force applied by the weapon (easier or harder to break item)")]
    [SerializeField]
    private float incoming_force_modifier = 1;

    [Header("Modifies the velocity of the hit object (make it faster or slower)")]
    [SerializeField]
    private float outgoing_velocity_modifier = 1;

    [SerializeField]
    private GameObject text_game_object = null;

    private GameObject last_weapon_hit = null;

	// Use this for initialization
	void Start () {
        Initialise ();
	}

    public override void Add_Force (Vector3 _direction, float _force) {
        base.Add_Force (_direction, _force * outgoing_velocity_modifier);
        Hit ((_direction * _force).magnitude);
    }

    public void Hit (float _force) {
        print(_force);
        if (true == Break_Check (_force * incoming_force_modifier)) {
            if(null != hitting_object.Get_Hitter().GetComponent<t_player>()) {
                hitting_object.Get_Hitter().GetComponent<t_player>().Add_Score(Mathf.RoundToInt(_force));
            }
            Spawn_Text_Object(Mathf.RoundToInt(break_force), Color.red);
            Break_Physics_Object ();
        }
        else {
            break_force -= _force * incoming_force_modifier;
            if (null != hitting_object.Get_Hitter().GetComponent<t_player>()) {
                hitting_object.Get_Hitter().GetComponent<t_player>().Add_Score(Mathf.RoundToInt(_force));
            }
            if (_force > 1) {
                Spawn_Text_Object(Mathf.RoundToInt(break_force), Color.yellow);
            }
        }
    }

    void Spawn_Text_Object(int _score, Color _color) {
        GameObject text_object = Instantiate(text_game_object, this.transform.position, Quaternion.identity) as GameObject;
        t_object_score_display score_display = text_game_object.GetComponent<t_object_score_display>();
        score_display.Initialize();
        //score_display.Setup(_score.ToString(), _color, _color);
    }

    protected virtual bool Break_Check (float _force) {
        if(_force > break_force) {
            return true;
        }
        return false;
    }

    protected virtual void Break_Physics_Object () {
        if (false == object_shattered)
        {
            Spawn_Replacement();
            //Spawn_Loot_Object();
            Destroy(this.gameObject);
            object_shattered = true;
        }
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

    protected override void Handle_Collision(Collision _col) {
        base.Handle_Collision(_col);
        if (false != _col.gameObject.CompareTag("weapon")) {
            print("Hit by player");
            hitting_object.Set_Hitter(_col.gameObject.transform.root.gameObject);
        } else if (null != _col.gameObject.GetComponent<t_hitting_object>()) {
            hitting_object.Set_Hitter(_col.gameObject.GetComponent<t_hitting_object>().Get_Hitter());
        }

        Hit(_col.relativeVelocity.magnitude);
    }
}
