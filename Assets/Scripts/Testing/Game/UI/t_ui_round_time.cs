using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_round_time : MonoBehaviour {

    public static t_ui_round_time ui_round_time;

    [SerializeField]
    private Text ui_round_time_text = null;


	// Use this for initialization
	void Start () {
	    if(null == ui_round_time) {
            ui_round_time = this;
        }
        else if(this != ui_round_time) {
            Destroy(this.gameObject);
        }

        if(null == ui_round_time_text) {
            ui_round_time_text = GetComponentInChildren<Text>();
        }
	}
	
	public void Update_Time(string _new_time) {
        if (null == ui_round_time_text) {
            ui_round_time_text = GetComponentInChildren<Text>();
        }

        if (null != ui_round_time_text) {
            ui_round_time_text.text = _new_time;
        }
    }
}
