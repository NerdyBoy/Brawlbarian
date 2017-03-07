using UnityEngine;
using System.Collections;

public class Boss_Health : Health {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Modify_Health(int _amount)
    {
        if (Application.loadedLevel == 5)
        {
            base.Modify_Health(_amount);
        }
    }
}
