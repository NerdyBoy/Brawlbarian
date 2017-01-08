using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class t_player_destroyer : MonoBehaviour {
    
    void OnEnable() {
        SceneManager.sceneLoaded += Destroy_All_Players;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= Destroy_All_Players;
    }

    void Destroy_All_Players(Scene scene, LoadSceneMode scene_mode) {
        t_player[] players = FindObjectsOfType<t_player>();
        print(players.Length);
        if (players.Length > 0) {
            for (int i = 0; i < players.Length; i++) {
                Destroy(players[i].gameObject);
            }
        }
    }
	
}
