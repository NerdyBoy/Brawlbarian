using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_player_stat_controller : MonoBehaviour {

    private Text coin_text;
    private t_player linked_player;

	// Use this for initialization
	void Start () {
        coin_text = GetComponentInChildren<Text> ();
        StartCoroutine (Get_Player ());
	}
	
	public void Update_Coin_Text (int _new_value) {
        coin_text.text = "Coins: " + _new_value.ToString ();
    }

    IEnumerator Get_Player () {
        while(Object.FindObjectsOfType<t_player>().Length == 0) {
            print (Object.FindObjectsOfType<t_player>().Length);
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
