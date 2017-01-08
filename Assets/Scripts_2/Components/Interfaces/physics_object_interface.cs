using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface physics_object_interface : IEventSystemHandler {

    void On_Add_Force(Vector3 _direction, float _force);
}
