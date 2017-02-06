using UnityEngine;
using System.Collections;

public class torch_fire_spread : MonoBehaviour {

    public GameObject light_replacement;
    public GameObject fire_spread_component;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor") == true)
        {
            if (Random.Range(0, 100) < 2.4f)
            {
                Instantiate(fire_spread_component, this.transform.position, Quaternion.identity);
            }
        }
        
    }
}
