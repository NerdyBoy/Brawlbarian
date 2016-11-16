using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class t_save_load_game : MonoBehaviour {
    private static string filename = "/highscore.gd";
    public static t_save_data saved_data = null;
    
    public static void Load_Data() {
        if (File.Exists(Application.persistentDataPath + filename)) {
            BinaryFormatter binary_formatter = new BinaryFormatter();
            FileStream file_stream = File.Open(Application.persistentDataPath + filename, FileMode.Open);
            saved_data = (t_save_data)binary_formatter.Deserialize(file_stream);
            file_stream.Close();
        }
        else {
            Generate_Blank_Save_Data();
            Load_Data();
        }
    }

    public static void Save_Data(string _name, int _score) {
        if(File.Exists(Application.persistentDataPath + filename)) {
            Load_Data();
            Update_Highscore_Data(_name, _score);
            BinaryFormatter binary_formatter = new BinaryFormatter();
            FileStream file_stream = File.Open(Application.persistentDataPath + filename, FileMode.Open);
            binary_formatter.Serialize(file_stream, saved_data);
            file_stream.Close();
        }
        else {
            Generate_Blank_Save_Data();
            Save_Data(_name, _score);
        }
    }

    public static void Generate_Blank_Save_Data() {
        saved_data = new t_save_data();
        saved_data.Generate_Blank();
        BinaryFormatter binary_formatter = new BinaryFormatter();
        FileStream file_stream = File.Open(Application.persistentDataPath + filename, FileMode.OpenOrCreate);
        binary_formatter.Serialize(file_stream, saved_data);
        file_stream.Close();
    }

    public static void Update_Highscore_Data(string _name, int _score) {
        saved_data.Update_Highscore_Table(_name, _score);
    }

    public static List<KeyValuePair<string, int>> Get_Highscore_Table() {
        return saved_data.Get_Highscore_Table();
    }
            
}

[System.Serializable]
public class t_save_data{
    [SerializeField]
    int number_of_scores = 9;
    [SerializeField]
    private List<KeyValuePair<string, int>> highscore_table;

    public void Generate_Blank() {
        highscore_table = new List<KeyValuePair<string, int>>();
        for(int i = 0; i < number_of_scores; i++) {
            KeyValuePair<string, int> score = new KeyValuePair<string, int>("AAA", i * 500);
            highscore_table.Add(score);
        }
        Sort_Highscore_Table();
    }

    public void Update_Highscore_Table(string _name, int _score) {

       for(int i = 0; i < highscore_table.Count; i++) {
            if(_score > highscore_table[i].Value) {
                KeyValuePair<string, int> score = new KeyValuePair<string, int>(_name, _score);
                highscore_table.Add(score);
                break;
            }
        }
        Sort_Highscore_Table();
        Prune_Highscore_Table();
        Sort_Highscore_Table();
        
    }

    public void Sort_Highscore_Table() {
        bool swap_required = true;
        while(true == swap_required) {
            bool swapped = false;
            for(int i = 0; i < highscore_table.Count - 1; i++) {
                if(highscore_table[i].Value < highscore_table[i + 1].Value) {
                    KeyValuePair<string, int> temp = highscore_table[i + 1];
                    highscore_table[i + 1] = highscore_table[i];
                    highscore_table[i] = temp;
                    swapped = true;
                }
            }
            swap_required = swapped;
        }
    }

    public void Prune_Highscore_Table() {
        if (highscore_table.Count > number_of_scores) {
            for(int i = 0; i < highscore_table.Count; i++) {
                if(i > number_of_scores) {
                    highscore_table.RemoveAt(i);
                    Prune_Highscore_Table();
                }
            }
        }
    }

    public List<KeyValuePair<string, int>> Get_Highscore_Table() {
        return highscore_table;
    }

}