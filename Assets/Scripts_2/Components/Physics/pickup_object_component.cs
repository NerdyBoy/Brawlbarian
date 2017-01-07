using UnityEngine;
using System.Collections;
using System;

public class pickup_object_component : MonoBehaviour, input_button_interface
{
    [SerializeField]
    private Camera look_object;
    [SerializeField]
    private float pickup_distance = 4.0f;
    [SerializeField]
    private float hold_distance = 3.0f;
    [SerializeField]
    private float object_launch_force;

    private pickupable_object_component picked_up_object;

    void Start()
    {
        Get_Look_Object();
    }

    void Get_Look_Object()
    {
        if(null == look_object)
        {
            look_object = this.transform.root.GetComponentInChildren<Camera>();
        }
    }

    public void On_Button_Input(action_buttons _button_action, action_button_states _button_state)
    {
        if (null == picked_up_object)
        {
            if (action_buttons.interact_button == _button_action && action_button_states.down == _button_state)
            {
                Vector3 origin = this.transform.position;
                Vector3 direction = this.transform.forward;

                if(null != look_object)
                {
                    origin = look_object.transform.position;
                    direction = look_object.transform.forward;
                }

                pickupable_object_component found_pickupable_object = cast_utility.Cast_For_Component<pickupable_object_component>(origin, direction, pickup_distance);
                if (null != found_pickupable_object)
                {
                    Pickup_Object(found_pickupable_object);
                }
            }
        }
        else
        {
            if (action_buttons.interact_button == _button_action && action_button_states.down == _button_state)
            {
                Release_Object();
            }
            if(action_buttons.secondary_button == _button_action && action_button_states.down == _button_state)
            {
                Launch_Object();
            }
        }
    }

    void Pickup_Object(pickupable_object_component _pickupable_object)
    {
        Parent_Pickupable_Object(_pickupable_object, true);
        Position_Pickupable_Object(_pickupable_object);
        _pickupable_object.GetComponent<Rigidbody>().isKinematic = true;
        picked_up_object = _pickupable_object;
    }

    void Release_Object()
    {
        picked_up_object.GetComponent<Rigidbody>().isKinematic = false;
        Parent_Pickupable_Object(picked_up_object, false);
        picked_up_object = null;
    }

    void Parent_Pickupable_Object(pickupable_object_component _pickupable_object, bool _set_parent_to_self)
    {
        if(true == _set_parent_to_self)
        {
            _pickupable_object.transform.parent = this.transform;
        }
        else
        {
            _pickupable_object.transform.parent = null;
        }
    }

    void Position_Pickupable_Object(pickupable_object_component _pickupable_object)
    {
        _pickupable_object.transform.position = this.transform.position + (this.transform.forward * hold_distance);
    }

    void Launch_Object()
    {
        pickupable_object_component object_ref = picked_up_object;
        Release_Object();
        object_ref.Launch_Object(this.transform.forward, object_launch_force);
    }
}
