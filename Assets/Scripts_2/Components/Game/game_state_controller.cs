using UnityEngine;
using System.Collections;

public class game_state_controller : MonoBehaviour
{

    public enum game_states { round_start, in_play, paused, round_end };

    public static game_state_controller current_state_controller;
    private game_states current_state;

    // Use this for initialization
    void Start()
    {
        if (null != current_state_controller)
        {
            Destroy(current_state_controller.gameObject);
        }
        current_state_controller = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Switch_State(game_states _state)
    {
        if(game_states.round_start == current_state)
        {
            if(game_states.in_play == _state)
            {
                SendMessage("Begin_Play");
                current_state = game_states.in_play;
            }
        }
        else if(game_states.in_play == current_state)
        {
            if(game_states.paused == _state)
            {
                SendMessage("Begin_Pause");
                current_state = game_states.paused;
            }
            if(game_states.round_end == _state)
            {
                SendMessage("Begin_Round_End");
                current_state = game_states.round_end;
            }
        }
        else if(game_states.paused == current_state)
        {
            if(game_states.paused == _state)
            {
                SendMessage("End_Pause");
                current_state = game_states.in_play;
            }
        }
        else if(game_states.round_end == current_state)
        {
            if(game_states.round_start == _state)
            {
                SendMessage("Begin_Round_End");
                current_state = game_states.round_start;
            }
        }
    }

    public game_states Get_Current_State()
    {
        return current_state;
    }


}
