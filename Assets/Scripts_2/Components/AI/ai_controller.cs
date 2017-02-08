﻿using UnityEngine;
using System.Collections;

public class ai_controller : MonoBehaviour
{

    public float ai_speed;
    public float initial_attack_delay;
    public float ai_attack_delay;
    private bool can_attack = false;
    private bool should_attack = false;
    character_controller player_character;
    NavMeshAgent navmesh_agent;
    Animator ai_animator;
    ai_weapon weapon;

    // Use this for initialization
    void Start()
    {
        player_character = FindObjectOfType<character_controller>();
        navmesh_agent = GetComponent<NavMeshAgent>();
        navmesh_agent.speed = ai_speed;
        ai_animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<ai_weapon>();
        StartCoroutine(Initial_Attack_Delay_Wait());
        navmesh_agent.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player_character == null)
        {
            player_character = FindObjectOfType<character_controller>();
        }
        if (player_character != null)
        {
            Vector3 ai_to_player = player_character.transform.position - this.transform.position;
            navmesh_agent.SetDestination(player_character.transform.position + -(ai_to_player.normalized * 2));
            //navmesh_agent.speed = 3.5f;
        }
    }

    IEnumerator AI_Attack()
    {
        while (should_attack == true)
        {
            
            ai_animator.SetTrigger("attack");
            yield return new WaitForSeconds(ai_attack_delay);
        }
    }

    void Attack_Start()
    {
        if (can_attack == true)
        {
            weapon.attacking = true;
        }
    }

    void Attack_End()
    {
        weapon.attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            should_attack = true;
            if (can_attack == true)
            {
                StartCoroutine(AI_Attack());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            should_attack = false;
            StopCoroutine(AI_Attack());
        }
    }

    IEnumerator Initial_Attack_Delay_Wait()
    {
        yield return new WaitForSeconds(initial_attack_delay);
        can_attack = true;
    }
}
