using UnityEngine;
using System.Collections;

public class destroy_on_impact : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (destroyed_fragment_cleaner.fragment_cleaner != null)
        {
            destroyed_fragment_cleaner.fragment_cleaner.Add_Fragment(this.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision _col)
    {
        if(_col.gameObject.CompareTag("weapon"))
        {
            if(destroyed_fragment_cleaner.fragment_cleaner != null)
            {
                destroyed_fragment_cleaner.fragment_cleaner.Remove_Self(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
