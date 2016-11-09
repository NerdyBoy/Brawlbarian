using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class t_game_controller : MonoBehaviour {

    public enum round_states { starting, playing, paused, ending, ended };
    public round_states round_state = round_states.starting;

    public static t_game_controller game_controller;

    private t2_timer timer;

    private t_player[] players;

    [SerializeField]
    private Text ui_player_weapon_upgrade = null;

    [SerializeField]
    private GameObject start_splash = null;
    [SerializeField]
    private GameObject end_splash = null;

    [SerializeField]
    private GameObject furniture = null;
    [SerializeField]
    private int round_length_minutes = 0;
    [SerializeField]
    private int round_length_seconds = 0;

    [SerializeField]
    private int upgrade_one_score_requirement = 0;
    [SerializeField]
    private float upgrade_one = 1;

    [SerializeField]
    private int upgrade_two_score_requirement = 0;
    [SerializeField]
    private float upgrade_two = 1;

    [SerializeField]
    private int upgrade_three_score_requirement = 0;
    [SerializeField]
    private float upgrade_three = 1;

    private int total_round_length_seconds = 0;
    private bool round_in_progress = false;
    private bool round_ended = false;

    private int time_points = 0;


    // Use this for initialization
    void Start() {
        if (null == game_controller) {
            game_controller = this;
        }
        else if (this != game_controller) {
            Destroy(this.gameObject);
        }

        players = GameObject.FindObjectsOfType<t_player>();
        timer = GetComponent<t2_timer>();
        Switch_State(round_states.starting);
        Assign_Player_Controls();

        
    }

    void Assign_Player_Controls() {
        players[0].Assign_Controller(t_player.input_types.mouse);
        for (int i = 1; i < players.Length; i++) {
            players[i].Assign_Controller(t_player.input_types.gamepad);
        }

    }

    // Update is called once per frame
    void Update() {
        switch (round_state) {
            case round_states.starting:
                Start_Update();
                break;
            case round_states.playing:
                Play_Update();
                break;
            case round_states.paused:
                Paused_Update();
                break;
            case round_states.ending:
                Ending_Update();
                break;
            case round_states.ended:
                Ended_Update();
                break;
            default:
                break;
        }
    }

    void Start_Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            timer.Set_Time(round_length_minutes, round_length_seconds);
            round_in_progress = true;
            Switch_State(round_states.playing);
        }
    }

    void Play_Update() {
        if (0 >= timer.Get_Seconds_Remaining() || 0 == furniture.transform.childCount) {
            Switch_State(round_states.ending);
        }
        if (true == Input.GetKeyDown(KeyCode.Escape)) {
            t_pause_controller.pause_controller.Pause();
            Switch_State(round_states.paused);
        }
        Update_UI_Time();
    }

    void Paused_Update() {
        if (true == Input.GetKeyDown(KeyCode.Escape)) {
            t_pause_controller.pause_controller.Unpause();
            Switch_State(round_states.playing);
        }
    }

    void Ending_Update() {
        //give out points
    }

    void Ended_Update() {
        //give weapon upgrade
        //show button to go to next level
    }

    void Switch_State(round_states _desired_state) {
        if (_desired_state == round_state) {
            UnityEngine.Debug.Log("You are trying to switch to a state you are already in!");
        }
        if (_desired_state == round_states.starting) {
            if (null != start_splash && false == start_splash.activeInHierarchy) {
                start_splash.SetActive(true);
            }
        }

        if (_desired_state == round_states.ending || _desired_state == round_states.ended || _desired_state == round_states.paused) {
            Cursor.lockState = CursorLockMode.None;
            timer.Stop_Timer();
            round_in_progress = false;

            if(_desired_state == round_states.ending) {
                time_points = timer.Get_Seconds_Remaining();
                StartCoroutine(Distribute_Points());
            }

            if (_desired_state == round_states.ended) {
                if (null != end_splash && false == end_splash.activeInHierarchy) {
                    Perform_Weapon_Upgrades();
                    end_splash.SetActive(true);
                }
            }
        }

        else if (_desired_state == round_states.playing) {
            if (true == start_splash.activeInHierarchy) {
                start_splash.SetActive(false);
            }
            Cursor.lockState = CursorLockMode.Locked;
            timer.Start_Timer();
            round_in_progress = true;
        }
        round_state = _desired_state;
    }

    public void Unpause() {
        Switch_State(round_states.playing);
    }

    void Update_UI_Time() {
        print(timer.Get_Current_Time_Minutes() + ":" + timer.Get_Current_Time_Seconds() + "    " + timer.Get_Seconds_Remaining());
        t_ui_round_time.ui_round_time.Update_Time(string.Format("{0:D2}:{1:D2}", timer.Get_Current_Time_Minutes(), timer.Get_Current_Time_Seconds()));
    }

    void Update_UI_Time_Score_Based() {
        t_ui_round_time.ui_round_time.Update_Time(string.Format("{0:D2}:{1:D2}", time_points / 60, time_points % 60));
    }

    public bool Get_Round_In_Progress() {
        return round_in_progress;
    }

    IEnumerator Distribute_Points() {
        while(0 < time_points) {
            for (int i = 0; i < players.Length; i++) {
                time_points -= 1;
                players[i].Add_Score(1);
            }
            Update_UI_Time_Score_Based();
            yield return new WaitForSeconds(0.012f);
        }
        Switch_State(round_states.ended);
    }

    void Perform_Weapon_Upgrades() {
        for(int i = 0; i < players.Length; i++) {
            if(players[i].Get_Score() > upgrade_one_score_requirement) {
                string weapon_strength = "Old weapon strength: " + players[i].Get_Weapon_Force().ToString();
                players[i].Set_Weapon_Force(players[i].Get_Weapon_Force() * upgrade_one);
                weapon_strength += "\nNew weapon strength: " + players[i].Get_Weapon_Force().ToString();
                ui_player_weapon_upgrade.text = weapon_strength;
            }
            else if(players[i].Get_Score() > upgrade_two_score_requirement) {
                string weapon_strength = "Old weapon strength: " + players[i].Get_Weapon_Force().ToString();
                players[i].Set_Weapon_Force(players[i].Get_Weapon_Force() * upgrade_two);
                weapon_strength += "\nNew weapon strength: " + players[i].Get_Weapon_Force().ToString();
                ui_player_weapon_upgrade.text = weapon_strength;
            }
            else if(players[i].Get_Score() > upgrade_three_score_requirement) {
                string weapon_strength = "Old weapon strength: " + players[i].Get_Weapon_Force().ToString();
                players[i].Set_Weapon_Force(players[i].Get_Weapon_Force() * upgrade_three);
                weapon_strength += "\nNew weapon strength: " + players[i].Get_Weapon_Force().ToString();
                ui_player_weapon_upgrade.text = weapon_strength;
            }
            else {
                ui_player_weapon_upgrade.text = "Score too low.\nNo weapon upgrade.";
            }
        }
    }
}
