﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class t_player : MonoBehaviour {
    public enum input_types {none, mouse, gamepad};
    public input_types input_type = input_types.mouse;

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

    public int total_score = 0;
    public int score = 0;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
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
            Debug.LogError ("No t_weapon attached or hierarchy incorrect. Camera->weapon_position->weapon_object(contains t_weapon)");
        }
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

    void Handle_Mouse_Input() {
        //mouse input
        float horizontal_delta = Input.GetAxis("Horizontal_Keyboard");
        float vertical_delta = Input.GetAxis("Vertical_Keyboard");

        float mouse_x_delta = Input.GetAxis("Mouse X");
        float mouse_y_delta = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

        Move_Body(horizontal_delta, vertical_delta);
        Rotate_Camera(mouse_y_delta);
        Rotate_Body(mouse_x_delta);

        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }
    }

    void Handle_Gamepad_Input () {
        float horizontal_delta = Input.GetAxis("J1_Left_Stick_X_Axis");
        float vertical_delta =  Input.GetAxis("J1_Left_Stick_Y_Axis");

        float mouse_x_delta = Input.GetAxis("J1_Right_Stick_X_Axis");
        float mouse_y_delta = Input.GetAxis("J1_Right_Stick_Y_Axis");
        print(mouse_x_delta + " " + mouse_y_delta);

        if (Input.GetButtonDown("J1_A")) {
            Jump();
        }

        Move_Body(horizontal_delta, vertical_delta);
        Rotate_Camera(mouse_y_delta * 2); //2 is sensitivity
        Rotate_Body(mouse_x_delta * 2); //2 is sensitivity

        
        if (Input.GetButtonDown("J1_B")) {
            Attack();
        }
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
            score += _col.gameObject.GetComponent<t_loot_controller> ().Get_Loot_Coin_Value ();
            if(null != ui_stat_controller) {
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

    public void Add_Score(int _input) {
        total_score += _input;
        score += _input;
        Update_Stats();
    }

    public int Get_Score() {
        return score;
    }

    public void Update_Stats () {
        ui_stat_controller.Update_Coin_Text (score);
    }

    public void Set_Weapon_Force(float _new_strength) {
        if(null != weapon_script) {
            weapon_script.Set_Weapon_Strength(_new_strength);
        }
    }

    public float Get_Weapon_Force() {
        if(null != weapon_script) {
            return weapon_script.Get_Weapon_Strength();
        }
        UnityEngine.Debug.Log("No weapon set");
        return 0;
    }
}
