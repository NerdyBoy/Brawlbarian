using UnityEngine;
using System.Collections;

public class t_destructible_light : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision _col) {
        t_weapon weapon = _col.gameObject.GetComponent<t_weapon>();
        print(_col.gameObject.name);
        Destroy(this.gameObject);
        
    }
}
