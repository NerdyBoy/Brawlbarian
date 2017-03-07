using UnityEngine;
using System.Collections;

public class explosion : base_special {

    public float sphere_radius = 10.0f;
    public float blast_force = 4500.0f;
    CharacterController character_reference;
    private void Start()
    {
        character_reference = this.transform.root.GetComponent<CharacterController>();
    }

    public override void Activate_Attack()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit[] hit_outs = Physics.SphereCastAll(ray, sphere_radius);
        for(int i = 0; i < hit_outs.Length; i++)
        {
            Vector3 vec = this.transform.position - hit_outs[i].transform.position;
            if(Vector3.Cross(this.transform.right, vec).y > 0)
            {
                Rigidbody rigidbody = hit_outs[i].transform.GetComponent<Rigidbody>();
                Health health = hit_outs[i].transform.GetComponent<Health>();

                if(rigidbody != null)
                {
                    Vector3 force = (hit_outs[i].transform.position - this.transform.position).normalized * 50;
                    rigidbody.AddForce(force, ForceMode.Impulse);
                }
                if(health != null)
                {
                    health.Modify_Health(-25);
                }
            }
        }
    }

    
}
