using UnityEngine;
using System.Collections;

public class AI_Minion_Controller : MonoBehaviour {

    NavMeshAgent nav_mesh_agent;
    Animator minion_animator;
    Character_Controller player;
    float distance;
    float angle;
    public float turn_speed;
    public float move_speed;
    bool player_speed_modified = false;
    void Start()
    {
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        minion_animator = GetComponent<Animator>();
        nav_mesh_agent.speed = move_speed;
    }

    private void Update()
    {
        if(player == null)
        {
            Find_Player();
        }
        else
        {
            Turn_Toward_Player();
            Move_Toward_Player();
        }
    }

    void Find_Player()
    {
        if(player == null)
        {
            player = FindObjectOfType<Character_Controller>();
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
        nav_mesh_agent.SetDestination(player.transform.position + player.transform.forward * 1.8f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(player != null && other.gameObject == player.gameObject)
        {
            player.move_speed = 4;
            player_speed_modified = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player != null && other.gameObject == player.gameObject)
        {
            player.move_speed = 4;
            player_speed_modified = false;
        }
    }

    private void OnDestroy()
    {
        if(player_speed_modified == true && player != null)
        {
            player.move_speed = 4;
            player_speed_modified = false;
        }
    }
}
