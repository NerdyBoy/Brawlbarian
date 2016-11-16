using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class t_save_load_game : MonoBehaviour {
   
    void Start() {
        t_save_data data = new t_save_data();
        data.Create_Blank_Table();
    }

    public static void Save_Game(string name, int score) {
        if(File.Exists(Application.persistentDataPath + "/highscores.gd")) {
            t_save_data data = new t_save_data();
            BinaryFormatter binary_formatter = new BinaryFormatter();
            FileStream file_stream = File.Open(Application.persistentDataPath + "/highscores.gd", FileMode.Open);
            binary_formatter.Serialize(file_stream, data);
            file_stream.Close();

        }
        else {

        }
    }

    public static void Load_Game() {
        t_save_data data;
        if(File.Exists(Application.persistentDataPath + "/highscores.gd")) {
            BinaryFormatter binary_formatter = new BinaryFormatter();
            FileStream file_stream = File.Open(Application.persistentDataPath + "/highscores.gd", FileMode.Open);
            data = (t_save_data)binary_formatter.Deserialize(file_stream);
            file_stream.Close();
        }
    }
}



public class t_save_data : MonoBehaviour {
    List<KeyValuePair<string, int>> highscore_table;

    public void Create_Blank_Table() {
        highscore_table = new List<KeyValuePair<string, int>>();
        for(int i = 0; i < 10; i++) {
            KeyValuePair<string, int> _score = new KeyValuePair<string, int>("AAA", i * 500);
            highscore_table.Add(_score);
        }
        bool sorting_finished = true;
        while(true == sorting_finished) {
            sorting_finished = Sort_Highscore_Table();
        }

        for(int i = 0; i < 10; i++) {
            print(highscore_table[i].Key + " " + highscore_table[i].Value);
        }
    }

    bool Sort_Highscore_Table() {
        bool swap_required = false;
        for(int i = 0; i < highscore_table.Count() - 1; i++) {
            KeyValuePair<string, int> temp;
            if(highscore_table[i].Value > highscore_table[i + 1].Value) {
                temp = highscore_table[i + 1];
                highscore_table[i + 1] = highscore_table[i];
                highscore_table[i] = temp;
                swap_required = true;
            }
            
        }
        return swap_required;
    }

}