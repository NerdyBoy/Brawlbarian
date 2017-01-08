using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class t_button_quit_to_main_menu : MonoBehaviour {

	public void Quit_To_Main_Menu() {
        SceneManager.LoadScene(0);
    }
}
