using UnityEngine;
using System.Collections;

public class load_next_level : MonoBehaviour {

	public void Load_Level()
    {
        int current_level = Application.loadedLevel;
        Application.LoadLevel(current_level + 1);
    }
}
