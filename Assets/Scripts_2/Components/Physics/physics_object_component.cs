using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class physics_object_component : MonoBehaviour, physics_object_interface {

    private Rigidbody physics_object_rigidbody;
    private hit_tracking_component hit_tracker;

    private void Start()
    {
        physics_object_rigidbody = GetComponent<Rigidbody>();
        hit_tracker = GetComponent<hit_tracking_component>();
    }

    public void On_Add_Force(Vector3 _direction, float _force)
    {
        if (null != physics_object_rigidbody)
        {
            physics_object_rigidbody.AddForce(_direction * _force, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision _collision)
    {
        if(null != hit_tracker && null != hit_tracker.Get_Hit_Root_Character_Score())
        {
            ExecuteEvents.Execute<hit_tracking_interface>(_collision.gameObject, null, (hit_tracking_interface _handle, BaseEventData _data) => _handle.Set_Hit_Root_Character(hit_tracker.Get_Hit_Root_Character_Score()));
        }
    }


}
