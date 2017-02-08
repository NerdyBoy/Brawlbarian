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
        Application.LoadLevel(_num);
    }
}
