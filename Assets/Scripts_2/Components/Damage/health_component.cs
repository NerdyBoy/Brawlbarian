using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class health_component : MonoBehaviour, health_interface {

    private static readonly ExecuteEvents.EventFunction<destruction_interface> s_on_health_is_zero
        = delegate (destruction_interface _handler, BaseEventData _data) { };

    [SerializeField]
    private float health_value = 0;

    public void On_Modify_Health(float _amount)
    {
        health_value += _amount;

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
