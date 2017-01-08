using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class movement_speed_powerup_component : MonoBehaviour {

    /*private static readonly ExecuteEvents.EventFunction<character_powerup_interface> s_on_collision_powerup
        = delegate (character_powerup_interface _handler, BaseEventData _data) { };*/

    [SerializeField]
    private float amount = 1.0f;
    [SerializeField]
    private float lifetime = 1.0f;

    void OnTriggerEnter(Collider _collider)
    {
        if (null != _collider.GetComponent<character_controller>())
        {
            ExecuteEvents.Execute<character_powerup_interface>(_collider.gameObject, null, (character_powerup_interface _handler, BaseEventData _data) => _handler.Enable_Speed_Modifier(amount, lifetime));
            Destroy(this.gameObject);
        }
    }
}
