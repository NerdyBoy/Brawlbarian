using UnityEngine;
using System.Collections;

public struct weapon_struct {
    public Vector2 mouse_delta;
    public int button_index;

    public weapon_struct(Vector2 _mouse_delta, int _button_index) {
        mouse_delta = _mouse_delta;
        button_index = _button_index;
    }
}

public struct movement_struct {
    public float horizontal_delta;
    public float vertical_delta;
}

public struct rotation_struct {
    public GameObject rotation_object;

    public float horizontal_delta;
    public float vertical_delta;

    public bool rotate_vertical;
    public bool rotate_horizontal;

    public float min_vertical_clamp;
    public float max_vertical_clamp;

    public float min_horizontal_clamp;
    public float max_horizontal_clamp;

    public rotation_struct(GameObject _rotation_object, float _horizontal_delta, float _vertical_delta, bool _rotate_vertical, bool _rotate_horizontal, 
        float _min_vertical_clamp, float _max_vertical_clamp, float _min_horizontal_clamp, float _max_horizontal_clamp) {
        rotation_object = _rotation_object;
        horizontal_delta = _horizontal_delta;
        vertical_delta = _vertical_delta;
        rotate_horizontal = _rotate_horizontal;
        rotate_vertical = _rotate_vertical;
        min_vertical_clamp = _min_vertical_clamp;
        max_vertical_clamp = _max_vertical_clamp;
        min_horizontal_clamp = _min_horizontal_clamp;
        max_horizontal_clamp = _max_horizontal_clamp;
    }
}

[RequireComponent(typeof(t_movement))]
[RequireComponent(typeof(t_rotation))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CapsuleCollider))]
public class t_player : MonoBehaviour {
    public enum input_types {none, mouse, gamepad};
    public input_types input_type = input_types.mouse;
    
    private GameObject camera_object;

    public GameObject axe_object;

    private GameObject weapon_position;

    t_player_stat_controller ui_stat_controller = null;

    [SerializeField]
    private float jump_force = 1.0f;

    [SerializeField]
    private float forward_speed = 1.0f;
    [SerializeField]
    private float sideways_speed = 1.0f;

    public int total_score = 0;
    public int score = 0;

    private bool look_enabled = true;

    Vector3 initial_position;
    Quaternion initial_rotation;
    Transform initial_parent;


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
        initial_position = this.transform.position;
        initial_rotation = this.transform.rotation;
        camera_object = GetComponentInChildren<Camera> ().gameObject;
	}

    public void Assign_Controller(input_types _type) {
        input_type = _type;
    }

    void OnLevelWasLoaded() {
        score = 0;
        this.transform.position = GameObject.FindGameObjectWithTag("spawn_point").transform.position;
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
        if (true == t_game_controller.game_controller.Get_Round_In_Progress()) {
            if (input_types.mouse == input_type) {
                Handle_Mouse_Input();
            }
            else if (input_types.gamepad == input_type) {
                Handle_Gamepad_Input();
            }
            
        }
	}

    void Set_Look_Enabled(bool _enabled) {
        look_enabled = _enabled;
    }

    void Handle_Mouse_Input() {
        //mouse input
        if (true == look_enabled) {
            float horizontal_delta = Input.GetAxis("Horizontal_Keyboard");
            float vertical_delta = Input.GetAxis("Vertical_Keyboard");

            float mouse_x_delta = Input.GetAxis("Mouse X");
            float mouse_y_delta = Input.GetAxis("Mouse Y");

            movement_struct movement_values;
            movement_values.horizontal_delta = horizontal_delta;
            movement_values.vertical_delta = vertical_delta;
            SendMessage("Move", movement_values);
            
            rotation_struct head_rotation_values = new rotation_struct(camera_object, -mouse_x_delta, -mouse_y_delta, false, true, 0.0f, 0.0f, 0.0f, 0.0f);
            SendMessage("Rotate_Object", head_rotation_values);

            rotation_struct body_rotation_values = new rotation_struct(this.gameObject, mouse_x_delta, mouse_y_delta, true, false, 0.0f, 0.0f, 0.0f, 0.0f);
            SendMessage("Rotate_Object", body_rotation_values);
            

            if (Input.GetMouseButton(0)) {
                BroadcastMessage("Start_Attack", 0, SendMessageOptions.DontRequireReceiver);
                BroadcastMessage("Calculate_Swing", new weapon_struct(new Vector2(mouse_x_delta, mouse_y_delta), 0));
            }

            else if (Input.GetMouseButtonDown(1)) {
                BroadcastMessage("Calculate_Swing", new weapon_struct(new Vector2(mouse_x_delta, mouse_y_delta), 1));
                

            }
        }
    }

    void Handle_Gamepad_Input () {
        if (true == look_enabled) {
            float horizontal_delta = Input.GetAxis("J1_Left_Stick_X_Axis");
            float vertical_delta = Input.GetAxis("J1_Left_Stick_Y_Axis");

            float mouse_x_delta = Input.GetAxis("J1_Right_Stick_X_Axis");
            float mouse_y_delta = Input.GetAxis("J1_Right_Stick_Y_Axis");

            movement_struct movement_values;
            movement_values.horizontal_delta = horizontal_delta;
            movement_values.vertical_delta = vertical_delta;
            SendMessage("Move", movement_values);

            rotation_struct head_rotation_values = new rotation_struct(camera_object, mouse_x_delta, mouse_y_delta, false, true, 0.0f, 0.0f, 0.0f, 0.0f);
            SendMessage("Rotate_Object", head_rotation_values);

            rotation_struct body_rotation_values = new rotation_struct(this.gameObject, mouse_x_delta, mouse_y_delta, true, false, 0.0f, 0.0f, 0.0f, 0.0f);
            SendMessage("Rotate_Object", body_rotation_values);


            if (Input.GetButtonDown("J1_B")) {
                //Attack(0);
            }
        }
    }

    public t_player_stat_controller Get_Stat_Controller () {
        return ui_stat_controller;
    }

    public void Set_Stat_Controller(t_player_stat_controller _stat_controller) {
        ui_stat_controller = _stat_controller;
    }

    public void Add_Score(int _input) {
        total_score += _input;
        score += _input;
        Update_Stats();
    }

    public int Get_Score() {
        return score;
    }

    public void Update_Stats () {
        ui_stat_controller.Update_Coin_Text (score.ToString());
    }
}
