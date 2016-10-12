using UnityEngine;
using System.Collections;

public class t2_hitting_object : MonoBehaviour {
    
    [SerializeField]
    private GameObject hitting_object = null;

    public GameObject Get_Hitting_Object() {
        return hitting_object;
    }

    void OnCollisionEnter(Collision _col) {
        if(null != _col.gameObject.GetComponent<t_player>()) { //if player collides then set as hitting object
            hitting_object = _col.gameObject;
        }
        else if(null != _col.gameObject.GetComponent<t_weapon>()) { //if weapon collides then set its player parent as hitting object
            hitting_object = _col.gameObject.GetComponentInParent<t_player>().gameObject;
        }
        else if(null != _col.gameObject.GetComponent<t2_hitting_object>()) { //if other hitting object collides see if it has a hitting object and set that (should pass player along objects)
            if (null != _col.gameObject.GetComponent<t2_hitting_object>().Get_Hitting_Object()) {
                hitting_object = _col.gameObject.GetComponent<t2_hitting_object>().Get_Hitting_Object();
            }
        }
    }
}
