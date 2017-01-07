using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
public enum controller_types {keyboard, xbox_360};
public enum action_buttons { primary_button, secondary_button, jump_button, interact_button, use_button };
public enum action_button_states { up, down, held};

public class input_component : MonoBehaviour
{
    private static readonly ExecuteEvents.EventFunction<input_button_interface> s_on_button_input 
        = delegate (input_button_interface _handler, BaseEventData _data){};

    private static readonly ExecuteEvents.EventFunction<movement_input_interface> s_on_movement_input
        = delegate (movement_input_interface _handler, BaseEventData _data){};

    private static readonly ExecuteEvents.EventFunction<rotation_input_interface> s_on_rotation_input
        = delegate (rotation_input_interface _handler, BaseEventData _data){};

    private controller_types controller_type;
    private int controller_number;

    // Use this for initialization
    void Start()
    {
        cursor_display.Disable_Cursor();
    }

    //handle button presses and mouse input
    private void Update()
    {
        Handle_Rotation_Input();
        Handle_Button_Input();
    }

    //handle movement input
    void FixedUpdate()
    {
        Handle_Movement_Input();
    }

    //virtual functions to be overridden in derived classes (mouse keyboard input, gamepad input)
    protected virtual void Handle_Rotation_Input() { }

    protected virtual void Handle_Button_Input() { }

    protected virtual void Handle_Movement_Input() { }

    public void Set_Controller_Type(controller_types _controller_type)
    {
        controller_type = _controller_type;
    }

    public void Set_Controller_Number(int _controller_number)
    {
        controller_number = _controller_number;
    }

    protected void On_Movement_Axis(float _forward, float _sideways)
    {
        ExecuteEvents.Execute<movement_input_interface>(this.gameObject, null, (movement_input_interface _handler, BaseEventData _data) => _handler.On_Movement_Input(_forward, _sideways));
    }

    protected void On_Rotation_Axis(float _horizontal, float _vertical)
    {
        ExecuteEvents.Execute<rotation_input_interface>(this.gameObject, null, (rotation_input_interface _handler, BaseEventData _data) => _handler.On_Rotation_Input(_horizontal, _vertical));
    }

    protected void On_Button(action_buttons _input_action, action_button_states _action_button_state)
    {
        ExecuteEvents.Execute<input_button_interface>(this.gameObject, null, (input_button_interface _handler, BaseEventData _data) => _handler.On_Button_Input(_input_action, _action_button_state));
    }
}

