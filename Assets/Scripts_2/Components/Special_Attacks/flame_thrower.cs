using UnityEngine;
using System.Collections;

public class flame_thrower : base_special {

    Camera cam_comp;
    public GameObject _flame_object;
    public float lifetime = 5.0f;

    private void Start()
    {
        cam_comp = this.transform.root.GetComponentInChildren<Camera>();
    }

    public override void Activate_Attack()
    {
        base.Activate_Attack();
        StartCoroutine(Flame_Attack());
    }

    IEnumerator Flame_Attack()
    {
        GameObject fire_object = Instantiate(_flame_object, cam_comp.transform.position, cam_comp.transform.rotation) as GameObject;
        float end_time = Time.fixedTime + lifetime;
        while(Time.fixedTime < end_time)
        {
            Ray ray = new Ray(cam_comp.transform.position, cam_comp.transform.forward);
            RaycastHit[] hit_out = Physics.RaycastAll(ray, 5);
            for(int i = 0; i < hit_out.Length; i++)
            {
                Flammable_Object flammable_object = hit_out[i].collider.GetComponent<Flammable_Object>();
                if(flammable_object != null && flammable_object.is_burning == false)
                {
                    flammable_object.Ignite();
                }
            }
            yield return new WaitForEndOfFrame();
        }
        Destroy(fire_object);
    }
}
