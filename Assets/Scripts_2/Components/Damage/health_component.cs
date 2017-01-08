using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class health_component : MonoBehaviour, health_interface {

    private static readonly ExecuteEvents.EventFunction<destruction_interface> s_on_health_is_zero
        = delegate (destruction_interface _handler, BaseEventData _data) { };

    [SerializeField]
    private int health_value = 0;

    public void On_Modify_Health(int _amount)
    {
        health_value += _amount;
        ExecuteEvents.Execute<hit_tracking_interface>(this.gameObject, null, (hit_tracking_interface _handler, BaseEventData _data) => _handler.Increase_Hit_Root_Score(Mathf.Abs(_amount)));

        if(0.0f >= health_value)
        {
            ExecuteEvents.Execute<destruction_interface>(this.gameObject, null, (destruction_interface _handler, BaseEventData _data) => _handler.On_Health_Is_Zero());
        }
        else
        {
            //do something else
        }
    }
}
