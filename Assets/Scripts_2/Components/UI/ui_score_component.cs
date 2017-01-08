using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_score_component : MonoBehaviour {

    private Text score_text;

	// Use this for initialization
	void Start () {
        score_text = GetComponent<Text>();
	}
	
	void Update_Score(int _amount)
    {
        score_text.text = "Score: " + _amount.ToString();
    }
}
