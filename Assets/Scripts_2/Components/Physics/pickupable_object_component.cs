using UnityEngine;
using System.Collections;

[RequireComponent(typeof(physics_object_component))]
public class pickupable_object_component : MonoBehaviour {

    physics_object_component physics_object;

    private void Start()
    {
        physics_object = GetComponent<physics_object_component>();
    }

    public void Launch_Object(Vector3 _direction, float _force)
    {
        /*if(null != physics_object)
        {
            physics_object.On_Add_Force(_direction, _force);
        }*/
    }
}
