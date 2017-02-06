using UnityEngine;
using System.Collections;

public class rage_component : MonoBehaviour {

    public float max_rage = 100.0f;
    public float current_rage = 0.0f;
    public float attack_cost = 100.0f;

    private void Start()
    {
        if(ui_rage_controller.rage_controller != null)
        {
            ui_rage_controller.rage_controller.Set_Max(max_rage / 100.0f);
            ui_rage_controller.rage_controller.Set_Current(current_rage / 100.0f);
        }
    }

    void Enable_Effect()
    {
        if(current_rage >= attack_cost)
        {
            current_rage -= attack_cost;
            if (null != ui_rage_controller.rage_controller)
            {
                ui_rage_controller.rage_controller.Set_Current(current_rage / 100.0f);
            }
            BroadcastMessage("Set_Effect_Active", true);
            
        }
    }

    void On_Impact()
    {
        if (current_rage < 100)
        {
            current_rage += 10;
        }
        if (null != ui_rage_controller.rage_controller)
        {
            ui_rage_controller.rage_controller.Set_Current(current_rage / 100.0f);
        }
    }

    void Elemental_Attack()
    {
        if(current_rage >= attack_cost)
        {
            current_rage -= attack_cost;
        }
    }
}
