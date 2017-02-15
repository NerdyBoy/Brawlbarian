using UnityEngine;
using System.Collections;

public class level_load_component : MonoBehaviour {

    public int level_number = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (level_number != -1)
            {
                Application.LoadLevel(level_number);
            }
            else
            {
                if (Application.loadedLevel + 1 == Application.levelCount)
                {
                    Application.LoadLevel(0);
                }
                else
                {
                    Application.LoadLevel(Application.loadedLevel + 1);
                }
            }
        }
    }
}
