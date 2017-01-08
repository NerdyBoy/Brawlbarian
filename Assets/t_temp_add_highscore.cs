using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class t_temp_add_highscore : MonoBehaviour {

    public string name = "JOE";
    public int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Add_High_Score() {
        t_save_load_game.Load_Data();
        t_save_load_game.Save_Data(name, score);
        List<KeyValuePair<string, int>> highscore_table = t_save_load_game.Get_Highscore_Table();
        for(int i = 0; i< highscore_table.Count; i++) {
            print(highscore_table[i].Key + " " + highscore_table[i].Value);
        }
    }
}
