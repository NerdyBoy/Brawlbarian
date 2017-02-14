using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class text_trigger : MonoBehaviour {

    public Text text_element;

    public string replace_text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            string new_string = replace_text.Replace(",", "\n");
            text_element.text = new_string;
        }
    }
}
