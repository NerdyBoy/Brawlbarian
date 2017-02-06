using UnityEngine;
using System.Collections;

public class explosion : base_special {

    public float sphere_radius = 10.0f;
    public float blast_force = 2.0f;
    character_controller character_reference;
    private void Start()
    {
        character_reference = this.transform.root.GetComponent<character_controller>();
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
                physics_object_component phys = hit_outs[i].transform.GetComponent<physics_object_component>();
                if(phys != null)
                {
                    hit_outs[i].collider.SendMessage("Set_Hit_Root_Character", character_reference, SendMessageOptions.DontRequireReceiver);
                    hit_outs[i].collider.SendMessage("On_Modify_Health", -10, SendMessageOptions.DontRequireReceiver);
                    phys.On_Add_Force((hit_outs[i].transform.position - this.transform.position).normalized * blast_force);
                }
            }
        }
    }

    
}
