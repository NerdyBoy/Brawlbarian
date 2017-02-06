using UnityEngine;
using System.Collections;

public class fire_scale_test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
	}
}
