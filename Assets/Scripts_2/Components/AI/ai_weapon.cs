using UnityEngine;
using System.Collections;

public class ai_weapon : MonoBehaviour {

    public bool attacking = false;

	// Use this for initialization
	void Start () {
        Physics.IgnoreCollision(GetComponent<Collider>(), this.transform.root.GetComponent<Collider>());
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("player") && attacking == true)
        {
            Application.LoadLevel(4);
        }
    }
}
