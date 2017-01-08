using UnityEngine;
using System.Collections;

[RequireComponent(typeof(t_movement_4))]
[RequireComponent(typeof(t_health_4))]
public class t_player_4 : MonoBehaviour {

    //scripts
    private t_health_4 health_component;

    void Start()
    {
        health_component = GetComponent<t_health_4>();
        
        if (null != health_component)
        {
            health_component.on_health_changed += Health_Check;
        }
    }

    void Health_Check(int _health, int _change_in_health)
    {

    }
}
