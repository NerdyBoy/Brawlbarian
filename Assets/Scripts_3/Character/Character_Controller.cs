using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Character_Controller : MonoBehaviour {

    public Camera player_camera;
    public Weapon equipped_weapon;
    public GameObject weapon_parent;
    public special_attack_controller special_attack;
    private Animator player_animator;
    private Rigidbody player_rigidbody;
    private Rage rage;
    public float move_speed;
    public float turn_speed;
    public bool blocking;
    private int attack_counter = 1;
    public bool is_attacking = false;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("player_layer"), LayerMask.NameToLayer("player_ignore_layer"));
        Equip_Weapon(equipped_weapon);
        player_animator = GetComponent<Animator>();
        player_rigidbody = GetComponent<Rigidbody>();
        rage = GetComponent<Rage>();
	}

    private void OnLevelWasLoaded(int level)
    {
        GameObject spawn_object = FindObjectOfType<Player_Spawn>().gameObject;
        if(spawn_object != null)
        {
            this.transform.position = spawn_object.transform.position;
            this.transform.rotation = spawn_object.transform.rotation;
        }
    }

    public void Equip_Weapon(Weapon _weapon_to_equip)
    {
        if(_weapon_to_equip != null && weapon_parent != null)
        {
            equipped_weapon.transform.parent = weapon_parent.transform;
            equipped_weapon.transform.position = Vector3.zero;

            equipped_weapon.transform.localPosition = new Vector3(-0.019f, 0.148f, 0.04f);
            equipped_weapon.transform.localPosition += equipped_weapon.transform.up.normalized * 0.5f;
            equipped_weapon.transform.localEulerAngles  = new Vector3(0.0f, -100.472f, 140.0f);
            Physics.IgnoreCollision(equipped_weapon.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
	
	// Update is called once per frame
	void Update () {
        Handle_Keyboard_Input();
        Handle_Mouse_Input();
	}

    void Handle_Keyboard_Input()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Move(horizontal, vertical);
        if(Input.GetKeyDown(KeyCode.E))
        {
            Special_Attack();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause_controller.controller.Toggle_Pause();
        }
    }

    void Move(float _h, float _v)
    {
        Vector3 initial_velocity = player_rigidbody.velocity;

        Vector3 forward = this.transform.forward * (_v * (move_speed));
        Vector3 right = this.transform.right * (_h * (move_speed));

        Vector3 move = forward + right;
        move.y = initial_velocity.y;

        //player_rigidbody.velocity = move * (Time.deltaTime * 0.6f);
        player_rigidbody.MovePosition(this.transform.position + (move * Time.deltaTime));
    }

    void Handle_Mouse_Input()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        Rotate_Head(vertical);
        Rotate_Body(horizontal);

        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Rotate_Head(float _x)
    {
        player_camera.transform.Rotate(new Vector3(-_x * turn_speed * Time.deltaTime, 0.0f, 0.0f));
        Clamp_Head();
    }

    void Clamp_Head()
    {
        Vector3 current_rotation = player_camera.transform.rotation.eulerAngles;

        if(current_rotation.x > 45 && current_rotation.x < 200)
        {
            if(current_rotation.x < 180)
            {
                current_rotation.x = 45;
            }
            else
            {
                current_rotation.x = 200;
            }
        }
        current_rotation.z = 0;
        player_camera.transform.rotation = Quaternion.Euler(current_rotation);
    }

    void Rotate_Body(float _y)
    {
        this.transform.Rotate(new Vector3(0.0f, _y * turn_speed * Time.deltaTime, 0.0f));
    }

    void Attack()
    {
        if (player_animator != null && equipped_weapon != null)
        {
            if (attack_counter % 2 == 0)
            {
                player_animator.SetTrigger("swipe_right");
                StartCoroutine(Turn_On_Weapon_Collision(0.25f));
            }
            else
            {
                player_animator.SetTrigger("swipe_left");
                StartCoroutine(Turn_On_Weapon_Collision(0.25f));
            }

            attack_counter++;
        }
    }

    IEnumerator Turn_On_Weapon_Collision(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        equipped_weapon.Toggle_Trigger(false);
    }

    void Special_Attack()
    {
        if(special_attack != null && rage != null && rage.current_rage >= rage.max_rage)
        {
            special_attack.Activate_Special_Attack();
            rage.current_rage = 0;
        }
    }

    //not good to have here
    void AI_Attack_Start()
    {
        is_attacking = true;
    }

    void AI_Attack_End()
    {
        is_attacking = false;
    }

}
