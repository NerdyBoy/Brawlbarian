using UnityEngine;
using System.Collections;

public class ai_weapon : MonoBehaviour {

    public bool attacking = false;

	// Use this for initialization
	void Start () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("player"))
        {
            if(attacking == true)
            {
                other.gameObject.SendMessage("On_Player_Modify_Health", -100);
            }
        }
    }
}
