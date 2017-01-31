using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class movement_component : MonoBehaviour, movement_input_interface, input_button_interface {

    bool sprinting;

    input_component input;

    Rigidbody character_rigidbody;

    [SerializeField]
    private float movement_speed;

    [SerializeField]
    private float jump_force;

    private movement_speed_modifier speed_modifier;

    private void Start()
    {
        character_rigidbody = GetComponent<Rigidbody>();
        character_rigidbody.freezeRotation = true;

        input = GetComponent<input_component>();

        speed_modifier = GetComponent<movement_speed_modifier>();
    }

    void On_Jump(action_buttons _action_button, action_button_states _action_button_state)
    {
        if(_action_button == action_buttons.jump_button && _action_button_state == action_button_states.down
            && game_state_controller.current_state_controller.Get_Current_State() == game_state_controller.game_states.in_play)
        {
            Jump();
        }
    }

    public void On_Movement_Input(float _forward, float _sideways)
    {
        if (game_state_controller.current_state_controller.Get_Current_State() == game_state_controller.game_states.in_play)
        {
            if (null == character_rigidbody)
            {
                character_rigidbody = GetComponent<Rigidbody>();
            }
            if (null != character_rigidbody)
            {
                float modifier = 1.0f;
                if (null != speed_modifier)
                {
                    modifier = speed_modifier.Get_Speed_Modifier_Value();
                }

                Vector3 velocity = character_rigidbody.velocity;
                float velocity_y = velocity.y;

                Vector3 forward_movement = this.transform.forward * ((_forward * (movement_speed * modifier)) * Time.deltaTime);
                Vector3 sideways_movement = this.transform.right * ((_sideways * (movement_speed * modifier)) * Time.deltaTime);

                velocity = (forward_movement + sideways_movement);
                velocity.y = velocity_y;
                character_rigidbody.velocity = velocity;
            }
        }
    }

    void Jump()
    {
        //raycast down to test for ground
            //if ground found
                //add jump force
    }

    public void On_Button_Input(action_buttons _button_action, action_button_states _button_state)
    {
        if(action_buttons.sprint_button == _button_action)
        {
            if(action_button_states.down == _button_state)
            {
                sprinting = true;
                speed_modifier.Set_Speed_Modifier_Value(2);
            }
            else if(action_button_states.up == _button_state)
            {
                sprinting = false;
                speed_modifier.Set_Speed_Modifier_Value(1);
            }
        }
        
    }
}
