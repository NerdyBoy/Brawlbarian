using UnityEngine;
using System.Collections;

public class quit_to_main : MonoBehaviour {

	public void Load_Main()
    {
		Time.timeScale = 1;
        Application.LoadLevel(0);
    }
}
