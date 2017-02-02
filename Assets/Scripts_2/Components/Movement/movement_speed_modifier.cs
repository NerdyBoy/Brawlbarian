using UnityEngine;
using System.Collections;

public class movement_speed_modifier : MonoBehaviour {

    [SerializeField]
    private float speed_modifier_value;

	public void Set_Speed_Modifier_Value(float _new_value)
    {
        speed_modifier_value = _new_value;
        BroadcastMessage("Update_Speed", speed_modifier_value, SendMessageOptions.DontRequireReceiver);
    }

    public float Get_Speed_Modifier_Value()
    {
        return speed_modifier_value;
    }
}
