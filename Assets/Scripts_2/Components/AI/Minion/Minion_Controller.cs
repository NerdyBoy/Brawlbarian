using UnityEngine;
using System.Collections;

public class Minion_Controller : MonoBehaviour {

    Rigidbody minion_rigidbody;
    GameObject enemy_target;
    NavMeshAgent nav_agent;
    public float attack_distance;
    public float inactive_time;

	// Use this for initialization
	void Start () {
        minion_rigidbody = GetComponent<Rigidbody>();
        nav_agent = GetComponent<NavMeshAgent>();
	}

    void Find_Target()
    {
        Character_Controller[] characters = FindObjectsOfType<Character_Controller>();
        for(int i =  0; i < characters.Length; i++)
        {
            enemy_target = characters[Random.Range(0, characters.Length)].gameObject;
        }
    }

    void Move_To_Target()
    {
        if(nav_agent != null && nav_agent.enabled == true && nav_agent.isOnNavMesh == true && enemy_target != null)
        {
            this.transform.forward = -(this.transform.position - enemy_target.transform.position);
            nav_agent.SetDestination(enemy_target.transform.position + (enemy_target.transform.forward * 1.5F));
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(enemy_target == null)
        {
            Find_Target();
        }
        if(enemy_target != null)
        {
            Move_To_Target();
        }
	}

    IEnumerator Fly_Off(GameObject _hitting_object)
    {
        nav_agent.enabled = false;
        minion_rigidbody.constraints = RigidbodyConstraints.None;
        minion_rigidbody.AddForce(_hitting_object.transform.root.transform.forward * 5, ForceMode.Impulse);
        yield return new WaitForSeconds(inactive_time);
        minion_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        nav_agent.enabled = true;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        Weapon weapon = _collision.gameObject.GetComponent<Weapon>();
        if (null != weapon)
        {
            StartCoroutine(Fly_Off(_collision.gameObject));
        }
    }
}
