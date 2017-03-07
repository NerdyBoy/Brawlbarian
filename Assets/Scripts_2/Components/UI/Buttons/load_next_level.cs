using UnityEngine;
using System.Collections;

public class load_next_level : MonoBehaviour {

	public void Load_Level()
    {
        int current_level = Application.loadedLevel;
        Application.LoadLevel(current_level + 1);
    }

    public void Load_Level_By_Num(int _num)
    {
        if(_num == -1)
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        if (PlayerPrefs.GetInt("times_played") == 0)
        {
            PlayerPrefs.SetInt("times_played", 1);
            Application.LoadLevel(1);
        }
        else
        {
            Application.LoadLevel(_num);
        }
    }
}
