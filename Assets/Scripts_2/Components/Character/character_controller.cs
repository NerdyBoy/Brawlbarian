using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(input_recorder_component))]
public class character_controller : MonoBehaviour, input_button_interface
{
    private Vector3 attack_direction;

    [SerializeField]
    private float directional_move_distance = 0.0f;
    [SerializeField]
    private float button_hold_time_minimum = 0.0f;
    [SerializeField]
    private Animator character_animator;
    
    private input_recorder_component input_record;

    private bool weapon_equipped;

    [SerializeField]
    private float activation_time_delay = 0.025f;
    private float next_activation_time;

    private bool can_attack = true;
    
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        input_record = GetComponent<input_recorder_component>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        GameObject spawn = GameObject.FindGameObjectWithTag("spawn_point");
        if(null != spawn)
        {
            this.transform.position = spawn.transform.position;
            this.transform.rotation = spawn.transform.rotation;
        }
    }

    void Weapon_Equipped(bool _weapon_equipped)
    {
        weapon_equipped = _weapon_equipped;
    }

    public void On_Button_Input(action_buttons _action_button, action_button_states _action_button_state)
    {
        if (true == weapon_equipped && game_state_controller.current_state_controller.Get_Current_State() == game_state_controller.game_states.in_play)
        {
            float current_time = Time.fixedTime;

            if (action_button_states.up == _action_button_state)
            {
                can_attack = true;
            }

            if(action_buttons.primary_button == _action_button && action_button_states.down == _action_button_state)
            {
                attack_direction = -this.transform.right;
                character_animator.SetTrigger("swipe_left");
                next_activation_time = current_time + activation_time_delay;
                //attack left
            }
            else if(action_buttons.secondary_button == _action_button && action_button_states.down == _action_button_state)
            {
                attack_direction = this.transform.right;
                character_animator.SetTrigger("swipe_right");
                next_activation_time = current_time + activation_time_delay;
                //attack right
            }
            else if(action_buttons.ternary_button == _action_button && action_button_states.down == _action_button_state)
            {
                print("THRUST");
                attack_direction = this.transform.forward;
                character_animator.SetTrigger("thrust");
                next_activation_time = current_time + activation_time_delay;
                //attack thrust
            }
            else if(action_buttons.scroll_up == _action_button && action_button_states.down == _action_button_state)
            {
                attack_direction = this.transform.up;
                character_animator.SetTrigger("swipe_up");
                next_activation_time = current_time + activation_time_delay;
                //attack up
            }
            else if(action_buttons.scroll_down == _action_button && action_button_states.down == _action_button_state)
            {
                attack_direction = -this.transform.up;
                character_animator.SetTrigger("swipe_down");
                next_activation_time = current_time + activation_time_delay;
                //attack down
            }

            /*float hold_time = input_record.Get_Button_Hold_Time();
            float average_magnitude = input_record.Get_Average_Magnitude();
            float current_time = Time.fixedTime;
            if (action_buttons.primary_button == _action_button &&
                ((hold_time > button_hold_time_minimum || average_magnitude > directional_move_distance) && current_time > next_activation_time)
                && true == can_attack)
            {
                can_attack = false;
                if (average_magnitude < directional_move_distance)
                {
                    //thrust
                    attack_direction = this.transform.forward;
                    character_animator.SetTrigger("thrust");
                    next_activation_time = current_time + activation_time_delay;
                }
                else
                {
                    Vector2 average_attack_vector = input_record.Get_Average();
                    if (Mathf.Abs(average_attack_vector.x) > Mathf.Abs(average_attack_vector.y))
                    {
                        //horizontal attack
                        if (average_attack_vector.x <= 0.0f)
                        {
                            attack_direction = -this.transform.right;
                            character_animator.SetTrigger("swipe_left");
                            next_activation_time = current_time + activation_time_delay;
                            //attack left
                        }
                        else
                        {
                            attack_direction = this.transform.right;
                            character_animator.SetTrigger("swipe_right");
                            next_activation_time = current_time + activation_time_delay;
                            //attack right
                        }
                    }
                    else
                    {
                        //vertical attack
                        if (average_attack_vector.y <= 0.0f)
                        {
                            attack_direction = -this.transform.up;
                            character_animator.SetTrigger("swipe_down");
                            next_activation_time = current_time + activation_time_delay;
                            //attack down
                        }
                        else
                        {
                            attack_direction = this.transform.up;
                            character_animator.SetTrigger("swipe_up");
                            next_activation_time = current_time + activation_time_delay;
                            //attack up
                        }
                    }
                }
            }
            else if(action_buttons.secondary_button == _action_button &&(true == can_attack))
            {
                BroadcastMessage("Launch_Equipped_Weapon");
            }*/
        }
    }
    
    public Vector3 Get_Attack_Direction()
    {
        return attack_direction;
    }
}
