using UnityEngine;
using System.Collections;

public class Rage : MonoBehaviour {

    public static Rage rage;

    public float current_rage = 0;
    public float total_rage = 0;
    public float max_rage = 100;

	// Use this for initialization
	void Start () {
        if(rage == null)
        {
            rage = this;
        }
	}

    public void Modify_Rage(float _amount)
    {
        current_rage += _amount;
        total_rage += _amount;
        if (UI_Rage.ui_rage != null)
        {
            UI_Rage.ui_rage.Update_Rage(current_rage, max_rage);
        }
    }
}
