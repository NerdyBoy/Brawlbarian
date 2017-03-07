using UnityEngine;
using System.Collections;

public class pause_controller : MonoBehaviour {

    public static pause_controller controller;
    public GameObject pause_object;

	// Use this for initialization
	void Start () {
	    if(controller == null)
        {
            controller = this;
        }
	}

    public void Toggle_Pause()
    {
        if(pause_object.activeSelf == true)
        {
            Disable_Pause_Menu();
        }
        else
        {
            Enable_Pause_Menu();
        }
    }
	
	public void Enable_Pause_Menu()
    {
        pause_object.SetActive(true);
        cursor_display.Enable_Cursor();
        game_state_controller.current_state_controller.Switch_State(game_state_controller.game_states.paused);
        Time.timeScale = 0;
    }

    public void Disable_Pause_Menu()
    {
        pause_object.SetActive(false);
        Time.timeScale = 1;
        cursor_display.Disable_Cursor();
        game_state_controller.current_state_controller.current_state = game_state_controller.game_states.in_play;
    }
}
