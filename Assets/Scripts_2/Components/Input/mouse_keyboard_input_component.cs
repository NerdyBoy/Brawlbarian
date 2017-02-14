﻿using UnityEngine;
using System.Collections;

public class mouse_keyboard_input_component : input_component {

    protected override void Handle_Rotation_Input()
    {
        base.Handle_Rotation_Input();

        float vertical_rotation = Input.GetAxis("Mouse Y");
        float horizontal_rotation = Input.GetAxis("Mouse X");

        if (0.0f != vertical_rotation || 0.0f != horizontal_rotation)
        {
            On_Rotation_Axis(horizontal_rotation, vertical_rotation);
        }
        
    }

    protected override void Handle_Button_Input()
    {
        base.Handle_Button_Input();

        //hacky pause shit, fix this
        if(game_state_controller.current_state_controller.Get_Current_State() == game_state_controller.game_states.in_play)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                pause_controller.controller.Enable_Pause_Menu();
            }
        }
        else if(game_state_controller.current_state_controller.Get_Current_State() == game_state_controller.game_states.paused)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                print("DISABLING");
                pause_controller.controller.Disable_Pause_Menu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            On_Button(action_buttons.jump_button, action_button_states.down);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            On_Button(action_buttons.interact_button, action_button_states.down);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            On_Button(action_buttons.use_button, action_button_states.down);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            On_Button(action_buttons.flip_button, action_button_states.down);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            On_Button(action_buttons.flip_button, action_button_states.up);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            On_Button(action_buttons.special_button, action_button_states.down);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            On_Button(action_buttons.sprint_button, action_button_states.down);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            On_Button(action_buttons.sprint_button, action_button_states.up);
        }

        //left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            On_Button(action_buttons.primary_button, action_button_states.down);
        }
        else if (Input.GetMouseButton(0))
        {
            On_Button(action_buttons.primary_button, action_button_states.held);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            On_Button(action_buttons.primary_button, action_button_states.up);
        }

        //right mouse button
        if(Input.GetMouseButtonDown(1))
        {
            On_Button(action_buttons.secondary_button, action_button_states.down);
        }
        else if (Input.GetMouseButton(1))
        {
            On_Button(action_buttons.secondary_button, action_button_states.held);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            On_Button(action_buttons.secondary_button, action_button_states.up);
        }

        //middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            On_Button(action_buttons.ternary_button, action_button_states.down);
        }

        //handle scroll wheel
        float scroll_wheel = Input.GetAxis("Mouse ScrollWheel");
        if(0.0f != scroll_wheel)
        {
            if(scroll_wheel < 0.0f)
            {
                On_Button(action_buttons.scroll_down, action_button_states.down);
            }
            else
            {
                On_Button(action_buttons.scroll_up, action_button_states.down);
            }
        }
        
    }

    protected override void Handle_Movement_Input()
    {
        base.Handle_Movement_Input();

        float forward_movement = Input.GetAxis("Vertical");
        float sideways_movement = Input.GetAxis("Horizontal");

        if (0.0f != forward_movement || 0.0f != sideways_movement)
        {
            On_Movement_Axis(forward_movement, sideways_movement);
        }
    }
}
