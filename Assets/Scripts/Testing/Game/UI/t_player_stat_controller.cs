using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_player_stat_controller : MonoBehaviour {

    private Text score_text;
    private t_player linked_player;

	// Use this for initialization
	void Start () {
        score_text = GetComponentInChildren<Text> ();
        StartCoroutine (Get_Player ());
	}
	
	public void Update_Coin_Text (string _new_value) {
        score_text.text = "Score: " + _new_value;
    }

    IEnumerator Get_Player () {
        while(Object.FindObjectsOfType<t_player>().Length == 0) {
            yield return new WaitForSeconds (0); //better way of doing this? Time based?
        }
        Link_Player ();
    }

    private void Link_Player () {
        t_player[] all_players = FindObjectsOfType (typeof (t_player)) as t_player[];
        for(int player = 0; player < all_players.Length; player++) {
            if(null == all_players[player].Get_Stat_Controller ()) {
                all_players[player].Set_Stat_Controller (this);
                linked_player = all_players[player];
                linked_player.Update_Stats ();
                return;
            }
        }
        this.gameObject.SetActive (false);
    }
}
