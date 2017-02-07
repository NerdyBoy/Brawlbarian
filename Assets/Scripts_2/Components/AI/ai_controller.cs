using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class ai_controller : MonoBehaviour {

    public float ai_speed;
    public float ai_attack_delay;
    public float ai_attack_distance;
    public float ai_attack_cooldown;
    public AnimationCurve ai_speed_curve;
    character_controller player_character;
    NavMeshAgent navmesh_agent;
    private bool can_attack = true;

	// Use this for initialization
	void Start () {
        player_character = FindObjectOfType<character_controller>();
        navmesh_agent = GetComponent<NavMeshAgent>();
        navmesh_agent.speed = ai_speed;
	}
	
	// Update is called once per frame
	void Update () {
        navmesh_agent.SetDestination(player_character.transform.position);
        navmesh_agent.speed = navmesh_agent.remainingDistance;
        print(navmesh_agent.remainingDistance);
        if((navmesh_agent.remainingDistance < ai_attack_distance))
        {
            print("ATTACK");
            StartCoroutine(AI_Attack());
        }
	}

    IEnumerator AI_Attack()
    {
        can_attack = true;
        yield return new WaitForSeconds(ai_attack_delay);
        if(navmesh_agent.remainingDistance < ai_attack_distance)
        {
            print("ATTTAAAACCCCKKKK");
            player_character.SendMessage("On_Modify_Health", -100);
        }
        can_attack = false;
        yield return new WaitForSeconds(ai_attack_cooldown);

    }
}
