using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Damage : MonoBehaviour {

    GameObject hitting_player;
    Rigidbody damage_rigidbody;

	// Use this for initialization
	void Start () {
        damage_rigidbody = GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        Character_Controller character_controller = collision.gameObject.GetComponent<Character_Controller>();
        Health health = collision.gameObject.GetComponent<Health>();
        if(health != null && character_controller == null)
        {
            if(damage_rigidbody != null && damage_rigidbody.velocity.magnitude > 0.3f)
            {
                health.Modify_Health(-10);
            }
        }
    }
}
