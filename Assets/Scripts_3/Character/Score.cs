using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    public static Score score;

    public int player_score;

	// Use this for initialization
	void Start () {
	    if(score == null)
        {
            score = this;
        }
	}

    public void Update_Score(int _amount)
    {
        player_score += _amount;
        if(UI_Score.ui_score != null)
        {
            UI_Score.ui_score.Update_Score(player_score);
        }
    }
}
