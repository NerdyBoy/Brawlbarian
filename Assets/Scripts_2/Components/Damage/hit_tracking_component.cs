using UnityEngine;
using System.Collections;
using System;

public class hit_tracking_component : MonoBehaviour, hit_tracking_interface {

    private character_controller root_hit_character;
    private GameObject hit_object;

    private int number_of_objects_hit = 0;

    public character_controller Get_Hit_Root_Character()
    {
        return root_hit_character;
    }

    public void Set_Hit_Root_Character(character_controller _character)
    {
        root_hit_character = _character;
    }

    public void Set_Hit_Object(GameObject _hit_object)
    {
        hit_object = _hit_object;
    }

    public int Get_Number_Of_Objects_Hit()
    {
        return number_of_objects_hit;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        hit_tracking_component hit_tracker = _collision.gameObject.GetComponent<hit_tracking_component>();
        weapon_component weapon = _collision.gameObject.GetComponent<weapon_component>();
        if(null != weapon)
        {
            number_of_objects_hit = 0;
        }

        if(null != hit_tracker)
        {
            number_of_objects_hit = hit_tracker.Get_Number_Of_Objects_Hit() + 1;
            if(null != hit_tracker.Get_Hit_Root_Character())
            {
                Set_Hit_Root_Character(hit_tracker.Get_Hit_Root_Character());
            }
        }
    }
}
