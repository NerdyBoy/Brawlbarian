using UnityEngine;
using System.Collections;

public class weapon_handling_component : MonoBehaviour, input_button_interface{

    [SerializeField]
    private float pickup_distance;

    [SerializeField]
    private GameObject position_and_rotation_object; //character camera
    [SerializeField]
    private weapon_component equipped_weapon; //the weapon currently equipped
    [SerializeField]
    private GameObject connection_bone; //bone in the armature to connect the weapon to

    private weapon_component found_weapon; //weapon found through casting

    private void Start()
    {
        if(null != equipped_weapon)
        {
            Equip_Weapon(equipped_weapon);
        }
    }

    public void On_Button_Input(action_buttons _button_action, action_button_states _button_state)
    {
        if(action_buttons.interact_button == _button_action && action_button_states.down == _button_state
            && null != position_and_rotation_object && null != connection_bone)
        {
            found_weapon = Get_Weapon();
            if (null != found_weapon)
            {
                Equip_Weapon(found_weapon);
            }
        }
    }

    private weapon_component Get_Weapon()
    {
        weapon_component found_weapon = cast_utility.Cast_For_Component<weapon_component>(position_and_rotation_object.transform.position, position_and_rotation_object.transform.forward, pickup_distance);
        if(null != found_weapon)
        {
            return found_weapon;
        }
        return null;
    }

    private void Equip_Weapon(weapon_component _weapon)
    {
        if(null != equipped_weapon)
        {
            Discard_Weapon();
        }
        if(null == equipped_weapon)
        {
            _weapon.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            Parent_Weapon(_weapon, true);
            Position_Weapon(_weapon);
            Ignore_Weapon_Collision(_weapon, true);
            SendMessage("Weapon_Equipped", true);
        }
    }

    private void Discard_Weapon()
    {
        equipped_weapon.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Parent_Weapon(equipped_weapon, false);
        Ignore_Weapon_Collision(equipped_weapon, false);
        SendMessage("Weapon_Equipped", false);
    }

    private void Parent_Weapon(weapon_component _weapon, bool _set_bone_as_parent)
    {
        if (true == _set_bone_as_parent)
        {
            _weapon.transform.parent = connection_bone.transform;
        }
        else
        {
            _weapon.transform.parent = null;
        }
    }

    private void Position_Weapon(weapon_component _weapon)
    {
        _weapon.transform.localPosition = new Vector3(-0.019f, 0.148f, 0.04f);
        _weapon.transform.localEulerAngles = new Vector3(0.0f, -100.472f, 0.0f);
    }

    private void Ignore_Weapon_Collision(weapon_component _weapon, bool _ignore_collision)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), _weapon.gameObject.GetComponent<Collider>(), _ignore_collision);
    }
}
