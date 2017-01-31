using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(damage_component))]
[RequireComponent(typeof(velocity_tracking_component))]
public class physics_damage_component : MonoBehaviour {

    private force_component force;
    private force_modifier_component force_modifier;

    private damage_component damage;
    private damage_modifier_component damage_modifier;

    private velocity_tracking_component velocity_tracker;
    private hit_tracking_component hit_tracking_component;
    private character_score_component score_component;

    [SerializeField]
    private bool overwrite_physics = false;

    private void Start()
    {
        force = GetComponent<force_component>();
        force_modifier = GetComponent<force_modifier_component>();

        damage = GetComponent<damage_component>();
        damage_modifier = GetComponent<damage_modifier_component>();
        velocity_tracker = GetComponent<velocity_tracking_component>();

        score_component = this.transform.root.GetComponent<character_score_component>();

        hit_tracking_component = GetComponent<hit_tracking_component>();
    }

    private void OnCollisionEnter(Collision _collision)
    {
        Vector3 direction_value = _collision.relativeVelocity;
        float force_value = 10.0f;
        float damage_value = 10.0f;

        if(null != damage)
        {
            damage_value = damage.Get_Damage_Amount();
            if(null != damage_modifier)
            {
                damage_value *= damage_modifier.Get_Damage_Modifier_Value();
            }
        }

        if(null != velocity_tracker)
        {
            direction_value = velocity_tracker.Get_Velocity().normalized;
            
        }
        if(null != force)
        {
            force_value = force.Get_Force_Amount();
            if(null != force_modifier)
            {
                force_value *= force_modifier.Get_Force_Modifier();
            }
        }
        if (null == score_component)
        {
            score_component = this.transform.root.GetComponent<character_score_component>();
        }

        if (true == overwrite_physics)
        {
            if (null != score_component && null != this.transform.root.GetComponent<character_controller>())
            {
                //this is a horrible workaround that gets the direction based on character controller, not good, very bad, fix
                float force_amount = force_value * Random.Range(0.75f, 1.25f);
                ExecuteEvents.Execute<physics_object_interface>(_collision.gameObject, null, (physics_object_interface _handler, BaseEventData _data) => _handler.On_Add_Force(this.transform.root.GetComponent<character_controller>().Get_Attack_Direction(), force_amount));
                if(null != hit_tracking_component)
                {
                    hit_tracking_component.Get_Hit_Root_Character().gameObject.SendMessage("Increase_Score", force_amount);
                }
            }
            else
            {
                ExecuteEvents.Execute<physics_object_interface>(_collision.gameObject, null, (physics_object_interface _handler, BaseEventData _data) => _handler.On_Add_Force(_collision.relativeVelocity.normalized, _collision.relativeVelocity.normalized.magnitude));
            }
        }
        ExecuteEvents.Execute<health_interface>(_collision.gameObject, null, (health_interface _handler, BaseEventData _data) => _handler.On_Modify_Health(-(int)damage_value));
    }
}
