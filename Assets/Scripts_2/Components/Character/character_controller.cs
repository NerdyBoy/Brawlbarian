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
    
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
                character_animator.SetTrigger("swipe_down");
                next_activation_time = current_time + activation_time_delay;
                Move_Into_Range();
                //attack down
            }
            else if(action_buttons.secondary_button == _action_button && action_button_states.down == _action_button_state)
            {
                attack_direction = this.transform.right;
                character_animator.SetTrigger("thrust");
                next_activation_time = current_time + activation_time_delay;
                //attack forward
            }
            else if(action_buttons.special_button == _action_button && action_button_states.down == _action_button_state)
            {
                SendMessage("Elemental_Attack");
            }
        }
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
            Physics.Raycast(ray, out hit_out, lunge_max_value);
            if(null != hit_out.collider && null != hit_out.collider.GetComponent<health_component>())
            {
                float distance = Vector3.Distance(hit_out.point, this.transform.position);
                if(distance >= lunge_min_value)
                {
                    Vector3 target = hit_out.point + (-this.transform.forward * 1.5f);
                    StartCoroutine(Lunge(target));
                }
            }
        }
    }

    IEnumerator Lunge(Vector3 _target_position)
    {
        float end_time = Time.fixedTime + 0.5f; //do lunge for 2 seconds
        Disable_Collision_And_Rigidbody();
        while(Time.fixedTime < end_time)
        {
            Vector3 current_position = this.transform.position;
            current_position = Vector3.Lerp(this.transform.position, _target_position, 5.0f * Time.deltaTime);
            this.transform.position = current_position;
            yield return new WaitForEndOfFrame();
        }
        Enable_Collision_And_Rigidbody();
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
