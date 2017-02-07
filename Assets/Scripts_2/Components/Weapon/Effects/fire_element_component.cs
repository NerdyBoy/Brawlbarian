using UnityEngine;
using System.Collections;

public class fire_element_component : elemental_base_component {

    public GameObject fire_spread_prefab;

    public override void Activate_On_Collision(Collision _collision)
    {
        base.Activate_On_Collision(_collision);
        if(true == _collision.collider.CompareTag("flammable"))
        {
            GameObject fire = Instantiate(fire_spread_prefab, Vector3.zero, Quaternion.identity) as GameObject;
            //fire.transform.parent = _collision.collider.transform;
            //fire.transform.localPosition = Vector3.zero;
        }
    }
}
