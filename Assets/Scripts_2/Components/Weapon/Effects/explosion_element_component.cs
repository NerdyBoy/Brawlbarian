using UnityEngine;
using System.Collections;

public class explosion_element_component : elemental_base_component {

    public float radius = 500.0f;
    public float force = 4000.0f;

    public override void Activate_On_Collision(Collision _collision)
    {
        base.Activate_On_Collision(_collision);
        Explode();
    }

    void Explode()
    {
        RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, radius, this.transform.forward);
        print(hits.Length);
        for(int i = 0; i < hits.Length; i++)
        {
            if(null != hits[i].collider)
            {
                physics_object_component physics_object = hits[i].collider.GetComponent<physics_object_component>();
                if(null != physics_object)
                {
                    physics_object.On_Add_Force(( physics_object.transform.position - this.transform.position) * radius);
                }
            }
        }
    }
}
