using UnityEngine;
using System.Collections;

public class hitting_component : MonoBehaviour {

    private void OnCollisionEnter(Collision _collision)
    {
        hit_tracking_component hit_tracker = _collision.gameObject.GetComponent<hit_tracking_component>();
        if(null != hit_tracker)
        {
            hit_tracker.Set_Hit_Root_Character(this.transform.root.GetComponentInChildren<character_controller>());
        }
    }
}
