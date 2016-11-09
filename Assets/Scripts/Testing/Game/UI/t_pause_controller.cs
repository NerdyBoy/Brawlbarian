using UnityEngine;
using System.Collections;

public class t_pause_controller : MonoBehaviour {

    public static t_pause_controller pause_controller = null;

    [SerializeField]
    private GameObject pause_display = null;

	// Use this for initialization
	void Start () {
	    if(null == pause_controller) {
            pause_controller = this;
        }
        else if(this != pause_controller) {
            Destroy(this.gameObject);
        }
	}

    public void Pause() {
        if(null != pause_display) {
            pause_display.SetActive(true);
        }
    }

    public void Unpause() {
        t_game_controller.game_controller.Unpause();
        if(null != pause_display) {
            pause_display.SetActive(false);
        }
    }
}
