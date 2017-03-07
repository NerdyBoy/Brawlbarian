using UnityEngine;
using System.Collections;

public class base_special : MonoBehaviour {

	public virtual void Activate_Attack()
    {
        Rage.rage.current_rage = 0;
    }
}
