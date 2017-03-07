using UnityEngine;
using System.Collections;

public class Reset_Player_Prefs : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        PlayerPrefs.SetInt("times_played", 0);
    }
}
