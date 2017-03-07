using UnityEngine;
using System.Collections;

public class AI_Boss_Controller : MonoBehaviour {

    NavMeshAgent nav_mesh_agent;
    AI_Boss_Health boss_health;
    Character_Controller player;
    public float turn_speed = 1;
    float angle = 0;
    float distance = 0;
    AI_Weapon boss_weapon;
    Animator ai_animator;

    private void Start()
    {
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        boss_health = GetComponent<AI_Boss_Health>();
        ai_animator = GetComponent<Animator>();
        boss_weapon = GetComponentInChildren<AI_Weapon>();

        if(boss_weapon != null)
        {
            Physics.IgnoreCollision(boss_weapon.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
    }

    void Find_Player()
    {
        if(player == null)
        {
            player = FindObjectOfType<Character_Controller>();
        }
    }

    private void Update()
    {
        if (boss_health.health > 0)
        {
            if (player == null)
            {
                Find_Player();
            }
            if (player != null)
            {
                Turn_Toward_Player();
            }
            if (nav_mesh_agent != null && distance > 3)
            {
                Move_Toward_Player();
            }
            else if (distance < 3)
            {
                Attack_Player();
            }
        }
    }

    void Turn_Toward_Player()
    {
        Vector3 vector_between = player.transform.position - this.transform.position;
        this.transform.forward = Vector3.Lerp(this.transform.forward, vector_between, turn_speed * Time.deltaTime);

        distance = vector_between.magnitude;
        angle = Vector3.Angle(-this.transform.forward, player.transform.forward);
    }

    void Move_Toward_Player()
    {
        nav_mesh_agent.SetDestination(player.transform.position + player.transform.forward * 2f);
    }

    void Attack_Player()
    {
        if(ai_animator != null)
        {
            ai_animator.SetTrigger("attack");
        }
    }

    void AI_Damage_Start()
    {
        ai_animator.SetBool("being_damaged", true);
    }

    void AI_Damage_End()
    {
        ai_animator.SetBool("being_damaged", false);
    }

    void AI_Attack_Start()
    {
        ai_animator.SetBool("attacking", true);
        //is_attacking = true;
        boss_weapon.Toggle_Trigger(false);
    }

    void AI_Attack_End()
    {
        ai_animator.SetBool("attacking", false);
        //is_attacking = false;
        boss_weapon.Toggle_Trigger(true);
    }
}
