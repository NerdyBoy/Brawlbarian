using UnityEngine;
using System.Collections;

public class t_hitting_object : MonoBehaviour {

    private GameObject hitter;
    
    public void Set_Hitter(GameObject _hitter) {
        hitter = _hitter;
    }

    public GameObject Get_Hitter() {
        return hitter;
    }

}
