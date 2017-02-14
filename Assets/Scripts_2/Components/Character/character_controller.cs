using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(input_recorder_component))]
public class character_controller : MonoBehaviour, input_button_interface
{
    private Vector3 attack_direction;

    public float lunge_min_value, lunge_max_value;

    [SerializeField]
    private GameObject player_camera;

    [SerializeField]
    private float directional_move_distance = 0.0f;
    [SerializeField]
    private float button_hold_time_minimum = 0.0f;
    [SerializeField]
    private Animator character_animator;

    private bool weapon_equipped;

    [SerializeField]
    private float activation_time_delay = 0.025f;
    private float next_activation_time;

    private bool can_attack = true;

    int attack_counter = 1;

    rage_component rage;
    
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        rage = GetComponent<rage_component>();
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("player_ignore_layer"), LayerMask.NameToLayer("player_layer"));
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("player_ignore_layer"), LayerMask.NameToLayer("player_ignore_layer"));
    }

    private void OnLevelWasLoaded(int level)
    {
        GameObject spawn = GameObject.FindGameObjectWithTag("spawn_point");
        if (null != spawn)
        {
            this.transform.position = spawn.transform.position;
            this.transform.rotation = spawn.transform.rotation;
        }
        else
        {
            this.transform.position = Vector3.zero;
        }

        rage_component.global_rage_component.current_rage = 0.0f;
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
                if (attack_counter == 1)
                {
                    attack_direction = this.transform.right;
                    character_animator.SetTrigger("swipe_right");
                    next_activation_time = current_time + activation_time_delay;
                }
                else if(attack_counter == 2)
                {
                    attack_direction = -this.transform.right;
                    character_animator.SetTrigger("swipe_left");
                    next_activation_time = current_time + activation_time_delay;
                    attack_counter = 0;
                }
                attack_counter++;
                //Move_Into_Range();
                //attack downs
                
            }
            else if(_action_button == action_buttons.secondary_button && _action_button_state == action_button_states.down)
            {
                RaycastHit hit_object = Get_Object();
                if (null != hit_object.collider)
                {
                    Flick_Up_Object(hit_object.collider.gameObject);
                }
            }
            /*else if(action_buttons.flip_button == _action_button && action_button_states.down == _action_button_state)
            {
                RaycastHit hit_object = Get_Object();
                if(null != hit_object.collider)
                {
                    StartCoroutine(Lunge_Flip(hit_object));
                }
                //attack_direction = this.transform.right;
                //character_animator.SetTrigger("thrust");
                //next_activation_time = current_time + activation_time_delay;
                //Move_Into_Range();
            }*/
            else if(action_buttons.use_button == _action_button && action_button_states.down == _action_button_state)
            {
                //SendMessage("Elemental_Attack");
                if (rage.current_rage >= rage.attack_cost)
                {
                    BroadcastMessage("Activate_Special_Attack");
                }
            }

            if(_action_button == action_buttons.flip_button && _action_button_state == action_button_states.down)
            {
                time_controller_component.time_controller.Warp_Time(0.025f, 0.55f);
            }
            else if(_action_button == action_buttons.flip_button && _action_button_state == action_button_states.up)
            {
                time_controller_component.time_controller.Warp_Time(1.0f, 300f);
            }
        }
    }
    
    RaycastHit Get_Object()
    {
        Ray ray = new Ray(player_camera.transform.position, player_camera.transform.forward);
        RaycastHit[] hit_outs = Physics.SphereCastAll(ray, 0.25f, lunge_max_value);
        //Physics.SphereCast(ray, 0.25f, out hit_out, lunge_max_value);
        //Physics.Raycast(ray, out hit_out, lunge_max_value);
        for (int i = 0; i < hit_outs.Length; i++)
        {
            if (null != hit_outs[i].collider && null != hit_outs[i].collider.GetComponent<health_component>())
            {
                return hit_outs[i];
            }
        }
        return hit_outs[0];
    }

    public Vector3 Get_Attack_Direction()
    {
        return attack_direction;
    }

    public void Move_Into_Range()
    {
        if (null != player_camera)
        {
            RaycastHit hit_out;
            Ray ray = new Ray(player_camera.transform.position, player_camera.transform.forward);
            RaycastHit[] hit_outs = Physics.SphereCastAll(ray, 0.25f, lunge_max_value);
            //Physics.SphereCast(ray, 0.25f, out hit_out, lunge_max_value);
            //Physics.Raycast(ray, out hit_out, lunge_max_value);
            for (int i = 0; i < hit_outs.Length; i++) {
                if (null != hit_outs[i].collider && null != hit_outs[i].collider.GetComponent<health_component>())
                {
                    float distance = Vector3.Distance(hit_outs[i].point, this.transform.position);
                    if (distance >= lunge_min_value && distance <= lunge_max_value)
                    {
                        Vector3 target = hit_outs[i].point + (-this.transform.forward * 1.25f);
                        StartCoroutine(Lunge(target));
                        break;
                    }
                }
            }
        }
    }

    IEnumerator Lunge(Vector3 _target_position)
    {
        float end_time = Time.fixedTime + 0.5f; //do lunge for 2 seconds
        _target_position.y = this.transform.position.y;
        Disable_Collision_And_Rigidbody();
        while(Time.fixedTime < end_time)
        {
            Vector3 current_position = this.transform.position;
            current_position = Vector3.LerpUnclamped(this.transform.position, _target_position, 15.0f * Time.deltaTime);
            this.transform.position = current_position;
            yield return new WaitForEndOfFrame();
        }
        Enable_Collision_And_Rigidbody();
    }

    void Flick_Up_Object(GameObject _object)
    {
        _object.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    IEnumerator Lunge_Flip(RaycastHit _hit_object)
    {
        Vector3 target_position = _hit_object.point + (-this.transform.forward * 1.15f);
        float end_time = Time.fixedTime + 0.5f; //do lunge for 2 seconds
        target_position.y = this.transform.position.y;
        Disable_Collision_And_Rigidbody();
        while (Time.fixedTime < end_time)
        {
            Vector3 current_position = this.transform.position;
            current_position = Vector3.LerpUnclamped(this.transform.position, target_position, 15.0f * Time.deltaTime);
            this.transform.position = current_position;
            yield return new WaitForEndOfFrame();
        }
        Enable_Collision_And_Rigidbody();
        StartCoroutine(Flip_Object(_hit_object));
    }

    IEnumerator Flip_Object(RaycastHit _hit_object)
    {
        Time.timeScale = 0.25f;
        Time.fixedDeltaTime = 0.005f;
        attack_direction = this.transform.up;
        _hit_object.collider.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
        next_activation_time = Time.fixedTime + activation_time_delay;
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }

    void Disable_Collision_And_Rigidbody()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    void Enable_Collision_And_Rigidbody()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
    }
}
