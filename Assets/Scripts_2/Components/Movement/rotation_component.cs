using UnityEngine;
using System.Collections;
using System;

public class rotation_component : MonoBehaviour, rotation_input_interface, input_button_interface {

    private input_component input;

    [SerializeField]
    private GameObject vertical_rotate_object;
    [SerializeField]
    private GameObject horizontal_rotate_object;

    private bool primary_button_down;
    private float button_down_speed_offset = 1.0f;

    private fps_camera_clamp_component camera_clamp;

    private void Start()
    {
        camera_clamp = this.gameObject.AddComponent<fps_camera_clamp_component>();
        
    }

    public void On_Rotation_Input(float _horizontal, float _vertical)
    {
        if (game_state_controller.current_state_controller.Get_Current_State() == game_state_controller.game_states.in_play)
        {
            if (null != vertical_rotate_object)
            {
                vertical_rotate_object.transform.Rotate(new Vector3(0.0f, _horizontal * button_down_speed_offset, 0.0f));
            }

            if (null != vertical_rotate_object)
            {
                horizontal_rotate_object.transform.Rotate(new Vector3(-_vertical * button_down_speed_offset, 0.0f, 0.0f));
                camera_clamp.Clamp_Camera();
            }
        }
    }

    public void On_Button_Input(action_buttons _button_action, action_button_states _button_state)
    {
        if(action_buttons.primary_button == _button_action && action_button_states.down == _button_state)
        {
            primary_button_down = true;
            button_down_speed_offset = 0.2f;
        }
        else if(action_buttons.primary_button == _button_action && action_button_states.up == _button_state)
        {
            primary_button_down = false;
            button_down_speed_offset = 1;
        }
    }
}
