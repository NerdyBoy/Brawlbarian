using UnityEngine;
using System.Collections;

public class AI_Weapon : MonoBehaviour
{

    BoxCollider weapon_collider;
    public int damage_amount;

    private void Start()
    {
        weapon_collider = GetComponent<BoxCollider>();
        if (weapon_collider != null)
        {
            weapon_collider.isTrigger = true;
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("weapon_layer"), LayerMask.NameToLayer("weapon_ignore_layer"));
        }
    }

    public void Toggle_Trigger(bool _is_trigger)
    {
        if (weapon_collider != null)
        {
            weapon_collider.isTrigger = _is_trigger;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        Rigidbody col_rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (health != null)
        {
            Character_Controller character_controller = collision.gameObject.GetComponent<Character_Controller>();
            Toggle_Trigger(true);
            health.Modify_Health(-damage_amount);
            if (Rage.rage != null)
            {
                Rage.rage.Modify_Rage(10);
            }
            if (col_rigidbody != null)
            {
                col_rigidbody.AddForce(this.transform.root.forward * (5 * col_rigidbody.mass), ForceMode.Impulse);
            }
        }
    }
}
