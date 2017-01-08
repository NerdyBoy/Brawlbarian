using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(t_input_4))]
public class t_movement_4 : MonoBehaviour {

    //scripts
    private t_input_4 input_component;

    //components
    private Camera camera_component = null;

    //exposed variables
    public float horizontal_turn_speed = 1.0f;
    public float vertical_turn_speed = 1.0f;

    public float horizontal_move_speed = 1.0f;
    public float vertical_move_speed = 1.0f;

    // Use this for initialization
    void Start () {
        camera_component = transform.root.GetComponentInChildren<Camera>();
        input_component = GetComponent<t_input_4>();

        if (null != input_component)
        {
            input_component.on_mouse_moved += Rotate;
            input_component.on_move_keys_pressed += Move;
        }

    }

    // Update is called once per frame
    void Update () {
	
	}

    void Move(Vector2 _keyboard_delta)
    {
    }

    void Rotate(Vector2 _mouse_delta)
    {
        if (null != camera_component)
        {
            camera_component.transform.Rotate(new Vector3((-_mouse_delta.y * (vertical_turn_speed * Time.deltaTime)), 0, 0));
        }
        this.transform.Rotate(new Vector3(0, (_mouse_delta.x * (horizontal_turn_speed * Time.deltaTime)), 0));
    }
}
