using UnityEngine;
using System.Collections;
using System;

public class character_powerup_component : MonoBehaviour, character_powerup_interface {

    private movement_speed_modifier movement_modifier;
    private damage_modifier_component damage_modifier;

	private void Start()
    {
        movement_modifier = GetComponent<movement_speed_modifier>();
        damage_modifier = this.transform.root.GetComponentInChildren<damage_modifier_component>();
    }

    public void Enable_Damage_Modifier(float _modify_amount, float _lifetime)
    {
        if(null == damage_modifier)
        {
            damage_modifier = this.transform.root.GetComponentInChildren<damage_modifier_component>();
        }
        if (null != damage_modifier)
        {
            StartCoroutine(Damage_Modifier(_modify_amount, _lifetime));
        }
    }

    public void Enable_Speed_Modifier(float _modify_amount, float _lifetime)
    {
        if (null != movement_modifier)
        {
            print("Movement modifier exists");
            StartCoroutine(Speed_Modifier(_modify_amount, _lifetime));
        }
        else
        {
            print("Movement modifier does not exist");
        }
    }

    IEnumerator Damage_Modifier(float _amount, float _lifetime)
    {
        damage_modifier.Set_Damage_Modifior_Value(_amount);
        yield return new WaitForSeconds(_lifetime);
        damage_modifier.Set_Damage_Modifior_Value(1);
    }

    IEnumerator Speed_Modifier(float _amount, float _lifetime)
    {
        movement_modifier.Set_Speed_Modifier_Value(_amount);
        yield return new WaitForSeconds(_lifetime);
        movement_modifier.Set_Speed_Modifier_Value(1);
    }
}
