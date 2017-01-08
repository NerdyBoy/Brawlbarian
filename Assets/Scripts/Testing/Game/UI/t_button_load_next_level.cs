using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class t_button_load_next_level : MonoBehaviour {

	public void Load_Next_Level() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
