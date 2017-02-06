using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
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

    [SerializeField]
    private int weapon_layer = 11;
    [SerializeField]
    private int destruction_layer = 13;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if(null != equipped_weapon)
        {
            found_weapon = equipped_weapon;
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

        if(action_buttons.special_button == _button_action && action_button_states.down == _button_state)
        {
            if(null != equipped_weapon)
            {
                this.gameObject.SendMessage("Enable_Effect");
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
            equipped_weapon = _weapon;
            Collider[] colliders = equipped_weapon.GetComponentsInChildren<Collider>();
            for(int i = 0; i < colliders.Length; i++)
            {
                Physics.IgnoreCollision(colliders[i], this.transform.root.GetComponent<Collider>());
            }
            SendMessage("Weapon_Equipped", true);
        }
    }

    private void Discard_Weapon()
    {
        equipped_weapon.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Parent_Weapon(equipped_weapon, false);
        Ignore_Weapon_Collision(equipped_weapon, false);
        equipped_weapon = null;
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
        _weapon.transform.localEulerAngles = new Vector3(0.0f, -100.472f, 140.0f);
    }

    private void Ignore_Weapon_Collision(weapon_component _weapon, bool _ignore_collision)
    {
        Collider[] weapon_colliders = _weapon.gameObject.GetComponentsInChildren<Collider>();
        for (int i = 0; i < weapon_colliders.Length; i++)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), _weapon.gameObject.GetComponent<Collider>(), _ignore_collision);
        }
    }

    void Attack_Start()
    {
        if (null != source)
        {
            source.Play();
        } 
        Physics.IgnoreLayerCollision(weapon_layer, destruction_layer, false);
        BroadcastMessage("Enable_Collider");
    }

    void Attack_End()
    {
        Physics.IgnoreLayerCollision(weapon_layer, destruction_layer, true);
        BroadcastMessage("Disable_Collider");
    }

    void Launch_Equipped_Weapon()
    {
        if(null != equipped_weapon)
        {
            weapon_component discarded_weapon = equipped_weapon;
            Physics.IgnoreLayerCollision(weapon_layer, destruction_layer, false);
            Discard_Weapon();
            if (null != position_and_rotation_object)
            {
                discarded_weapon.Launch_Weapon(position_and_rotation_object.transform.forward);
            }
            else
            {
                discarded_weapon.Launch_Weapon(this.transform.forward);
            }

        }
    }
}
