using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_score_component : MonoBehaviour {

    character_score_component character_score;
    private Text score_text;
    

	// Use this for initialization
	void Start () {
        character_score = FindObjectOfType<character_score_component>();
        score_text = GetComponent<Text>();
        int score = 0;
        score_text.text = score.ToString();
        if(character_score != null)
        {
            Update_Score(character_score.Get_Score());
        }
	}
	
	public void Update_Score(int _amount)
    {
        score_text.text = "Score: " + _amount.ToString();
    }
}
