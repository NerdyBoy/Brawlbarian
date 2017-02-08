using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(level_management))]
public class game_controller : MonoBehaviour {

    

    [SerializeField]
    private GameObject furniture_container;

    [SerializeField]
    private float round_start_coundown_time;

    [SerializeField]
    private float time_in_seconds;

    game_state_controller state_controller;

    level_management level_manager;

	// Use this for initialization
	void Start () {
        level_manager = GetComponent<level_management>();
        state_controller = GetComponent<game_state_controller>();
        Position_Players();
	}

    private void OnApplicationPause(bool pause)
    {
        if(pause == false)
        {
            cursor_display.Disable_Cursor();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level != 0 && level != 5)
        {
            cursor_display.Disable_Cursor();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if(focus == true)
        {
            cursor_display.Disable_Cursor();
        }
    }

    // Update is called once per frame
    void Update () {
        switch (state_controller.Get_Current_State())
        {
            case game_state_controller.game_states.round_start:
                Round_Start_Update();
                break;
            case game_state_controller.game_states.in_play:
                In_Play_Update();
                break;
            case game_state_controller.game_states.paused:
                Paused_Update();
                break;
            case game_state_controller.game_states.round_end:
                Round_End_Update();
                break;
        }
	}

    void Round_Start_Update()
    {
        game_state_controller.current_state_controller.Switch_State(game_state_controller.game_states.in_play);
    }

    void In_Play_Update()
    {
        /*if (null != furniture_container && furniture_container.transform.childCount <= 0)
        {
            game_state_controller.current_state_controller.Switch_State(game_state_controller.game_states.round_end);
        }*/
    }

    void Paused_Update()
    {
        
    }

    void Round_End_Update()
    {
        level_manager.Load_Next_Scene();        
    }

    void Begin_Play()
    {
    }

    void Begin_Pause()
    {
        cursor_display.Enable_Cursor();
    }

    void End_Pause()
    {
        cursor_display.Disable_Cursor();
    }

    void Begin_Round_End()
    {
    }

    void Position_Players()
    {
        /*GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        GameObject[] spawn_points = GameObject.FindGameObjectsWithTag("spawn_point");

        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < spawn_points.Length; j++)
            {
                spawn_point_component spawn = spawn_points[j].GetComponent<spawn_point_component>();
                bool successful_claim = spawn.Claim_Spawn_Point(players[i]);
                if(true == successful_claim)
                {
                    players[i].transform.position = spawn_points[j].transform.position;
                    players[i].transform.rotation = spawn_points[j].transform.rotation;
                    break;
                }
            }
        }*/
    }
}
