using UnityEngine;
using System.Collections;

public class special_attack_controller : MonoBehaviour {

    base_special[] specials;
    int next_special = 0;


	// Use this for initialization
	void Start () {
        specials = GetComponentsInChildren<base_special>();
        Decide_Next_Special();
	}

    public void Decide_Next_Special()
    {
        next_special = Random.Range(0, specials.Length);
        if (UI_Special_Attack.ui_special_attack != null)
        {
            UI_Special_Attack.ui_special_attack.Set_Image(next_special);
        }
    }
	
    public void Activate_Special_Attack()
    {
        specials[next_special].Activate_Attack();
        Decide_Next_Special();
    }
}
