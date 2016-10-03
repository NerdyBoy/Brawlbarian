using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class t_player : MonoBehaviour {

    enum input_types { mouse, gamepad};
    input_types input_type = input_types.mouse;

    private Rigidbody player_rigidbody;
    private GameObject camera_object;

    private GameObject weapon_position;
    private GameObject weapon_object;
    private t_weapon weapon_script;

    t_player_stat_controller ui_stat_controller = null;

    [SerializeField]
    private float jump_force = 1.0f;

    [SerializeField]
    private float forward_speed = 1.0f;
    [SerializeField]
    private float sideways_speed = 1.0f;

    public int coins = 0;

    // Use this for initialization
    void Start () {
        player_rigidbody = GetComponent<Rigidbody> ();
        player_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        camera_object = GetComponentInChildren<Camera> ().gameObject;

        weapon_script = GetComponentInChildren<t_weapon> ();
        if(null != weapon_script) {
            weapon_object = weapon_script.gameObject;
            weapon_position = weapon_object.transform.parent.gameObject;
            Ignore_All_Child_Colliders ();
        }
        else {
            Debug.LogError ("No t_weapon attached or heirarchy incorrect. Camera->weapon_position->weapon_object(contains t_weapon)");
        }
	}

    void Ignore_All_Child_Colliders () {
        Collider[] child_colliders = GetComponentsInChildren<Collider> ();
        for (int i = 0; i < child_colliders.Length; i++) {
            Physics.IgnoreCollision (this.GetComponent<Collider> (), child_colliders[i]);
        }
    }

    void Check_Input_Type () {
        //check if mouse input or gamepad input
    }
	
	// Update is called once per frame
	void Update () {
        if(input_types.mouse == input_type) {
            Handle_Mouse_Input ();
        }
        else if(input_types.gamepad == input_type) {
            Handle_Gamepad_Input ();
        }
	}

    void Handle_Mouse_Input() {
        //mouse input
        float horizontal_delta = Input.GetAxis ("Horizontal");
        float vertical_delta = Input.GetAxis ("Vertical");

        float mouse_x_delta = Input.GetAxis ("Mouse X");
        float mouse_y_delta = Input.GetAxis ("Mouse Y");

        if (Input.GetKeyDown (KeyCode.Space)) {
            Jump ();
        }

        Move_Body (horizontal_delta, vertical_delta);
        Rotate_Camera (mouse_y_delta);
        Rotate_Body (mouse_x_delta);

        if (Input.GetMouseButtonDown (0)) {
            Attack ();
        }
    }

    void Handle_Gamepad_Input () {
        //gamepad input
    }

    void Rotate_Camera(float _y_delta) {
        camera_object.transform.Rotate (new Vector3 (-_y_delta, 0, 0));
    }

    void Rotate_Body(float _x_delta) {
        this.transform.Rotate (new Vector3 (0, _x_delta, 0));
    }

    void Move_Body (float _horizontal_delta, float _vertical_delta) {
        Vector3 current_velocity = player_rigidbody.velocity;
        Vector3 forward_movement = this.transform.forward * (_vertical_delta * (forward_speed * Time.deltaTime));
        Vector3 sideways_movement = this.transform.right * (_horizontal_delta * (sideways_speed * Time.deltaTime));
        Vector3 movement_vector = forward_movement + sideways_movement;
        movement_vector.y = current_velocity.y;
        player_rigidbody.velocity = movement_vector;
    }

    void Jump () {
        if(true == Can_Jump ()) {
            player_rigidbody.AddForce (new Vector3 (0, jump_force, 0), ForceMode.Impulse);
        }
    }

    bool Can_Jump () {
        //raycast down to check for floor
        return true;
    }

    void Attack () {
        if(null != weapon_script) {
            weapon_script.Attack ();
        }
    }

    void OnCollisionEnter(Collision _col) {
        if(null != _col.gameObject.GetComponent<t_loot_controller> ()) {
            print ("Loot object collided");
            coins += _col.gameObject.GetComponent<t_loot_controller> ().Get_Loot_Coin_Value ();
            if(null != ui_stat_controller) {
                print ("Have stat controller");
                Update_Stats ();
            }
            Destroy (_col.gameObject);
        }
    }

    public t_player_stat_controller Get_Stat_Controller () {
        return ui_stat_controller;
    }

    public void Set_Stat_Controller(t_player_stat_controller _stat_controller) {
        ui_stat_controller = _stat_controller;
    }

    public void Update_Stats () {
        ui_stat_controller.Update_Coin_Text (coins);
    }
}
