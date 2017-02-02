using UnityEngine;
using System.Collections;

public class destruction_object_component : destruction_component {

    [SerializeField]
    private GameObject replacement_object;

    public float explosive_force;
    public float explosion_radius;

    public override void On_Health_Is_Zero()
    {
        if(null != replacement_object)
        {
            Rigidbody object_rigidbody = GetComponent<Rigidbody>();
            GameObject destroyed = Instantiate(replacement_object, this.transform.position, this.transform.rotation) as GameObject;
            for(int i = 0; i < destroyed.transform.childCount; i++)
            {
                Rigidbody rbody = destroyed.transform.GetChild(i).gameObject.GetComponentInChildren<Rigidbody>();
                if (null != rbody && object_rigidbody != null)
                {
                    rbody.AddExplosionForce(explosive_force, this.transform.position, explosion_radius);
                }
            }

        }
        Destroy(this.gameObject);
    }
}
