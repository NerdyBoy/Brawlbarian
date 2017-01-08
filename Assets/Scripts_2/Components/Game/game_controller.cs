using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class game_controller : MonoBehaviour {

    [SerializeField]
    private GameObject furniture_container;

    [SerializeField]
    private float round_start_coundown_time;

    [SerializeField]
    private float time_in_seconds;

    countdown game_countdown;

    game_state_controller state_controller;

	// Use this for initialization
	void Start () {
        game_countdown = this.gameObject.AddComponent<countdown>();
        state_controller = this.gameObject.AddComponent<game_state_controller>();
        game_countdown.Set_Time(round_start_coundown_time);
        game_countdown.Start_Timer();
        Position_Players();
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
        if(game_countdown.Get_Time_Remaining() <= 0)
        {
            game_countdown.Pause_Timer();
            game_state_controller.current_state_controller.Switch_State(game_state_controller.game_states.in_play);
        }
    }

    void In_Play_Update()
    {
        if (game_countdown.Get_Time_Remaining() <= 0 || furniture_container.transform.childCount <= 0)
        {
            game_countdown.Pause_Timer();
            game_state_controller.current_state_controller.Switch_State(game_state_controller.game_states.round_end);
        }
    }

    void Paused_Update()
    {
        
    }

    void Round_End_Update()
    {
        if(game_countdown.Get_Time_Remaining() <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //load next scene
        }
    }

    void Begin_Play()
    {
        game_countdown.Set_Time(time_in_seconds);
        game_countdown.Start_Timer();
    }

    void Begin_Pause()
    {
        cursor_display.Enable_Cursor();
        game_countdown.Pause_Timer();
    }

    void End_Pause()
    {
        cursor_display.Disable_Cursor();
        game_countdown.Start_Timer();
    }

    void Begin_Round_End()
    {
        game_countdown.Set_Time(15);
        game_countdown.Start_Timer();
    }

    void Position_Players()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
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
        }
    }
}
