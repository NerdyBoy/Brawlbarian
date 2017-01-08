using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class t_movement : MonoBehaviour {

    public Rigidbody movement_rigidbody = null;

    [SerializeField]
    private float forward_speed;
    [SerializeField]
    private float sideways_speed;

	// Use this for initialization
	void Start () {
        movement_rigidbody = GetComponent<Rigidbody>();
	}

    public void Move(movement_struct _movement) {
        Vector3 current_velocity = movement_rigidbody.velocity;
        Vector3 forward_movement = this.transform.forward * (_movement.vertical_delta * (forward_speed * Time.deltaTime));
        Vector3 sideways_movement = this.transform.right * (_movement.horizontal_delta * (sideways_speed * Time.deltaTime));
        Vector3 movement_vector = forward_movement + sideways_movement;
        movement_vector.y = current_velocity.y;
        movement_rigidbody.velocity = movement_vector;
    }
}
