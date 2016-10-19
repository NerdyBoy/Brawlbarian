using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_end_scene_controller : MonoBehaviour {

    [SerializeField]
    private Text final_score_text = null;
    private t_player player = null;

    void OnLevelWasLoaded() {
        StartCoroutine(Get_Player());
    }

    IEnumerator Get_Player() {
        while (null == player) {
            player = GameObject.FindGameObjectWithTag("player").GetComponent<t_player>();
            yield return new WaitForSeconds(0);
        }
        final_score_text.text = "Final score: " + player.total_score;
    }
}
