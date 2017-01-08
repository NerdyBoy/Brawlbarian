using UnityEngine;
using System.Collections;

public class damage_modifier_component : MonoBehaviour {

    [SerializeField]
    private float damage_modifier_amount;

	public float Get_Damage_Modifier_Value()
    {
        return damage_modifier_amount;
    }

    public void Set_Damage_Modifior_Value(float _amount)
    {
        damage_modifier_amount = _amount;
        this.transform.root.BroadcastMessage("Update_Damage", damage_modifier_amount);
    }
}
