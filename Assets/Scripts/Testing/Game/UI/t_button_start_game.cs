using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class t_button_start_game : MonoBehaviour {

	public void Start_Game() {
        SceneManager.LoadScene(1);
    }
}
