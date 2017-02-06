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
        GameObject flame_thrower = Instantiate(_flame_object, this.transform.position, Quaternion.identity) as GameObject;
        flame_thrower.transform.parent = cam_comp.transform.parent;
        flame_thrower.transform.up = cam_comp.transform.forward;
        yield return new WaitForSeconds(lifetime);
        Destroy(flame_thrower);
    }
}
