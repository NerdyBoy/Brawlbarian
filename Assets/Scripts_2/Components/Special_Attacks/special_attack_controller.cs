using UnityEngine;
using System.Collections;

public class special_attack_controller : MonoBehaviour {

    base_special[] specials;

	// Use this for initialization
	void Start () {
        specials = GetComponentsInChildren<base_special>();
	}
	
    void Activate_Special_Attack()
    {
        int attack_index = Random.Range(0, specials.Length);
        specials[attack_index].Activate_Attack();
    }
}
