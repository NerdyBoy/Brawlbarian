using UnityEngine;
using System.Collections;

public class t2_destructible_physics_object : t2_physics_object {

    [SerializeField]
    private GameObject shattered_object_prefab = null;
    [SerializeField]
    private float force_left_required_to_break = 0;
    [SerializeField]
    private GameObject text_object_prefab = null;
    [SerializeField]
    private float damage_rate_limit = 0.25f;
    private float next_damage_time = 0;

    private bool object_has_been_broken = false;

    void Start() {
        if(null == shattered_object_prefab) {
            Debug.Log("Destructible object has no shattered version.");
        }
    }

    protected virtual void Hit(float _force) {
        if (_force >= 1) {
            force_left_required_to_break -= _force;
            Instantiate_Text_Object(_force);
            Send_Score_To_Player(_force);
        }

        if(force_left_required_to_break <= 0) {
            Break_Object();
        }
    }

    protected virtual void Break_Object() {
        if (false == object_has_been_broken) {
            object_has_been_broken = true;
            if (null != shattered_object_prefab) {
                Instantiate_Replacement_Object();
            }
            Destroy(this.gameObject);
        }

    }

    protected virtual void Instantiate_Replacement_Object() {
        if (null != shattered_object_prefab) {
            GameObject shattered_object = Instantiate(shattered_object_prefab, this.transform.position, Quaternion.identity) as GameObject;
            shattered_object.GetComponent<Rigidbody>().velocity = physics_rigidbody.velocity;
        }
    }

    protected virtual void Instantiate_Text_Object(float _force) {
        GameObject display_object = Instantiate(text_object_prefab, this.transform.position, Quaternion.identity) as GameObject;
        if(null != display_object.GetComponent<t_object_score_display>()) {
            t_object_score_display display_object_script = display_object.GetComponent<t_object_score_display>();
            display_object_script.Setup(Mathf.RoundToInt(_force), Color.yellow);
        }
    }

    protected virtual void Send_Score_To_Player(float _force) {
        if (hitting_object != null && hitting_object.Get_Hitting_Object() != null) {
            hitting_object.Get_Hitting_Object().GetComponent<t_player>().Add_Score(Mathf.RoundToInt(_force));
        }
    }

    protected override void Add_Force(Vector3 _direction, float _force) {
        base.Add_Force(_direction, _force);
        if (Time.fixedTime > next_damage_time) {
            Hit(_force);
            next_damage_time = Time.fixedTime + damage_rate_limit;
        }
    }
}
