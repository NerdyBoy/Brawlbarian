using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_highscore : MonoBehaviour {

    private int hard_score = 15000;
    int highscore;

    [SerializeField]
    private Text highscore_text;
    [SerializeField]
    private Text current_score_text;

    private character_controller character;

	// Use this for initialization
	void Start () {
        if (true == PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
        }
        else
        {
            PlayerPrefs.SetInt("highscore", 15000);
        }
        StartCoroutine(Find_Player());
        highscore_text.text = "Highscore: " + highscore;
	}

    IEnumerator Find_Player()
    {
        while (null == character)
        {
            character = GameObject.FindGameObjectWithTag("player").GetComponent<character_controller>();
            yield return new WaitForFixedUpdate();
        }
        Update_Scores();
    }
    
    void Update_Scores()
    {
        current_score_text.text = "Current Score: " + character.GetComponent<character_score_component>().Get_Score().ToString();
        if (character.GetComponent<character_score_component>().Get_Score() > highscore)
        {
            StartCoroutine(Update_High_Score());
        }
    }

    IEnumerator Update_High_Score()
    {
        yield return new WaitForSeconds(3);
        highscore_text.text = "Highscore: " + character.GetComponent<character_score_component>().Get_Score().ToString();
        PlayerPrefs.SetInt("highscore", character.GetComponent<character_score_component>().Get_Score());
        highscore_text.color = Color.red;
    }
}
